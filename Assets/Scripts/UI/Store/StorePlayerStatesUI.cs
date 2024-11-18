using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class StorePlayerStatesUI : MonoBehaviour
{
    private PlayerCharecter _playerCharecter;

    public TextMeshProUGUI moneyCntText;

    public TextMeshProUGUI hpCntText;

    private async void OnEnable()
    {
        await TryGetPlayerCharecter();
    }

    private void Update()
    {
        RefreshPlayerStatesUI();
    }

    private void RefreshPlayerStatesUI()
    {
        if (null != _playerCharecter)
        {
            moneyCntText.text = _playerCharecter.playerMoneyCnt.ToString();
            hpCntText.text = _playerCharecter.maxHealth.ToString();
        }
    }

    private async Task TryGetPlayerCharecter()
    {
        GameObject _player = await GameManager.Instance.TryGetPlayer();
        _playerCharecter = _player.GetComponent<PlayerCharecter>();
    }
}
