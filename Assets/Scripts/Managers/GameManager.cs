using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //玩家游戏对象
    private GameObject _player;

    //玩家获取队列
    private TaskCompletionSource<GameObject> _playerCompletionSource = new TaskCompletionSource<GameObject>();

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
        //重置队列
        _playerCompletionSource = new TaskCompletionSource<GameObject>();
    }

    private void OnPlayerInitCompleted(GameObject playerObj)
    {
        _player = playerObj;
        //队列返回结果
        _playerCompletionSource.TrySetResult(_player);
    }

    public async Task<GameObject> TryGetPlayer()
    {
        if (_player != null)
        {
            return _player;
        }
        return await _playerCompletionSource.Task;
    }

    /// <summary>
    /// 获取最顶级父级
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public GameObject TryGetRootParent(GameObject gameObject)
    {
        Transform currentTransfrom = gameObject.transform;
        while (currentTransfrom.parent != null)
        {
            currentTransfrom = currentTransfrom.parent;
        }
        return currentTransfrom.gameObject;
    }

}
