using UnityEngine;

[CreateAssetMenu(fileName = "HpIncreaseOnlyOnTime", menuName = "Yans/BUFF/BuffAchieves/HpIncreaseOnlyOnTime")]
public class HpIncreaseOnlyOnTime : BuffBaseModule
{
    public BuffProperty property;

    public override void Apply(BuffInfo buffInfo, DamageInfo damageInfo = null)
    {
        if (GameManager.Instance.TryGetRootParent(buffInfo.target).TryGetComponent<Charecter>(out var charecter))
        {
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
