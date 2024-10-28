using DamageNumbersPro;
using UnityEngine;

public class DamageManager : Singleton<DamageManager>
{
    [Header("伤害文字预制件")]
    public DamageNumber damageNumberPerfab;
    [Header("伤害文字父级")]
    public GameObject damageParent;

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
            //伤害显示
            DamageTextSpawn(other.transform, bulletDamage);
            //伤害计算
            DamageToPlayer(other, bulletDamage);
        }
        if (other.tag.StartsWith("Enemy"))
        {
            //伤害显示
            DamageTextSpawn(other.transform, bulletDamage);
            //伤害计算
            DamageToEnemys(other, bulletDamage);
        }
    }

    private void DamageToEnemys(Collider2D other, float bulletDamage)
    {
        EnemyCharecter enemyCharecter = FindTopComponet<EnemyCharecter>(other);
        //计算对敌伤害
        enemyCharecter.enemyCurrentHealth -= (int) bulletDamage;
    }

    /// <summary>
    /// 对玩家造成伤害
    /// </summary>
    /// <param name="other">玩家目标</param>
    /// <param name="bulletDamage">伤害</param>
    private void DamageToPlayer(Collider2D other, float bulletDamage)
    {
        PlayerCharecter _playerCharecter = FindTopComponet<PlayerCharecter>(other);
        if (_playerCharecter.playerCurrentWeaponHealth - bulletDamage <= 0)
        {
            _playerCharecter.playerCurrentHealth -= ((int)bulletDamage - _playerCharecter.playerCurrentWeaponHealth);
            _playerCharecter.playerCurrentWeaponHealth = 0;
        }
        else
        {
            _playerCharecter.playerCurrentWeaponHealth -= (int)bulletDamage;
        }
    }

    /// <summary>
    /// 生成伤害文字显示
    /// </summary>
    /// <param name="trans">位置</param>
    /// <param name="damage">伤害</param>
    private void DamageTextSpawn(Transform trans, float damage)
    {
        DamageNumber damageNumber = damageNumberPerfab.Spawn(trans.position, damage);
        damageNumber.PrewarmPool();
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
