using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameLevelBaseData", menuName = "Yans/GameLevelBaseData")]
public class GameLevelBase_SO : ScriptableObject
{
    [Header("关卡名称")]
    public string levelName;

    [Header("关卡ID")]
    public int levelId;

    [Header("关卡等级")]
    public int levelOfLevel;

    [Header("关卡类型")]
    public GameLevelTypes levelType;

    [Header("关卡持续时长")]
    public int levelTimer;

    [Header("关卡怪物信息")]
    public List<GameLevelMonsterInfo> monsterInfos;

    [Header("下一关分支")]
    public List<GameLevelBase_SO> nextLevelBranchs;
}
