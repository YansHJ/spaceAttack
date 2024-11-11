using UnityEngine;

[System.Serializable]
public class GameLevelHandlerInfo
{
    [Header("关卡处理器Id")]
    public int levelHandlerId;

    [Header("关卡处理器实例")]
    public BaseLevelHandler handler;
}
