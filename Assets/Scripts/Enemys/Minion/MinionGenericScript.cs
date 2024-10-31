using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyCharecter))]
public class MinionGenericScript : MonoBehaviour
{
    private GameObject _playerObj;

    private EnemyCharecter _enemyCharecter;

    private Rigidbody2D _rigidbody;

    private GameObject _bulletsParent;

    //敌人当前状态
    private MinionEnemyState _minionEnemyState = MinionEnemyState.CloseToPlayer;
    //移动目的地
    private Vector3 _destination;
    //到达目的地
    private bool _arrived = true;

    [Header("攻击间隔/s")]
    public float enemyAttackInterval = 1f;
    [Header("敌人子弹预制件")]
    public GameObject enemyBulletPerFab;
    [Header("敌人子弹速度")]
    public float enemyBulletSpeed;
    [Header("距离玩家安全距离")]
    public float farFromPlayer = 10f;
    [Header("距离玩家距离偏移(活动范围)")]
    public float farFromPlayerOffset = 5f;
    [Header("无规则闪避范围左")]
    [Range(-10f, 0)]
    public float evasionRangeLeft;
    [Header("无规则闪避范围右")]
    [Range(0, 10f)]
    public float evasionRangeRight;

    private void Awake()
    {
        _bulletsParent = GameObject.FindGameObjectWithTag("BulletsParent");
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
        //敌人状态机
        switch(_minionEnemyState)
        {
            case MinionEnemyState.CloseToPlayer:
                CloseToPlayer();
                break;
            case MinionEnemyState.IrregularMove:
                IrregularMove();
                break;
        }
    }

    /// <summary>
    /// 靠近玩家
    /// </summary>
    private void CloseToPlayer()
    {
        //Debug.Log("进入靠近状态");
        //Debug.Log("当前距离玩家：" + (Vector3.Distance(_playerObj.transform.position, transform.position)));
        //靠近玩家
        _destination = _playerObj.transform.position;
        //移动
        _rigidbody.linearVelocity = _enemyCharecter.enemySpeed * Time.fixedDeltaTime * ToDestinationDir(_destination);
        //距离玩家达到指定距离范围
        if (Vector3.Distance(_playerObj.transform.position, transform.position) <= farFromPlayer)
        {
            //切换至无规则运动状态
            _minionEnemyState = MinionEnemyState.IrregularMove;
            //目的地切换为敌人自己
            _destination = transform.position;
        }
    }

    /// <summary>
    /// 无规则活动
    /// </summary>
    private void IrregularMove()
    {
        //Debug.Log("进入无规则状态");
        //Debug.Log("当前距离玩家：" + (Vector3.Distance(_playerObj.transform.position, transform.position)));
        //Debug.Log("当前距离目的地：" + (Vector3.Distance(transform.position, _destination)));
        //超过了安全距离范围
        if (Vector3.Distance(_playerObj.transform.position, transform.position) > (farFromPlayer + farFromPlayerOffset))
        {
            //切换至靠近状态
            _minionEnemyState = MinionEnemyState.CloseToPlayer;
            //切换到达状态
            _arrived = true;
        }
        //无规则运动
        if (_arrived)
        {
            //无规则运动目的地
            float evasionX = Random.Range(evasionRangeLeft, evasionRangeRight);
            float evasionY = Random.Range(evasionRangeLeft, evasionRangeRight);
            _destination = new Vector3(transform.position.x + evasionX, transform.position.y + evasionY);
            _arrived = false;
        }
        //移动
        _rigidbody.linearVelocity = _enemyCharecter.enemySpeed * Time.fixedDeltaTime * ToDestinationDir(_destination);
        //移动到目的地
        if (Vector3.Distance(transform.position, _destination) <= 1)
        {
            _arrived = true;
        }
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
        bullet.transform.parent = _bulletsParent.transform;
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.linearVelocity = attackDir * enemyBulletSpeed;
    }
}

/// <summary>
/// 低级敌人状态
/// </summary>
public enum MinionEnemyState {
    //贴近玩家
    CloseToPlayer,
    //无规则移动
    IrregularMove
}
