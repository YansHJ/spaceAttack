using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : Singleton<PlayerWeaponManager>
{
    public PlayerWeapons_SO playerWeaponData;

    [SerializeField]
    private List<WeaponInfo> _playerWeaponList;

    private void Start()
    {
        _playerWeaponList = playerWeaponData.playerWeaponList;
    }

    /// <summary>
    /// 获取指定品质下的随机武器
    /// </summary>
    /// <param name="grade">品质</param>
    /// <returns></returns>
    public WeaponInfo GetRandomWeaponByGrade(WeaponGrade grade)
    {
        List<WeaponInfo> conformList = _playerWeaponList.FindAll(o => o.weaponGrade == grade);
        if (conformList.Count <= 0)
        {
            return _playerWeaponList[0];
        }
        return conformList[Random.Range(0, conformList.Count)];
    }
}
