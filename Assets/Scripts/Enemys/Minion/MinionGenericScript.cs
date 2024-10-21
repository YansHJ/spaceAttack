using System.Collections;
using UnityEngine;

public class MinionGenericScript : MonoBehaviour
{
    private GameObject _playerObj;

    [Header("攻击间隔/s")]
    public float enemyAttackInterval = 1f;
    [Header("敌人子弹预制件")]
    public GameObject enemyBulletPerFab;
    [Header("敌人子弹速度")]
    public float enemyBulletSpeed;

    private void Awake()
    {
        _playerObj = GameObject.FindGameObjectWithTag("Player");
        if (null == _playerObj)
        {
            Debug.Log("无法获取玩家");
        }
    }

    private void Start()
    {
        StartCoroutine(EnemyAttack());
    }

    private void EnemyFollowPlayer()
    {

    }

    /// <summary>
    /// 敌人攻击协程
    /// </summary>
    /// <returns></returns>
    IEnumerator EnemyAttack()
    {
        while(true)
        {
            Vector3 attackDir = ToPayersDir();
            InstantiateBullets(attackDir);
            yield return new WaitForSeconds(enemyAttackInterval);
        }
    }

    /// <summary>
    /// 计算到玩家的方向
    /// </summary>
    /// <returns></returns>
    private Vector3 ToPayersDir()
    {
        return (_playerObj.transform.position - transform.position).normalized;
    }

    /// <summary>
    /// 生成子弹实体并给一个力
    /// </summary>
    /// <param name="attackDir"></param>
    private void InstantiateBullets(Vector3 attackDir)
    {
        GameObject bullet = Instantiate(enemyBulletPerFab, transform.position, transform.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.linearVelocity = attackDir * enemyBulletSpeed;
    }
}
