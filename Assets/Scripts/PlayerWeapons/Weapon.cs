using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("玩家武器装甲移动速度")]
    public int playerWeaponSpeed;
    [Header("最大武器装甲血量")]
    public int maxWeaponHealth;
    [Header("武器Id")]
    public int weaponId;
    [Header("武器子弹预制件")]
    public GameObject bulletPerfab;
    [Header("武器枪口")]
    public List<Transform> muzzles;

    public void Attack()
    {
        //获取鼠标世界坐标
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mousePosition.z = 0;
        for (int i = 0; i < muzzles.Count; i++)
        {
            //获取当前枪口坐标
            Transform muzzle = muzzles[i];
            //Debug.Log("坐标：" + muzzle.position);
            //计算枪口到鼠标的向量值
            Vector3 bulletDir = (mousePosition - muzzle.position).normalized;
            //生成子弹并给一个速度
            GameObject bullet = Instantiate(bulletPerfab, muzzle.position, muzzle.rotation);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.linearVelocity = bulletDir * 20;
        }
    }
}
