using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelManager : Singleton<GameLevelManager>
{

    public GameLevelBase_SO _currentLevel;

    public List<GameLevelHandlerInfo> levelHandlers;

    private Dictionary<int, BaseLevelHandler> _levelHandlerDicts = new();

    private BaseLevelHandler handler;

    private float _currentTimer;


    private void Start()
    {
        //初始化关卡处理器
        HandlersInit();
        //关卡执行
        LevelExecute();
    }

    private void LevelExecute()
    {
        switch(_currentLevel.levelType)
        {
            case GameLevelTypes.Minion:
                LevelStart(1);
                break;
            case GameLevelTypes.Store:
                LevelStart(2);
                break;
                

        }
    }

    private void LevelStart(int handlerId)
    {
        //获取处理器
        _levelHandlerDicts.TryGetValue(handlerId, out handler);
        handler.LevelInit(_currentLevel);
        handler.LevelStart();
        //同步当前关卡时长
        _currentTimer = _currentLevel.levelTimer;
        //当前关卡为计时关卡
        if (_currentLevel.levelTiming)
        {
            //关卡倒计时开始
            StartCoroutine(LevelCountDown());
        }
    }

    private void LevelEnd()
    {
        //暂时
        _currentLevel = _currentLevel.nextLevelBranchs[0];

        handler.LevelEnd();

        //暂时
        LevelExecute();
    }


    /// <summary>
    /// 获取当前关卡倒计时
    /// </summary>
    /// <returns></returns>
    public float CurrentLevelTimer()
    {
        return _currentTimer;
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

    /// <summary>
    /// 关卡处理器初始化
    /// </summary>
    private void HandlersInit()
    {
        if (levelHandlers != null && levelHandlers.Count > 0)
        levelHandlers.ForEach(o =>
        {
            _levelHandlerDicts.Add(o.levelHandlerId, o.handler);
        });
    }

}
