using UnityEngine;

public class StoreLevelHandler : BaseLevelHandler
{

    public override void LevelStart()
    {
        base.LevelStart();
        //卸载运行时UI
        ScenesController.Instance.UnloadScene(SceneContants.RUNTIME_UI);
        //加载商店UI
        ScenesController.Instance.LoadSceneAsync(SceneContants.STORE_UI);
    }

    public override void LevelEnd()
    {
        base.LevelEnd();
        //卸载商店UI
        ScenesController.Instance.UnloadScene(SceneContants.STORE_UI);
    }
    
}
