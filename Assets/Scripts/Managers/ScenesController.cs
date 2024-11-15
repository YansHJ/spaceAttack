using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : Singleton<ScenesController>
{

    private void Start()
    {
        LoadSceneAsync(SceneContants.RUNTIME_UI);
    }

    public void LoadSceneAsync(string sceneName)
    {
        //异步叠加加载UI场景
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    /// <summary>
    /// 尝试异步卸载场景
    /// </summary>
    /// <param name="sceneName">场景名称</param>
    public void TryUnloadScene(string sceneName)
    {
        if (IsSceneActive(sceneName))
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }   
    }

    /// <summary>
    /// 当前场景是否已经存在、激活
    /// </summary>
    /// <param name="sceneName">场景名称</param>
    /// <returns></returns>
    private bool IsSceneActive(string sceneName)
    {
        for(int i = 0;i < SceneManager.sceneCount;i++)
        {
            if (SceneManager.GetSceneAt(i).name == sceneName)
            {
                return true;
            }
        }
        return false;
    }
}
