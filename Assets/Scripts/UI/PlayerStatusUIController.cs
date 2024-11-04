using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStatusUIController : MonoBehaviour
{
    private PlayerCharecter _playerCharecter;

    [SerializeField]
    private TextMeshProUGUI _playerHealthText;

    [SerializeField]
    private TextMeshProUGUI _WeaponHealthText;

    private void Start()
    {
        //尝试获取玩家
        GameManager.Instance.TryGetPlayer((GameObject player) => { _playerCharecter = player.GetComponent<PlayerCharecter>(); });
    }

    private void Update()
    {
        if (_playerCharecter != null)
        {
            _playerHealthText.text = _playerCharecter.playerCurrentHealth.ToString();
            _WeaponHealthText.text = _playerCharecter.playerCurrentWeaponHealth.ToString();
        }
    }

}
