using UnityEngine;

[System.Serializable]
public class Item
{

    public int itemId;

    public string itemName;

    public string itemDescription;

    public ItemType itemType;

    public ItemQuality itemQuality;

    public string[] itemTags;

    public Sprite itemIcon;

    //按需
    public BuffData buffData;

    public GameObject weaponObj;
}

public enum ItemType
{
    Any,BUFF,Weapon
}

public enum ItemQuality
{
    Any,White,Green,Blue
}
