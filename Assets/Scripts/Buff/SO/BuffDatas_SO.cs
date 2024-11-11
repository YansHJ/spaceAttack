using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffDatas", menuName = "Yans/BuffDatas")]
public class BuffDatas_SO : ScriptableObject
{

    public List<Buff> buffs;

    public List<Buff> debuff;
}
