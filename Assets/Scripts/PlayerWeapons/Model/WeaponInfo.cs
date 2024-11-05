using UnityEngine;

[System.Serializable]
public class WeaponInfo
{
    public string weaponId;

    public string weaponName;

    public WeaponGrade weaponGrade;

    public GameObject weaponObj;
}

public enum WeaponGrade
{
    white
}
