using UnityEngine;

public class GameLevelManager : Singleton<GameLevelManager>
{

    public GameLevelBase_SO _currentLevel;


    private void Start()
    {
        LevelExecute();
    }

    private void LevelExecute()
    {
        EventManager.CallMonsterGenerateStart(_currentLevel.monsterInfos);
    }
}
