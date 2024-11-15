using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TotalItemDatas", menuName = "Yans/Item/TotalItemDatas")]
public class TotalItemDatas_SO : ScriptableObject
{
    public string itemDataDescr;

    public List<Item> itmeList;
}
