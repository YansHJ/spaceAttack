using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 怪物管理
/// </summary>
public class MonsterManager : Singleton<MonsterManager>
{
    [Header("怪物生成父物体")]
    public GameObject monsterParent;
    //当前状态
    private MonsterGenerateStates _currentGenerateStates;
    //当前怪物信息
    private List<GameLevelMonsterInfo> _currentMonsterInfos;
    //最大怪物生成数量
    private int _maxMonsterCnt;
    //当前怪物数量
    private int _currentMonsterCnt;
    //当前生成概率比例
    private float _currentProbabilityScale;

    private void OnEnable()
    {
        EventManager.MonsterGenerateStart += OnMonsterGenerateStart;
        EventManager.MonsterGenerateStop += OnMonsterGenerateStop;
    }

    private void OnDisable()
    {
        EventManager.MonsterGenerateStart -= OnMonsterGenerateStart;
        EventManager.MonsterGenerateStop -= OnMonsterGenerateStop;
    }

    private void Start()
    {
        _currentGenerateStates = MonsterGenerateStates.Stop;
        _maxMonsterCnt = 0;
        _currentMonsterCnt = 0;
        _currentProbabilityScale = 1f;
    }

    private void Update()
    {
        switch(_currentGenerateStates)
        {
            case MonsterGenerateStates.Stop :
                break;
            case MonsterGenerateStates.Start :
                StartGenerate();
                break;

        }
    }

    private void OnMonsterGenerateStop()
    {
        _currentGenerateStates = MonsterGenerateStates.Stop;
        StopGenerate();
    }

    private void OnMonsterGenerateStart(List<GameLevelMonsterInfo> monsterInfos)
    {
        _currentGenerateStates = MonsterGenerateStates.Start;
        _currentMonsterInfos = monsterInfos;
    }

    /// <summary>
    /// 开始生成
    /// </summary>
    private void StartGenerate()
    {
        if (_currentMonsterCnt <= 0)
        {
            CountMaxMonster();
            InitProbabilityScale();
        }
        GenerateMonster();
    }

    /// <summary>
    /// 停止生成
    /// </summary>
    private void StopGenerate()
    {
        _maxMonsterCnt = 0;
        _currentMonsterCnt = 0;
        _currentProbabilityScale = 1f;
        _currentMonsterInfos = null;
    }

    /// <summary>
    /// 计算当前生成怪物的最大数量
    /// </summary>
    private void CountMaxMonster()
    {
        _maxMonsterCnt = _currentMonsterInfos.Sum(m => m.max);
    }

    /// <summary>
    /// 生成怪物
    /// </summary>
    private void GenerateMonster()
    {
        float randomValue;
        float probabilityCumulative;
        while (_currentMonsterCnt <= _maxMonsterCnt)
        {
            randomValue = Random.Range(0.1f, 0.99f);
            probabilityCumulative = 0f;
            foreach (GameLevelMonsterInfo monsterInfo in _currentMonsterInfos)
            {
                probabilityCumulative += monsterInfo.refreshProbability;
                if (randomValue <= probabilityCumulative / _currentProbabilityScale)
                {
                    //生成
                    Debug.Log("生成了: " + monsterInfo.monsterPrefab.name);
                    //数量+1
                    _currentMonsterCnt++;
                }
            }
        }
    }

    /// <summary>
    /// 初始化生成概率比例
    /// </summary>
    private void InitProbabilityScale()
    {
        _currentProbabilityScale = _currentMonsterInfos.Sum(m => m.refreshProbability);
    }

}

/// <summary>
/// 怪物生成状态
/// </summary>
public enum MonsterGenerateStates
{
    Stop,Start
}
