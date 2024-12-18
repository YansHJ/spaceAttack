using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //玩家刚体
    private Rigidbody2D _playerRb;
    //玩家武器
    private Transform _playerWeaponTrans;
    //玩家主体
    private Transform _playerMainBodyTrans;
    //玩家输入
    private InputSystem_Player _playerInput;
    //玩家方向
    private Vector2 _playerDir;
    //玩家属性
    private PlayerCharecter _playerCharecter;
    //FEEL屏幕震动
    private MMF_Player _camemaShakeFeedBack;

    //鼠标世界坐标
    private Vector3 mouseWorldPosition;

    private void Awake()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        _playerInput = new();
        _playerCharecter = GetComponent<PlayerCharecter>();
        _camemaShakeFeedBack = GetComponentInChildren<MMF_Player>();
        _playerWeaponTrans = transform.Find("Weapons");
        _playerMainBodyTrans = transform.Find("MainBody");
    }

    private void Start()
    {
        _playerInput.PlayerOperate.Space.performed += Space => PlayerSpaceTouch();
        _playerInput.PlayerOperate.BaseAttack.performed += BaseAttack => PlayerBaseAttack();
        StartCoroutine(PlayerAutoRotation());
        //玩家初始化事件
        EventManager.CallPlayerInitCompleted(this.gameObject);
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
        MousePosition();
    }

    private void FixedUpdate()
    {
        PlayerWeaponRotate();
        PlayerMove();
    }

    /// <summary>
    /// 玩家移动
    /// </summary>
    private void PlayerMove()
    {
        _playerRb.linearVelocity = _playerCharecter.currentSpeed * Time.fixedDeltaTime * _playerDir;
    }

    /// <summary>
    /// 玩家空格(Test)
    /// </summary>
    private void PlayerSpaceTouch()
    {
        //_camemaShakeFeedBack.PlayFeedbacks();

        //测试
        /*WeaponInfo weaponInfo = PlayerWeaponManager.Instance.GetRandomWeaponByGrade(WeaponGrade.white);
        Instantiate(weaponInfo.weaponObj, transform.transform);*/

        //测试取物品
        List<Item> items = ItemManager.Instance.TryGetItemsWithConditions(5, ItemType.Any, ItemQuality.Any);
        string value = "";
        items.ForEach(o =>
        {
            value += o.itemName + " | ";
        });
        EventManager.CallPopover(value);

        
    }

    /// <summary>
    /// 玩家武器跟随转向
    /// </summary>
    private void PlayerWeaponRotate()
    {
        //鼠标与玩家的方向向量
        Vector3 mouseDir = (mouseWorldPosition - transform.position).normalized;
        //计算改方向向量的弧度值 * 180/π -> 角度值
        float angle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;
        //偏移
        //angle -= 90f;
        //玩家武器旋转至鼠标位置
        _playerWeaponTrans.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    /// <summary>
    /// 世界鼠标位置
    /// </summary>
    private void MousePosition()
    {
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;
    }

    /// <summary>
    /// 玩家本体自转
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayerAutoRotation()
    {
        Vector3 currentEulerAngles;
        while (true)
        {
            if (_playerCharecter.weaponActive)
            {
                currentEulerAngles = _playerMainBodyTrans.eulerAngles;
                currentEulerAngles.y += 1f;
                if (currentEulerAngles.y > 360f)
                    currentEulerAngles.y = 0f;
            }
            else
            {
                currentEulerAngles = _playerMainBodyTrans.eulerAngles;
                if(currentEulerAngles.y != 0f)
                    currentEulerAngles.y = 0f;
            }
            _playerMainBodyTrans.eulerAngles = currentEulerAngles;
            yield return new WaitForSeconds(0.05f);
        }
    }

    /// <summary>
    /// 玩家基础攻击
    /// </summary>
    private void PlayerBaseAttack()
    {
        if (null == _playerCharecter.currentWeapon)
        {
            return;
        }
        _playerWeaponTrans.GetComponent<WeaponCharecter>().Attack();
    }
}
