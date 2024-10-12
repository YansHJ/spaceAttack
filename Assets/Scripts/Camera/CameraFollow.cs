using Unity.Cinemachine;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTrans;

    public CinemachineCamera cam;

    private void Update()
    {
        Vector3 playerPosition = new(playerTrans.position.x, playerTrans.position.y, -5);
        transform.position = playerPosition;
    }
}
