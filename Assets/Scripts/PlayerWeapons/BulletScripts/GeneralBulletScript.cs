using System.Collections;
using UnityEngine;

public class GeneralBulletScript : MonoBehaviour
{
    [Header("子弹存活时间")]
    public float bulletlifeTime = 3f;
    [Header("子弹威力")]
    public float bulletDamage = 1f;

    private void Start()
    {
        Destroy(gameObject, bulletlifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
        EventManager.CallCauseDamage(other, bulletDamage);
    }
}
