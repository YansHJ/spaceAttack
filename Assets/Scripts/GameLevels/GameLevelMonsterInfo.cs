
using UnityEngine;

[System.Serializable]
public class GameLevelMonsterInfo
{
    [Header("怪物预制件")]
    public GameObject monsterPrefab;
    [Header("生成概率")]
    [Range(0.1f, 0.99f)]
    public float refreshProbability;
    [Header("怪物最大数量")]
    public int max;
}
