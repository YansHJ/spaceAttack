using System;
using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //玩家游戏对象
    private GameObject _player;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        EventManager.PlayerInitCompleted += OnPlayerInitCompleted;
        EventManager.PlayerDestroied += OnPlayerDestroied;
    }

    private void OnDisable()
    {
        EventManager.PlayerInitCompleted -= OnPlayerInitCompleted;
        EventManager.PlayerDestroied -= OnPlayerDestroied;
    }

    private void OnPlayerDestroied()
    {
        _player = null;
    }

    private void OnPlayerInitCompleted(GameObject playerObj)
    {
        _player = playerObj;
    }

    /// <summary>
    /// 尝试获取玩家实体
    /// </summary>
    /// <param name="callBack">回调方法</param>
    /// <returns></returns>
    public bool TryGetPlayer(Action<GameObject> callBack)
    {
        if (_player != null)
        {
            callBack?.Invoke(_player);
            return true;
        }
        else
        {
            StartCoroutine(TryGetPlayerIEnumerator(callBack));
            return false;
        }
    }

    IEnumerator TryGetPlayerIEnumerator(Action<GameObject> callBack)
    {
        yield return new WaitUntil(() => _player != null);
        callBack?.Invoke(_player);
    }

}
