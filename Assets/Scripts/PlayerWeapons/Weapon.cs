using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("玩家武器装甲移动速度")]
    public int playerWeaponSpeed;

    [Header("最大武器装甲血量")]
    public int maxWeaponHealth;

    [Header("子弹速度")]
    public int bulletSpeed;

    [Header("子弹左偏移")]
    [Range(-0.1f, 0)]
    public float bulletOffsetLeft;

    [Header("子弹右偏移")]
    [Range(0, 0.1f)]
    public float bulletOffsetRight;

    [Header("武器Id")]
    public int weaponId;

    [Header("武器子弹预制件")]
    public GameObject bulletPerfab;

    [Header("武器枪口")]
    public List<Transform> muzzles;

    //子弹父物体
    private GameObject _bulletsParent;

    public void Attack()
    {
        _bulletsParent = GameObject.FindGameObjectWithTag(TagConstants.BULLETS_PARENT);
        //获取鼠标世界坐标
        Vector3 mousePosition = GetMousePos();
        //mousePosition.z = 0;
        for (int i = 0; i < muzzles.Count; i++)
        {
            //获取当前枪口坐标
            Transform muzzle = muzzles[i];
            //Debug.Log("坐标：" + muzzle.position);
            //计算枪口到鼠标的向量值
            Vector3 bulletDir = (mousePosition - muzzle.position).normalized;
            //枪口偏移
            bulletDir = BulletOffset(bulletDir);
            //生成子弹并给一个速度
            GameObject bullet = Instantiate(bulletPerfab, muzzle.position, muzzle.rotation);
            bullet.transform.parent = _bulletsParent.transform;
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.linearVelocity = bulletDir * bulletSpeed;
        }
    }

    /// <summary>
    /// 子弹
    /// </summary>
    /// <param name="bulletDir"></param>
    /// <returns></returns>
    private Vector3 BulletOffset(Vector3 bulletDir)
    {
        float randomNumx = Random.Range(bulletOffsetLeft, bulletOffsetRight);
        float randomNumy = Random.Range(bulletOffsetLeft, bulletOffsetRight);
        return new Vector3(bulletDir.x + randomNumx, bulletDir.y + randomNumy, bulletDir.z);
    }

    /// <summary>
    /// 获取鼠标相对世界位置坐标
    /// </summary>
    /// <returns></returns>
    private Vector3 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
