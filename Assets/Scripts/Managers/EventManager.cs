
using System;

public class EventManager
{
    public static event Action EquipTheNextWeapon;

    public static void CallEquipTheNextWeapon()
    {
        EquipTheNextWeapon?.Invoke();
    }
}
