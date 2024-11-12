using UnityEngine;

[CreateAssetMenu(fileName = "BuffBaseModule", menuName = "Yans/BUFF/BuffBaseModule")]
public abstract class BuffBaseModule : ScriptableObject
{
    public abstract void Apply(BuffInfo buffInfo, DamageInfo damageInfo = null);
}
