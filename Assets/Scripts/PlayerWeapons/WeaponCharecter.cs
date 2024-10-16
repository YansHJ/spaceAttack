using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponCharecter : MonoBehaviour
{
    public int playerWeaponSpeed;

    public int maxWeaponHealth;

    public int weaponId;

    public GameObject bullet;

    public List<Transform> GetMuzzles()
    {
        Transform[] transforms = transform.GetComponentsInChildren<Transform>(true);
        Debug.Log(transforms.Length + "_______" + transforms);
        return transforms.Where(child => child.name == "GunMuzzle").ToList();
    }
}
