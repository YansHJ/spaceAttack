using UnityEngine;

public class EnemyCharecter : MonoBehaviour
{
    [Header("敌人最大血量")]
    public int enemyMaxHealth;
    [Header("敌人当前血量")]
    public int enemyCurrentHealth;
    [Header("敌人速度")]
    public int enemySpeed;

    private void Awake()
    {
        enemyCurrentHealth = enemyMaxHealth;
    }

    private void Update()
    {
        if (enemyCurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
