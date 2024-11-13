using UnityEngine;

[CreateAssetMenu(fileName = "ImmediateDamage", menuName = "Yans/BUFF/BuffAchieves/ImmediateDamage")]
public class ImmediateDamage : BuffBaseModule
{
    [Header("增伤数值")]
    public int damageIncrease = 0;
    [Header("伤害增幅比例(百分比)")]
    public float damageIncreaseProportion = 0;

    public override void Apply(BuffInfo buffInfo, DamageInfo damageInfo = null)
    {
        if (null != damageInfo)
        {
            Damage damage = damageInfo.damage;
            damage.damageValue += damageIncrease;
            damage.damageValue += (int) (damage.damageValue * damageIncreaseProportion);
        }
    }
}
