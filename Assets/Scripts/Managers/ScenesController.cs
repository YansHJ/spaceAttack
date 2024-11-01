using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : Singleton<ScenesController>
{

    private void Start()
    {
        //异步叠加加载UI场景
        SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
    }
}
