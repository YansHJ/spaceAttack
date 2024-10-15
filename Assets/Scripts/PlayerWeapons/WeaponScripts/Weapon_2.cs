using UnityEngine;

public class Weapon_2 : MonoBehaviour, IWeapon
{
    public void Attack(Transform topTransform)
    {
        Debug.Log("Weapon_2 Attack");
    }
}
