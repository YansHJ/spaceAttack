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
        EventManager.SubmitDamage += OnSubmitDamage;
    }

    private void OnDisable()
    {
        EventManager.SubmitDamage -= OnSubmitDamage;
    }

    private void OnSubmitDamage(DamageInfo damageInfo)
    {
        //BUFF处理
        if (null != damageInfo.attacker && null != damageInfo.defender)
        {
            BuffHandle(damageInfo);
        }
        //伤害结算
        Debug.Log("当前伤害Tag: " + damageInfo.defender.tag);
        if (damageInfo.defender.tag.StartsWith("Player"))
        {
            //伤害计算
            DamageToPlayer(damageInfo);
        }
        if (damageInfo.defender.tag.StartsWith("Enemy"))
        {
            //伤害计算
            DefaultDamageCheck(damageInfo);
        }
        //伤害显示
        DamageTextSpawn(damageInfo.defender.transform, damageInfo.damage.damageValue);
    }

    private void DefaultDamageCheck(DamageInfo damageInfo)
    {
        if (damageInfo.defender.TryGetComponent<Charecter>(out var charecter))
        {
            charecter.currentHealth -= damageInfo.damage.damageValue;
        }
    }

    /// <summary>
    /// 对玩家造成伤害
    /// </summary>
    /// <param name="other">玩家目标</param>
    /// <param name="bulletDamage">伤害</param>
    private void DamageToPlayer(DamageInfo damageInfo)
    {
        PlayerCharecter _playerCharecter = FindTopComponet<PlayerCharecter>(damageInfo.defender);
        if (_playerCharecter.playerCurrentWeaponHealth - damageInfo.damage.damageValue <= 0)
        {
            _playerCharecter.currentHealth -= (damageInfo.damage.damageValue - _playerCharecter.playerCurrentWeaponHealth);
            _playerCharecter.playerCurrentWeaponHealth = 0;
        }
        else
        {
            _playerCharecter.playerCurrentWeaponHealth -= damageInfo.damage.damageValue;
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
    T FindTopComponet<T>(GameObject obj) where T : Component
    {
        Transform current = obj.transform;
        while (null != current.parent)
        {
            current = current.parent;
        }
        return current.GetComponent<T>();
    }

    /// <summary>
    /// 伤害Buff处理
    /// </summary>
    /// <param name="damageInfo"></param>
    private void BuffHandle(DamageInfo damageInfo)
    {
        //攻击者的BUFF
        BuffHandler attackerBuffHandler = GameManager.Instance.TryGetComponetInHierarchy<BuffHandler>(damageInfo.attacker);
        if (null != attackerBuffHandler)
        {
            attackerBuffHandler.activeBuffList.ForEach(o =>
            {
                o.buffData.OnHit.Apply(o, damageInfo);
            });
        }
        //受击者的BUFF
        BuffHandler defenderBuffHandler = GameManager.Instance.TryGetComponetInHierarchy<BuffHandler>(damageInfo.defender);
        if (null != defenderBuffHandler)
        {
            defenderBuffHandler.activeBuffList.ForEach(o =>
            {
                o.buffData.OnBeHurt.Apply(o, damageInfo);
            });
        }
    }
}
