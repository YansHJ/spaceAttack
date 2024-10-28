
using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    public static event Action<GameObject> EquipTheNextWeapon;

    public static void CallEquipTheNextWeapon(GameObject currentWeapon)
    {
        EquipTheNextWeapon?.Invoke(currentWeapon);
    }

    public static event Action<Collider2D, float> CauseDamage;

    public static void CallCauseDamage(Collider2D other, float bulletDamage)
    {
        CauseDamage?.Invoke(other, bulletDamage);
    }

    /// <summary>
    /// 开始生成怪物
    /// </summary>
    public static event Action<List<GameLevelMonsterInfo>> MonsterGenerateStart;
    public static void CallMonsterGenerateStart(List<GameLevelMonsterInfo> monsterInfos)
    {
        MonsterGenerateStart?.Invoke(monsterInfos);
    }
    /// <summary>
    /// 停止生成怪物
    /// </summary>
    public static event Action MonsterGenerateStop;
    public static void CallMonsterGenerateStop()
    {
        MonsterGenerateStop?.Invoke();
    }
}
