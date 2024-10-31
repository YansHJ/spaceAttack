using TMPro;
using UnityEngine;

public class GameLevelInfoController : MonoBehaviour
{
    public TextMeshProUGUI gameLevelNameText;

    private GameLevelBase_SO _currentLevel;

    private void Start()
    {
        _currentLevel = GameLevelManager.Instance._currentLevel;
        gameLevelNameText.text = _currentLevel.levelName;
    }
}
