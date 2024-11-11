using UnityEngine;

public class BaseLevelHandler : MonoBehaviour 
{
    protected GameLevelBase_SO _currentLevel;

    public void LevelInit(GameLevelBase_SO _currentLevel)
    {
        this._currentLevel = _currentLevel;
    }

    public virtual void LevelStart()
    {

    }

    public virtual void LevelEnd()
    {

    }
}
