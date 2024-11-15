using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public TotalItemDatas_SO totalBuffDatas;

    public TotalItemDatas_SO totalWeaponDatas;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    /// <summary>
    /// 按需查询物品
    /// </summary>
    /// <param name="cnt">数量</param>
    /// <param name="itemType">类型</param>
    /// <param name="quality">品质</param>
    /// <returns>物品列表</returns>
    public List<Item> TryGetItemsWithConditions(int cnt, ItemType itemType, ItemQuality quality)
    {
        List<Item> items = new();
        switch(itemType)
        {
            case ItemType.Weapon:
                items.AddRange(totalWeaponDatas.itmeList);
                break;
            case ItemType.BUFF:
                items.AddRange(totalBuffDatas.itmeList);
                break;
            default:
                items.AddRange(totalBuffDatas.itmeList);
                items.AddRange(totalWeaponDatas.itmeList);
                break;
        }
        if (quality != ItemQuality.Any)
        {
            items = items.FindAll(o => o.itemQuality == quality);
        }

        if (items.Count < cnt)
        {
            cnt = items.Count;
        }
        Debug.Log("所有物品数量：" + items.Count);
        List<int> itemOrders = new();
        for (int i = 0; i < cnt; i++)
        {
            int index = Random.Range(0, cnt);
            while (itemOrders.Contains(index))
            {
                index = Random.Range(0, cnt);
                
            }
            itemOrders.Add(index);
        }
        List<Item> currentItemResult = new();
        itemOrders.ForEach(o =>
        {
            currentItemResult.Add(items[o]);
        });
        Debug.Log("最后物品数量：" + currentItemResult.Count);
        return currentItemResult;
    }
}
