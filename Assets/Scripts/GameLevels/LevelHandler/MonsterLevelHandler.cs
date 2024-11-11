using System.Collections;
using UnityEngine;

public class MonsterLevelHandler : BaseLevelHandler
{

    public override void LevelStart()
    {
        base.LevelStart();
        StartCoroutine(LevelStartEnumerator());
    }

    public override void LevelEnd()
    {
        base.LevelEnd();
        //停止怪物生成事件
        EventManager.CallMonsterGenerateStop();
    }

    IEnumerator LevelStartEnumerator()
    {
        //15s准备时间
        yield return new WaitForSeconds(15f);
        //开始生成怪物事件
        EventManager.CallMonsterGenerateStart(_currentLevel.monsterInfos, _currentLevel.monsterGenerateInterval);
    }
}
