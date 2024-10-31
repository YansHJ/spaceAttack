using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameLevelInfoController : MonoBehaviour
{
    public TextMeshProUGUI gameLevelNameText;

    public TextMeshProUGUI levelTimerText;

    private GameLevelBase_SO _currentLevel;

    private void Start()
    {
        _currentLevel = GameLevelManager.Instance._currentLevel;
        gameLevelNameText.text = _currentLevel.levelName;

        Color levelNameTextColor = new(1, 1, 1, 0);
        Color levelTimerTextColor = new(1, 1, 1, 1);
        gameLevelNameText.DOColor(levelNameTextColor, 5f);
        levelTimerText.DOColor(levelTimerTextColor, 5f);
    }

    private void Update()
    {
        levelTimerText.text = GameLevelManager.Instance.CurrentLevelTimer().ToString();
    }
}
