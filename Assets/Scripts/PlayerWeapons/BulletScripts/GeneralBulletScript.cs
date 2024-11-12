using System.Collections;
using UnityEngine;

public class GeneralBulletScript : MonoBehaviour
{
    [Header("子弹存活时间")]
    public float bulletlifeTime = 3f;
    [Header("子弹威力")]
    public int bulletDamage = 1;
    
    public GameObject source;

    private void Start()
    {
        Destroy(gameObject, bulletlifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        Damage damage = new()
        {
            damageValue = bulletDamage
        };
        DamageInfo damageInfo = new()
        {
            defender = other.gameObject,
            damage = damage
        };
        if (source != null)
        {
            damageInfo.attacker = source;
        }
        Destroy(gameObject);
        EventManager.CallSubmitDamage(damageInfo);
    }
}
