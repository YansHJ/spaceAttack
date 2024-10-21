using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyCharecter))]
public class MinionGenericScript : MonoBehaviour
{
    private GameObject _playerObj;

    private EnemyCharecter _enemyCharecter;

    private Rigidbody2D _rigidbody;

    //移动目的地
    private Vector3 _destination;
    //到达目的地
    private bool _arrived = false;

    [Header("攻击间隔/s")]
    public float enemyAttackInterval = 1f;
    [Header("敌人子弹预制件")]
    public GameObject enemyBulletPerFab;
    [Header("敌人子弹速度")]
    public float enemyBulletSpeed;
    [Header("距离玩家安全距离")]
    public float farFromPlayer = 10f;
    [Header("无规则闪避范围左")]
    [Range(-10f, 0)]
    public float evasionRangeLeft;
    [Header("无规则闪避范围右")]
    [Range(0, 10f)]
    public float evasionRangeRight;

    private void Awake()
    {
        _playerObj = GameObject.FindGameObjectWithTag("Player");
        if (null == _playerObj)
        {
            Debug.Log("无法获取玩家");
        }
        _enemyCharecter = transform.GetComponent<EnemyCharecter>();
        _rigidbody = transform.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(EnemyAttack());
    }

    private void FixedUpdate()
    {
        EnemyFollowPlayer();
    }

    /// <summary>
    /// 敌人跟随策略
    /// </summary>
    private void EnemyFollowPlayer()
    {
        if (Vector3.Distance(_playerObj.transform.position, transform.position) >= 10)
        {
            //靠近玩家
            _destination = _playerObj.transform.position;
        }
        else if (_arrived)
        {
            //无规则闪避
            float evasionX = Random.Range(evasionRangeLeft, evasionRangeRight);
            float evasionY = Random.Range(evasionRangeLeft, evasionRangeRight);
            _destination = new Vector3(transform.position.x + evasionX, transform.position.y + evasionY);
            _arrived = false;
        }
        else
        {
            //TODO 待完善
        }
        if (Vector3.Distance(transform.position, _destination) <= 1)
        {
            _arrived = true;
        }
        //移动
        _rigidbody.linearVelocity = _enemyCharecter.enemySpeed * Time.fixedDeltaTime * ToDestinationDir(_destination);
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
    /// 计算到目的地的方向
    /// </summary>
    /// <param name="destination"></param>
    /// <returns></returns>
    private Vector3 ToDestinationDir(Vector3 destination)
    {
        return (destination - transform.position).normalized;
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
