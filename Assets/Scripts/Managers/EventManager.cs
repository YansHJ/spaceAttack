
using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    //装备下一个武器
    public static event Action<GameObject> EquipTheNextWeapon;
    public static void CallEquipTheNextWeapon(GameObject currentWeapon)
    {
        EquipTheNextWeapon?.Invoke(currentWeapon);
    }

    //造成伤害
    public static event Action<Collider2D, float> CauseDamage;
    public static void CallCauseDamage(Collider2D other, float bulletDamage)
    {
        CauseDamage?.Invoke(other, bulletDamage);
    }

    /// <summary>
    /// 开始生成怪物
    /// </summary>
    public static event Action<List<GameLevelMonsterInfo>, float> MonsterGenerateStart;
    public static void CallMonsterGenerateStart(List<GameLevelMonsterInfo> monsterInfos, float generateInterval)
    {
        MonsterGenerateStart?.Invoke(monsterInfos, generateInterval);
    }
    /// <summary>
    /// 停止生成怪物
    /// </summary>
    public static event Action MonsterGenerateStop;
    public static void CallMonsterGenerateStop()
    {
        MonsterGenerateStop?.Invoke();
    }

    /// <summary>
    /// 玩家初始化完成
    /// </summary>
    public static event Action<GameObject> PlayerInitCompleted;
    public static void CallPlayerInitCompleted(GameObject playerObj)
    {
        PlayerInitCompleted?.Invoke(playerObj);
    }

    /// <summary>
    /// 玩家销毁事件
    /// </summary>
    public static event Action PlayerDestroied;
    public static void CallPlayerDestroied()
    {
        PlayerDestroied?.Invoke();
    }

    /// <summary>
    /// 玩家获取金钱
    /// </summary>
    public static event Action<int> PlayerGetMoney;
    public static void CallPlayerGetMoney(int money)
    {
        PlayerGetMoney?.Invoke(money);
    }

    /// <summary>
    /// 提示框事件
    /// </summary>
    public static event Action<string> Popover;
    public static void CallPopover(string message)
    {
        Popover?.Invoke(message);
    }
}
