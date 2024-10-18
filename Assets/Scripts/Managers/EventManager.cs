
using System;
using UnityEngine;

public class EventManager
{
    public static event Action<GameObject> EquipTheNextWeapon;

    public static void CallEquipTheNextWeapon(GameObject currentWeapon)
    {
        EquipTheNextWeapon?.Invoke(currentWeapon);
    }
}
