using TMPro;
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
        _playerCharecter = GameManager.Instance.player.GetComponent<PlayerCharecter>();
    }

    private void Update()
    {
        _playerHealthText.text = _playerCharecter.playerCurrentHealth.ToString();
        _WeaponHealthText.text = _playerCharecter.playerCurrentWeaponHealth.ToString();
    }
}
