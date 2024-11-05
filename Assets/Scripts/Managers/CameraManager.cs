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

    /// <summary>
    /// 当前坐标是否在相机范围内
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public bool IsInView(Vector3 point)
    {
        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(point);
        return viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1;
    }
}

