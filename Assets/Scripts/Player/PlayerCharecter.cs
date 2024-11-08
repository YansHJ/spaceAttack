using MoreMountains.Feedbacks;
using System;
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

    //玩家金币
    public int playerMoneyCnt;

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

    private void OnEnable()
    {
        EventManager.PlayerGetMoney += OnPlayerGetMoney;
    }

    private void OnDisable()
    {
        EventManager.PlayerGetMoney -= OnPlayerGetMoney;
    }

    /// <summary>
    /// 玩家获取金钱
    /// </summary>
    /// <param name="moneyCnt">金钱数量</param>
    private void OnPlayerGetMoney(int moneyCnt)
    {
        playerMoneyCnt += moneyCnt;
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
            //武器损毁提示框
            EventManager.CallPopover("装甲被击破！！");
            //武器损毁镜头振效
            _camemaShakeFeedBack.PlayFeedbacks();
            //重置玩家的武器状态
            currentWeapon = null;
            weaponActive = false;
            //销毁当前武器
            Destroy(_playerWeaponTrans.GetChild(0).gameObject);
            //自动装填下一个武器
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
                //没有下一个武器，玩家速度替换为本身的速度
                playerCurrentSpeed = playerBaseSpeed;
                weaponActive = false;
                return;
            }
            else
            {
                //切换至下一个武器
                currentWeapon = ownedWeapons[0];
                //移除武器列表的当前武器
                ownedWeapons.Remove(currentWeapon);
                //需改玩家的武器状态
                weaponActive = true;
                //修改玩家属性
                playerCurrentSpeed = currentWeapon.GetComponent<Weapon>().playerWeaponSpeed;
                playerCurrentWeaponHealth = currentWeapon.GetComponent<Weapon>().maxWeaponHealth;
            }
        }
        weaponActive = true;
        //装备下一把武器事件
        EventManager.CallEquipTheNextWeapon(currentWeapon);
    }
}
