
using System;
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
}
