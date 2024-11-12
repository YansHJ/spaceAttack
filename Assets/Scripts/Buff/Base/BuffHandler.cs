using System.Collections.Generic;
using UnityEngine;

public class BuffHandler : MonoBehaviour
{
    public List<BuffInfo> activeBuffList = new List<BuffInfo>();

    private void Update()
    {
        BuffCheck();
    }

    private void BuffCheck()
    {
        List<BuffInfo> deleteBuffList = new();
        foreach (BuffInfo buffInfo in activeBuffList)
        {
            if (buffInfo.buffData.isPermanent)
            {
                continue;
            }
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

    public void AddBuff(BuffInfo buffInfo)
    {
        BuffInfo currentBuff = FindInActive(buffInfo);
        if (currentBuff != null)
        {
            if (currentBuff.currentStack < currentBuff.buffData.maxStack)
            {
                currentBuff.currentStack++;
                switch(currentBuff.buffData.updateTimeEnum)
                {
                    case BuffUpdateTimeEnum.Add:
                        currentBuff.durationTimer += currentBuff.buffData.duration;
                        break;
                    case BuffUpdateTimeEnum.Replace:
                        currentBuff.durationTimer = currentBuff.buffData.duration;
                        break;
                }
                currentBuff.buffData.OnCreate.Apply(currentBuff);
            }
        }
        else
        {
            buffInfo.durationTimer = buffInfo.buffData.duration;
            buffInfo.currentStack = 1;
            buffInfo.tickTimer = buffInfo.buffData.tickTime;
            buffInfo.buffData.OnCreate.Apply(buffInfo);
            activeBuffList.Add(buffInfo);
        }
    }

    public void RemoveBuff(BuffInfo buffInfo)
    {
        switch (buffInfo.buffData.removeStackUpdateEnum)
        {
            case BuffRemoveStackUpdateEnum.Clear:
                buffInfo.buffData.OnRemove.Apply(buffInfo);
                activeBuffList.Remove(buffInfo);
                break;
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
