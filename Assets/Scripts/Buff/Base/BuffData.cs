using UnityEngine;

[CreateAssetMenu(fileName = "BuffData", menuName = "Yans/BUFF/BuffData")]
public class BuffData : ScriptableObject
{
    [Header("Buff ID")]
    public int buffId;
    [Header("Buff 名称")]
    public string buffName;
    [Header("Buff 描述")]
    public string buffDescription;
    [Header("Buff 图标")]
    public Sprite buffIcon;
    [Header("Buff 优先级")]
    public int priority;
    [Header("Buff 最大层数")]
    public int maxStack;
    [Header("Buff 标签列")]
    public string[] tags;

    //时间信息
    [Header("Buff 是否永久生效")]
    public bool isPermanent;
    [Header("Buff 持续时间")]
    public float duration;
    [Header("Buff 触发间隔")]
    public float tickTime;

    //更新方式
    [Header("Buff 更新时间的方式")]
    public BuffUpdateTimeEnum updateTimeEnum;
    [Header("Buff 移除时更新时间的方式")]
    public BuffRemoveStackUpdateEnum removeStackUpdateEnum;

    //回调点
    [Header("Buff 创建时")]
    public BuffBaseModule OnCreate;
    [Header("Buff 移除时")]
    public BuffBaseModule OnRemove;
    [Header("Buff 触发时")]
    public BuffBaseModule OnTick;
    [Header("Buff 伤害时")]
    public BuffBaseModule OnHit;
    [Header("Buff 被伤害时")]
    public BuffBaseModule OnBeHurt;
    [Header("Buff 击杀时")]
    public BuffBaseModule OnKill;
    [Header("Buff 被击杀时")]
    public BuffBaseModule OnBeKill;


}
