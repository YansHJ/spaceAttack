using MoreMountains.Feedbacks;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharecter : MonoBehaviour
{
    //玩家基础速度
    public int playerBaseSpeed;
    //玩家速度
    public int playerCurrentSpeed;
    //玩家最大血量
    public int playerMaxHealth;
    //玩家当前血量
    public int playerCurrentHealth;
    //玩家当前武器血量
    public int playerCurrentWeaponHealth = 0;
    //武器状态
    public bool weaponActive = true;
    //当前装备武器
    public GameObject currentWeapon;
    //拥有的武器列表
    public List<GameObject> ownedWeapons;
    //玩家武器父物体
    private Transform _playerWeaponTrans;
    //FEEL屏幕震动
    private MMF_Player _camemaShakeFeedBack;

    private void Awake()
    {
        _playerWeaponTrans = transform.Find("Weapons");
        _camemaShakeFeedBack = GetComponentInChildren<MMF_Player>();
    }

    private void Start()
    {
        PlayerCharecterInit();
        WeaponInit();
    }

    private void Update()
    {
        WeaponDestroyCheck();
    }

    private void HealthCheck()
    {
        
    }

    private void WeaponDestroyCheck()
    {
        if (0 >= playerCurrentWeaponHealth && null != currentWeapon)
        {
            _camemaShakeFeedBack.PlayFeedbacks();
            currentWeapon = null;
            weaponActive = false;
            Destroy(_playerWeaponTrans.GetChild(0).gameObject);
            WeaponInit();
        }
    }

    private void PlayerCharecterInit()
    {
        playerCurrentHealth = playerMaxHealth;
        playerCurrentSpeed = playerBaseSpeed;
    }

    /// <summary>
    /// 玩家武器初始化
    /// </summary>
    private void WeaponInit()
    {
        if (null == currentWeapon)
        {
            if (ownedWeapons.Count < 1)
            {
                playerCurrentSpeed = playerBaseSpeed;
                weaponActive = false;
                return;
            }
            else
            {
                currentWeapon = ownedWeapons[0];
                ownedWeapons.Remove(currentWeapon);
                weaponActive = true;
                //修改玩家属性
                playerCurrentSpeed = currentWeapon.GetComponent<Weapon>().playerWeaponSpeed;
                playerCurrentWeaponHealth = currentWeapon.GetComponent<Weapon>().maxWeaponHealth;
            }
        }
        weaponActive = true;
        EventManager.CallEquipTheNextWeapon(currentWeapon);
    }
}
