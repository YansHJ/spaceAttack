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

    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }
}
