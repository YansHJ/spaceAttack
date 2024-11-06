using System;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusUIController : MonoBehaviour
{
    private PlayerCharecter _playerCharecter;

    private Weapon _currentWeapon;

    //玩家血量显示文本
    [SerializeField]
    private TextMeshProUGUI _playerHealthText;
    //玩家武器血量显示文本
    [SerializeField]
    private TextMeshProUGUI _WeaponHealthText;

    [SerializeField]
    private RectTransform _playerHealthBack;
    [SerializeField]
    private RectTransform _playerHealthFront;
    [SerializeField]
    private RectTransform _weaponHealthBack;
    [SerializeField]
    private RectTransform _weaponHealthFront;

    //血条宽度比例(一滴血 换算为 宽度 的比例)
    [Header("血条宽度比例(一滴血 换算为 宽度 的比例)")]
    [SerializeField]
    private int _healthWidthScale;
    //血条宽度偏移
    [Header("血条宽度偏移(左 <- 右)")]
    [SerializeField]
    private int _healthWidthOffset;

    private int _maxWidth = 500;

    private void OnEnable()
    {
        EventManager.EquipTheNextWeapon += OnEquipTheNextWeapon;
        EventManager.PlayerInitCompleted += OnPlayerInitCompleted;
    }

    private void OnDisable()
    {
        EventManager.EquipTheNextWeapon -= OnEquipTheNextWeapon;
        EventManager.PlayerInitCompleted -= OnPlayerInitCompleted;
    }

    private async void Start()
    {
       await InitPlayerInfoAsync();
    }

    /// <summary>
    /// 装备下一个武器事件
    /// </summary>
    /// <param name="currentWeapon"></param>
    private void OnEquipTheNextWeapon(GameObject currentWeapon)
    {
        _currentWeapon = currentWeapon.GetComponent<Weapon>();
        InitWeaponHealthImage();
    }

    /// <summary>
    /// 玩家初始化完成
    /// </summary>
    /// <param name="currentPlayer"></param>
    private void OnPlayerInitCompleted(GameObject currentPlayer)
    {
        _playerCharecter = currentPlayer.GetComponent<PlayerCharecter>();
        InitPlayerHealthImage();
    }

    private void Update()
    {
        //刷新血量文字
        RefreshHealthText();
        //刷新血量图形
        RefreshHealth();
        //检查武器血量是否需要展示
        CheckWeaponHealthDisPlay();
    }

    /// <summary>
    /// 初始化玩家血量
    /// </summary>
    private void InitPlayerHealthImage()
    {
        int width = _playerCharecter.playerMaxHealth * _healthWidthScale;
        if (width > _maxWidth)
        {
            width = _maxWidth;
        }
        _playerHealthBack.sizeDelta = new Vector2(width, _playerHealthBack.sizeDelta.y);
    }

    /// <summary>
    /// 初始化武器血量
    /// </summary>
    private void InitWeaponHealthImage()
    {
        int width = _currentWeapon.maxWeaponHealth * _healthWidthScale;
        if (width > _maxWidth)
        {
            width = _maxWidth;
        }
        _weaponHealthBack.sizeDelta = new Vector2(width, _weaponHealthBack.sizeDelta.y);
    }

    /// <summary>
    /// 刷新血量图形显示
    /// </summary>
    private void RefreshHealth()
    {
        if (_playerCharecter != null)
        {
            //当前血量比例
            float healthScale = _playerCharecter.playerCurrentHealth / (float) _playerCharecter.playerMaxHealth;
            //血量显示长度
            _playerHealthFront.sizeDelta = new Vector2((_playerHealthBack.sizeDelta.x * healthScale) - _healthWidthOffset, _playerHealthFront.sizeDelta.y);
        }
        if (_currentWeapon != null)
        {
            //当前血量比例
            float _weaponHealthScale = _playerCharecter.playerCurrentWeaponHealth / (float) _currentWeapon.maxWeaponHealth;
            //血量显示长度
            _weaponHealthFront.sizeDelta = new Vector2((_weaponHealthBack.sizeDelta.x * _weaponHealthScale) - _healthWidthOffset, _weaponHealthFront.sizeDelta.y);
        }
    }

    /// <summary>
    /// 刷新血量文字显示
    /// </summary>
    private void RefreshHealthText()
    {
        if (_playerCharecter != null)
        {
            _playerHealthText.text = _playerCharecter.playerCurrentHealth.ToString();
            _WeaponHealthText.text = _playerCharecter.playerCurrentWeaponHealth.ToString();
        }
    }

    /// <summary>
    /// 检查武器血量显示
    /// </summary>
    private void CheckWeaponHealthDisPlay()
    {
        if (_weaponHealthFront.sizeDelta.x <= 0)
        {
            _weaponHealthBack.gameObject.SetActive(false);
            _WeaponHealthText.gameObject.SetActive(false);
        } 
        else
        {
            _weaponHealthBack.gameObject.SetActive(true);
            _WeaponHealthText.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 初始化玩家信息
    /// </summary>
    /// <returns></returns>
    private async Task InitPlayerInfoAsync()
    {
        GameObject _player = await GameManager.Instance.TryGetPlayer();
        _playerCharecter = _player.GetComponent<PlayerCharecter>();
        _currentWeapon = _playerCharecter.currentWeapon.GetComponent<Weapon>();
        InitPlayerHealthImage();
        InitWeaponHealthImage();
    }

}
