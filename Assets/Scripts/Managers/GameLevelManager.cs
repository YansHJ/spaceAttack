using System.Collections;
using UnityEngine;

public class GameLevelManager : Singleton<GameLevelManager>
{

    public GameLevelBase_SO _currentLevel;

    private float _currentTimer;


    private void Start()
    {
        LevelExecute();
    }

    private void LevelExecute()
    {
        switch(_currentLevel.levelType)
        {
            case GameLevelTypes.Minion:
                break;

        }

        StartCoroutine(LevelStart());
    }

    IEnumerator LevelStart()
    {
        //同步当前关卡时长
        _currentTimer = _currentLevel.levelTimer;
        //15s准备时间
        yield return new WaitForSeconds(15f);
        //开始生成怪物事件
        EventManager.CallMonsterGenerateStart(_currentLevel.monsterInfos, _currentLevel.monsterGenerateInterval);
        //关卡倒计时开始
        StartCoroutine(LevelCountDown());
    }

    /// <summary>
    /// 获取当前关卡倒计时
    /// </summary>
    /// <returns></returns>
    public float CurrentLevelTimer()
    {
        return _currentTimer;
    }

    private void LevelEnd()
    {
        //暂时
        _currentLevel = _currentLevel.nextLevelBranchs[0];
        //停止怪物生成事件
        EventManager.CallMonsterGenerateStop();
        //暂时
        LevelExecute();
    }


    /// <summary>
    /// 关卡倒计时
    /// </summary>
    /// <returns></returns>
    IEnumerator LevelCountDown()
    {
        while (_currentTimer >= 1)
        {
            _currentTimer--;
            yield return new WaitForSeconds(1f);
        }
        if (_currentTimer <= 0)
        {
            LevelEnd();
        }
    }


}
