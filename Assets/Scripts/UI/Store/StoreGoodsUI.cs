using System.Collections.Generic;
using UnityEngine;

public class StoreGoodsUI : MonoBehaviour
{
    public GameObject goodsDetailPrefab;

    public int storeGoodsCnt = 10;

    private List<GameObject> _goodsList = new();

    private void Start()
    {
        RefeashGoodsList();
    }

    private void RefeashGoodsList()
    {
        List<Item> itemList = ItemManager.Instance.TryGetItemsWithConditions(storeGoodsCnt, ItemType.Any, ItemQuality.Any);
        _goodsList.Clear();
        foreach(Item item in itemList)
        {
            GameObject goodsObject = Instantiate(goodsDetailPrefab, transform);
            InitGoodsInfo(goodsObject, item);
            _goodsList.Add(goodsObject);
        }
    }

    private void InitGoodsInfo(GameObject goodsObj, Item itemInfo)
    {
        if (goodsObj.TryGetComponent<StoreGoodsDetail>(out var goodsDetail))
        {
            goodsDetail.goodsNameText.text = itemInfo.itemName;
            goodsDetail.goodsTypeText.text = itemInfo.itemType.ToString();
            goodsDetail.goodsDescribe.text = itemInfo.itemDescription;
            goodsDetail.goodsIcon.sprite = itemInfo.itemIcon;
        }
    }
}
