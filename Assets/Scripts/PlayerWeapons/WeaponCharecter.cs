using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponCharecter : MonoBehaviour
{
    public int playerWeaponSpeed;

    public int maxWeaponHealth;

    public int weaponId;

    public List<Transform> weaponMuzzles;

    private void Awake()
    {
        Transform[] transforms = transform.GetComponentsInChildren<Transform>(true);
        weaponMuzzles = transforms.Where(child => child.name == "GunMuzzle").ToList();
    }
}
