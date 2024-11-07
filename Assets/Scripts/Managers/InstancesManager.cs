using System.Collections.Generic;
using UnityEngine;

public class InstancesManager : Singleton<InstancesManager>
{

    public Transform defaultParent;

    /// <summary>
    /// 在指定父级下实例化
    /// </summary>
    /// <param name="objList"></param>
    /// <param name="parent"></param>
    public void Instantiate(List<GameObject> objList, Transform parent)
    {
        objList.ForEach(o =>
        {
            GameObject gameobject = Instantiate(o);
            gameobject.transform.SetParent(parent, false);
        });
    }

    /// <summary>
    /// 在默认父级下实例化
    /// </summary>
    /// <param name="objList"></param>
    public void Instantiate(List<GameObject> objList)
    {
        objList.ForEach(o =>
        {
            GameObject gameobject = Instantiate(o);
            gameobject.transform.SetParent(defaultParent, false);
        });
    }

    /// <summary>
    /// 在指定位置下实例化
    /// </summary>
    /// <param name="objList"></param>
    /// <param name="vector3"></param>
    public void Instantiate(List<GameObject> objList, Vector3 vector3)
    {
        objList.ForEach(o =>
        {
            GameObject gameobject = Instantiate(o, vector3, Quaternion.identity);
            gameobject.transform.SetParent(defaultParent, false);
        });
    }
}
