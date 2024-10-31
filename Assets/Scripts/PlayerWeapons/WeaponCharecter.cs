using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponCharecter : MonoBehaviour
{
    private Weapon _weapon;

    private PlayerCharecter _playerCharecter;

    private void Awake()
    {
        _playerCharecter = transform.parent.GetComponent<PlayerCharecter>();
    }

    private void OnEnable()
    {
        EventManager.EquipTheNextWeapon += OnEquipTheNextWeapon;
    }

    private void OnDisable()
    {
        EventManager.EquipTheNextWeapon -= OnEquipTheNextWeapon;
    }

    private void OnEquipTheNextWeapon(GameObject currentWeapon)
    {
        GameObject weapon = Instantiate(currentWeapon);
        weapon.transform.SetParent(transform, false);
        _weapon = weapon.GetComponent<Weapon>();
    }

    public void Attack()
    {
        _weapon.Attack();
    }
}
