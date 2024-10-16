using System.Collections.Generic;
using UnityEngine;

public class Weapon_1 : MonoBehaviour, IWeapon
{
    private WeaponCharecter _weaponCharecter;

    public Transform playerWeaponTrans;

    private void Start()
    {
        _weaponCharecter = transform.GetComponent<WeaponCharecter>();
        if (_weaponCharecter == null)
        {
            Debug.Log("武器属性为空");
        }
        playerWeaponTrans = GameObject.FindGameObjectWithTag("PlayerWeapon").transform;
    }

    public void Attack(Transform topTransform)
    {
        //获取武器枪口位置
        List<Transform> muzzles = _weaponCharecter.GetMuzzles();
        //获取子弹预制体
        GameObject bulletPerfab = _weaponCharecter.bullet;
        //获取鼠标世界坐标
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mousePosition.z = 0;
        for (int i = 0; i < muzzles.Count; i++)
        {
            //获取当前枪口坐标
            Transform muzzle = muzzles[i];
            //计算枪口旋转
            muzzle.rotation = playerWeaponTrans.rotation;
            //转换为玩家角色相对的坐标
            Vector3 muzzleWorldTrans = topTransform.TransformPoint(muzzle.position);
            //计算枪口到鼠标的向量值
            Vector3 bulletDir = (mousePosition - muzzleWorldTrans).normalized;
            //生成子弹并给一个速度
            GameObject bullet = Instantiate(bulletPerfab, muzzleWorldTrans, playerWeaponTrans.rotation);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.linearVelocity = bulletDir * 20;
        }
    }
}
