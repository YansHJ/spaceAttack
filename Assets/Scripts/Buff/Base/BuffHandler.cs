using System.Collections.Generic;
using UnityEngine;

public class BuffHandler : MonoBehaviour
{
    [Header("当前生效的BUFF集合")]
    public List<BuffInfo> activeBuffList = new List<BuffInfo>();

    private void Update()
    {
        //Buff状态检测
        BuffCheck();
    }

    /// <summary>
    /// Buff状态检测
    /// </summary>
    private void BuffCheck()
    {
        //待删除Buff集合
        List<BuffInfo> deleteBuffList = new();
        //遍历所有的Buff
        foreach (BuffInfo buffInfo in activeBuffList)
        {
            //计算触发间隔
            if (buffInfo.buffData.OnTick != null)
            {
                if (buffInfo.tickTimer < 0)
                {
                    buffInfo.buffData.OnTick.Apply(buffInfo);
                    buffInfo.tickTimer = buffInfo.buffData.tickTime;
                }
                else
                {
                    buffInfo.tickTimer -= Time.deltaTime;
                }
            }
            //永久的则不进行删除
            if (buffInfo.buffData.isPermanent)
            {
                continue;
            }
            //计算Buff过期
            if (buffInfo.durationTimer <= 0)
            {
                deleteBuffList.Add(buffInfo);
            }
            else
            {
                buffInfo.durationTimer -= Time.deltaTime;
            }
        }
        //删除所有过期的buff
        deleteBuffList.ForEach(o =>
        {
            activeBuffList.Remove(o);
        });
    }

    /// <summary>
    /// 添加BUFF
    /// </summary>
    /// <param name="buffInfo">Buff信息</param>
    public void AddBuff(BuffInfo buffInfo)
    {
        //查询存在的Buff
        BuffInfo currentBuff = FindInActive(buffInfo);
        //刷新Buff层数和时间
        if (currentBuff != null)
        {
            if (currentBuff.currentStack < currentBuff.buffData.maxStack)
            {
                currentBuff.currentStack++;
                switch(currentBuff.buffData.updateTimeEnum)
                {
                    //叠加时间
                    case BuffUpdateTimeEnum.Add:
                        currentBuff.durationTimer += currentBuff.buffData.duration;
                        break;
                    //时间刷新
                    case BuffUpdateTimeEnum.Replace:
                        currentBuff.durationTimer = currentBuff.buffData.duration;
                        break;
                }
                currentBuff.buffData.OnCreate.Apply(currentBuff);
            }
        }
        else
        {
            //添加新Buff
            buffInfo.durationTimer = buffInfo.buffData.duration;
            buffInfo.currentStack = 1;
            buffInfo.tickTimer = buffInfo.buffData.tickTime;
            buffInfo.buffData.OnCreate.Apply(buffInfo);
            activeBuffList.Add(buffInfo);
        }
    }

    /// <summary>
    /// 移除buff
    /// </summary>
    /// <param name="buffInfo"></param>
    public void RemoveBuff(BuffInfo buffInfo)
    {
        switch (buffInfo.buffData.removeStackUpdateEnum)
        {
            //清除Buff
            case BuffRemoveStackUpdateEnum.Clear:
                buffInfo.buffData.OnRemove.Apply(buffInfo);
                activeBuffList.Remove(buffInfo);
                break;
            //Buff减层数
            case BuffRemoveStackUpdateEnum.Reduce:
                buffInfo.currentStack--;
                if (buffInfo.currentStack == 0)
                {
                    buffInfo.buffData.OnRemove.Apply(buffInfo);
                    activeBuffList.Remove(buffInfo);
                }
                else
                {
                    buffInfo.durationTimer = buffInfo.buffData.duration;
                }
                break;
        }
    }

    /// <summary>
    /// 查找对应Buff
    /// </summary>
    /// <param name="buffInfo"></param>
    /// <returns></returns>
    private BuffInfo FindInActive(BuffInfo buffInfo)
    {
        foreach (BuffInfo activeBuff in activeBuffList)
        {
            if (activeBuff.buffData.buffId == buffInfo.buffData.buffId)
            {
                return activeBuff;
            }
        }
        return default;
    }
}
