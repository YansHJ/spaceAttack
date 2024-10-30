using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 怪物管理
/// </summary>
public class MonsterManager : Singleton<MonsterManager>
{
    [Header("相机管理")]
    public CameraManager cameraManager;
    [Header("怪物生成父物体")]
    public GameObject monsterParent;
    //当前状态
    [SerializeField]
    private MonsterGenerateStates _currentGenerateStates = MonsterGenerateStates.Stop;
    //当前怪物信息
    [SerializeField]
    private List<GameLevelMonsterInfo> _currentMonsterInfos;
    //最大怪物生成数量
    [SerializeField]
    private int _maxMonsterCnt = 0;
    //当前怪物数量
    [SerializeField]
    private int _currentMonsterCnt = 0;
    //当前生成概率比例
    [SerializeField]
    private float _currentProbabilityScale = 1f;
    [SerializeField]
    public float _monsterGenerateInterval = 1f;

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

    private void Update()
    {
        //生成状态
        switch (_currentGenerateStates)
        {
            case MonsterGenerateStates.Stop:
                break;
            case MonsterGenerateStates.Start:
                StartGenerate();
                break;

        }
    }

    private void OnMonsterGenerateStop()
    {
        _currentGenerateStates = MonsterGenerateStates.Stop;
        StopCoroutine(GenerateMonster());
        StopGenerate();
    }

    private void OnMonsterGenerateStart(List<GameLevelMonsterInfo> monsterInfos, float generateInterval)
    {
        _currentGenerateStates = MonsterGenerateStates.Start;
        _currentMonsterInfos = monsterInfos;
        _monsterGenerateInterval = generateInterval;
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
            StartCoroutine(GenerateMonster());
        }
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
    private IEnumerator GenerateMonster()
    {
        // TODO 性能优化问题！！！！

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
                    GameObject monster = Instantiate(monsterInfo.monsterPrefab, MonsterGeneratePosition(), Quaternion.identity);
                    monster.transform.SetParent(monsterParent.transform, false);
                    //统计当前怪物数量
                    _currentMonsterCnt = monsterParent.transform.childCount;
                }
            }
            yield return new WaitForSeconds(_monsterGenerateInterval);
        }
    }

    /// <summary>
    /// 初始化生成概率比例
    /// </summary>
    private void InitProbabilityScale()
    {
        _currentProbabilityScale = _currentMonsterInfos.Sum(m => m.refreshProbability);
    }

    /// <summary>
    /// 怪物生成位置
    /// </summary>
    /// <returns></returns>
    private Vector2 MonsterGeneratePosition()
    {
        Vector2 topLeft = cameraManager.TopLeftPosition();
        Vector2 topRight = cameraManager.TopRightPosition();
        int randomValue = RandomValue(1, 4);
        return randomValue switch
        {
            1 => new Vector2(topLeft.x - 1, RandomValue(0, (int)topLeft.y)),
            2 => new Vector2(RandomValue(0, (int)topRight.x), topRight.y + 1),
            3 => new Vector2(topRight.x + 1, RandomValue(0, (int)topRight.y)),
            _ => new Vector2(0, 0),
        };
    }

    /// <summary>
    /// 获取指定区间的随机数
    /// </summary>
    /// <param name="left">左</param>
    /// <param name="right">右</param>
    /// <returns></returns>
    private int RandomValue(int left, int right)
    {
        return Random.Range(left, right);
    }

}

/// <summary>
/// 怪物生成状态
/// </summary>
public enum MonsterGenerateStates
{
    Stop,Start
}
