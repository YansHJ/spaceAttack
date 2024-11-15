using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWeaponData", menuName = "Yans/players/PlayerWeaponData")]
public class PlayerWeapons_SO : ScriptableObject
{
    //玩家拥有的所有武器
    public List<WeaponInfo> playerWeaponList;
}
