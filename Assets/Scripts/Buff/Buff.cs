using UnityEngine;

[System.Serializable]
public abstract class Buff
{
    [Header("buff名称")]
    public string buffName;
    [Header("buff类型")]
    public BuffType buffType;
    [Header("是否永久生效")]
    public bool isPermanent;
    [Header("持续时长")]
    public float buffDuration;
    [Header("作用目标")]
    protected GameObject targetObj;

    public Buff(string buffName, BuffType buffType, bool isPermanent, float buffDuration, GameObject targetObj)
    {
        this.buffName = buffName;
        this.buffType = buffType;
        this.isPermanent = isPermanent;
        this.buffDuration = buffDuration;
        this.targetObj = targetObj;
    }

    protected abstract void Apply();

    protected abstract void Remove();

    public virtual void Update(float deltaTime)
    {
        if (!isPermanent)
        {
            buffDuration -= deltaTime;
            if (buffDuration <= 0)
            {
                Remove();
            }
        }
    }
}

public enum BuffType
{
    buff,debuff
}
