using System.Collections.Generic;
using UnityEngine;

public class EnemyCharecter : Charecter
{
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
        currentHealth = maxHealth;
        currentSpeed = baseSpeed;
        //初始化money
        GenerateMoney();
    }

    private void Update()
    {
        if (currentHealth <= 0)
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

    public void EnemyDead()
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
