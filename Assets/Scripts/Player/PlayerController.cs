using MoreMountains.Feedbacks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _playerRb;

    private InputSystem_Player _playerInput;

    private Vector2 _playerDir;

    private PlayerCharecter _playerCharecter;

    private MMF_Player _camemaShakeFeedBack;
    private void Awake()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        _playerInput = new();
        _playerCharecter = GetComponent<PlayerCharecter>();
        _camemaShakeFeedBack = GetComponentInChildren<MMF_Player>();
    }

    private void Start()
    {
        _playerInput.PlayerOperate.Space.performed += Space => PlayerSpaceTouch();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Update()
    {
        _playerDir = _playerInput.PlayerMove.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        _playerRb.linearVelocity = _playerCharecter.playerSpeed * Time.fixedDeltaTime * _playerDir;
    }

    private void PlayerSpaceTouch()
    {
        _camemaShakeFeedBack.PlayFeedbacks();
    }
}
