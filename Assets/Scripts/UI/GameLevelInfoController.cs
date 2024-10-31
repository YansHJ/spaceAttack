using DG.Tweening;
using System.Collections;
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
        StartCoroutine(LevelInfoShow());
    }

    private void Update()
    {
        levelTimerText.text = GameLevelManager.Instance.CurrentLevelTimer().ToString();
    }
    IEnumerator LevelInfoShow()
    {
        Color disappearColor = new(1, 1, 1, 0);
        Color appearColor = new(1, 1, 1, 1);
        yield return new WaitForSeconds(2f);
        gameLevelNameText.DOColor(appearColor, 2f);
        yield return new WaitForSeconds(2f);
        gameLevelNameText.DOColor(disappearColor, 2f);
        yield return new WaitForSeconds(3f);
        levelTimerText.DOColor(appearColor, 2f);
    }
}
