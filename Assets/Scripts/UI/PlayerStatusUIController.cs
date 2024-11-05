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
    [SerializeField]
    private int _healthWidthScale;
    //血条宽度偏移
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

    private void OnEquipTheNextWeapon(GameObject currentWeapon)
    {
        _currentWeapon = currentWeapon.GetComponent<Weapon>();
        InitWeaponHealthImage();
    }

    private void OnPlayerInitCompleted(GameObject currentPlayer)
    {
        _playerCharecter = currentPlayer.GetComponent<PlayerCharecter>();
        InitPlayerHealthImage();
    }

    private void Update()
    {
        RefreshHealthText();
        RefreshHealth();
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

    private void RefreshHealthText()
    {
        if (_playerCharecter != null)
        {
            _playerHealthText.text = _playerCharecter.playerCurrentHealth.ToString();
            _WeaponHealthText.text = _playerCharecter.playerCurrentWeaponHealth.ToString();
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
