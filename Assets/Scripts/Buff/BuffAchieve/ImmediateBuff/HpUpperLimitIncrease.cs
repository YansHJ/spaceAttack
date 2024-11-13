using UnityEngine;

/// <summary>
/// 提高血量上限
/// </summary>
[CreateAssetMenu(fileName = "HpUpperLimitIncrease", menuName = "Yans/BUFF/BuffAchieves/HpUpperLimitIncrease")]
public class HpUpperLimitIncrease : BuffBaseModule
{
    public BuffProperty property;

    public override void Apply(BuffInfo buffInfo, DamageInfo damageInfo = null)
    {
        if (GameManager.Instance.TryGetRootParent(buffInfo.target).TryGetComponent<Charecter>(out var charecter))
        {
            charecter.maxHealth += property.health;
            if (charecter.maxHealth <= charecter.currentHealth + property.health)
            {
                charecter.currentHealth = charecter.maxHealth;
            }
            else
            {
                charecter.currentHealth += property.health;
            }
        }
    }
}
