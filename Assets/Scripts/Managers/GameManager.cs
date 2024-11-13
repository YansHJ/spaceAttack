using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
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

    /// <summary>
    /// 在OBJ所在结构中取第一个符合条件的
    /// </summary>
    /// <typeparam name="T">组件</typeparam>
    /// <param name="gameObject">OBJ</param>
    /// <returns>符合条件的OBJ</returns>
    public GameObject TryGetFirstObjectWithComponetInHierarchy<T>(GameObject gameObject) where T : Component
    {
        GameObject root = TryGetRootParent(gameObject);
        List<GameObject> resultList = new();
        //取子级符合条件的
        TryGetObjectsWithComponetInChilds<T>(root, resultList);
        if (resultList.Count > 0)
        {
            //取第一个符合条件的
            return resultList[0];
        }
        return default;
    }

    /// <summary>
    /// 在结构中取组件
    /// </summary>
    /// <typeparam name="T">组件</typeparam>
    /// <param name="gameObject">OBJ</param>
    /// <returns>组件</returns>
    public T TryGetComponetInHierarchy<T>(GameObject gameObject) where T : Component
    {
        GameObject root = TryGetRootParent(gameObject);
        List<GameObject> resultList = new();
        //取子级符合条件的
        TryGetObjectsWithComponetInChilds<T>(root, resultList);
        if (resultList.Count > 0)
        {
            //取第一个符合条件的
            return resultList[0].GetComponent<T>();
        }
        return default;
    }

    /// <summary>
    /// 递归查找符合条件的OBJ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="gameObject">父级</param>
    /// <param name="eligibleList">符合的列表</param>
    private void TryGetObjectsWithComponetInChilds<T>(GameObject gameObject, List<GameObject> eligibleList) where T : Component
    {
        if (gameObject.TryGetComponent(out T _))
        {
            eligibleList.Add(gameObject);
        }

        foreach(Transform child in gameObject.transform)
        {
            TryGetObjectsWithComponetInChilds<T>(child.gameObject, eligibleList);
        }
    } 

    

}
