using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    public Camera mainCamera;

    public Vector2 TopLeftPosition()
    {
        return mainCamera.ViewportToWorldPoint(new Vector3(0, 1, mainCamera.nearClipPlane));
    }

    public Vector2 TopRightPosition()
    {
        return mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
    }

    public Vector2 BottomLeftPosition()
    {
        return mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
    }

    public Vector2 BottomRightPosition()
    {
        return mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.nearClipPlane));
    }
}
