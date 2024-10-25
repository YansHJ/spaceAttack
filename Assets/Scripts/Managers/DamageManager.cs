using DamageNumbersPro;
using UnityEngine;

public class DamageManager : Singleton<DamageManager>
{
    public DamageNumber damageNumberPerfab;

    private void OnEnable()
    {
        EventManager.CauseDamage += OnCauseDamage;
    }

    private void OnDisable()
    {
        EventManager.CauseDamage -= OnCauseDamage;
    }

    private void OnCauseDamage(Collider2D other, float bulletDamage)
    {
        Debug.Log("当前伤害Tag: " + other.tag);
        if (other.tag.StartsWith("Player"))
        {
            damageNumberPerfab.Spawn(other.transform.position, bulletDamage);
            PlayerCharecter _playerCharecter = FindTopComponet<PlayerCharecter>(other);
            if (_playerCharecter.playerCurrentWeaponHealth - bulletDamage <= 0)
            {
                _playerCharecter.playerCurrentHealth -= ((int)bulletDamage - _playerCharecter.playerCurrentWeaponHealth);
                _playerCharecter.playerCurrentWeaponHealth = 0;
            }
            else
            {
                _playerCharecter.playerCurrentWeaponHealth -= (int) bulletDamage;
            }
        }
    }

    /// <summary>
    /// 获取当前最顶级物体的某个组件
    /// </summary>
    /// <typeparam name="T">组件类型</typeparam>
    /// <param name="other">当前碰撞体</param>
    /// <returns>组件</returns>
    T FindTopComponet<T>(Collider2D other) where T : Component
    {
        Transform current = other.transform;
        while (null != current.parent)
        {
            current = current.parent;
        }
        return current.GetComponent<T>();
    }
}
