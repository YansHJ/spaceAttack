using UnityEngine;

public class SpeedIncreaceBuff : Buff
{
    private int _speedIncrease;

    private PlayerCharecter _playerCharecter;

    public SpeedIncreaceBuff(float buffDuration, GameObject targetObj, int _speedIncrease) 
        : base("Speed Increase", BuffType.buff, false, buffDuration, targetObj)
    {
        this._speedIncrease = _speedIncrease;
    }

    protected override void Apply()
    {
        if(targetObj.TryGetComponent(out _playerCharecter))
        {
            _playerCharecter.playerCurrentSpeed += _speedIncrease;
        }
    }

    protected override void Remove()
    {
        if (targetObj.TryGetComponent(out _playerCharecter))
        {
            _playerCharecter.playerCurrentSpeed -= _speedIncrease;
        }
    }
}
