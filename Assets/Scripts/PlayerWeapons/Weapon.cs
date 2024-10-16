using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponCharecter _weaponCharecter;

    public Transform playerWeaponTrans;

    private void Start()
    {
        _weaponCharecter = transform.GetComponent<WeaponCharecter>();
        if (_weaponCharecter == null)
        {
            Debug.Log("武器属性为空");
        }
        playerWeaponTrans = GameObject.FindGameObjectWithTag("PlayerWeapon").transform;
        if (playerWeaponTrans == null)
        {
            Debug.Log("玩家武器实体为空");
        }
    }

    public void Attack(Transform topTransform)
    {
        //获取武器枪口位置
        List<Transform> muzzles = _weaponCharecter.GetMuzzles();
        //获取子弹预制体
        GameObject bulletPerfab = _weaponCharecter.bullet;
        //获取鼠标世界坐标
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerWeaponTrans = GameObject.FindGameObjectWithTag("PlayerWeapon").transform;
        //mousePosition.z = 0;
        for (int i = 0; i < muzzles.Count; i++)
        {
            //获取当前枪口坐标
            Transform muzzle = muzzles[i];
            //计算枪口旋转
            //muzzle.rotation = playerWeaponTrans.rotation;
            Debug.Log("祖物体：" + topTransform.position + "___" + topTransform.localPosition);
            Debug.Log("父物体：" + playerWeaponTrans.position + "___" + playerWeaponTrans.localPosition);
            Debug.Log("子物体：" + muzzle.position + "___" + muzzle.localPosition);
            //转换为玩家角色相对的坐标
            Vector3 muzzleWorldTrans = topTransform.TransformPoint(muzzle.position);
            Debug.Log("转换后的子物体：" + muzzleWorldTrans);
            //计算枪口到鼠标的向量值
            Vector3 bulletDir = (mousePosition - muzzleWorldTrans).normalized;
            //生成子弹并给一个速度
            GameObject bullet = Instantiate(bulletPerfab, muzzleWorldTrans, playerWeaponTrans.rotation);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.linearVelocity = bulletDir * 20;
        }
    }
}
