using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWeaponData", menuName = "Yans/players/PlayerWeaponData")]
public class PlayerWeapons_SO : ScriptableObject
{
    //玩家可拥有的所有武器
    public List<GameObject> allPlayerWeapons;

    //玩家当前拥有的武器
    public List<GameObject> currentPlayerWeapons;
}
