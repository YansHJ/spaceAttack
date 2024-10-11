using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public Rigidbody2D playerRb;

    private InputSystem_Player playerInput;

    private Vector2 playerDir;

    public int playerSpeed = 400;

    private void Awake()
    {
        playerInput = new();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void Update()
    {
        playerDir = playerInput.PlayerMove.Move.ReadValue<Vector2>();
        
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        playerRb.linearVelocity = playerSpeed * Time.fixedDeltaTime * playerDir;
    }

}
