using System.Collections.Generic;
using UnityEngine;

public class EnemyCharecter : MonoBehaviour
{
    [Header("敌人最大血量")]
    public int enemyMaxHealth;
    [Header("敌人当前血量")]
    public int enemyCurrentHealth;
    [Header("敌人速度")]
    public int enemySpeed;

    [Range(1f, 100f)]
    [SerializeField]
    private int moneyMax;

    [Range(1f, 100f)]
    [SerializeField]
    private int moneyMin;

    [SerializeField]
    private int currentMoney;

    //金币预制体
    [SerializeField]
    private GameObject moneyPerfab;

    private void Awake()
    {
        enemyCurrentHealth = enemyMaxHealth;
        //初始化money
        GenerateMoney();
    }

    private void Update()
    {
        if (enemyCurrentHealth <= 0)
        {
            EnemyDead();
        }
    }

    /// <summary>
    /// 生成money
    /// </summary>
    private void GenerateMoney()
    {
        currentMoney = Random.Range(moneyMin, moneyMax);
    }

    private void EnemyDead()
    {
        //生成金币
        List<GameObject> moneyList = new();
        moneyPerfab.GetComponent<Money>().SetMoneyCnt(currentMoney);
        moneyList.Add(moneyPerfab);
        InstancesManager.Instance.Instantiate(moneyList, transform.position);
        //销毁
        Destroy(gameObject);
    }
}
