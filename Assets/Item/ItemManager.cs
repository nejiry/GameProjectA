using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager: MonoBehaviour
{
    [SerializeField]
    private ItemDataBase itemDataBase;
    private Dictionary<Item, int> numOfItem = new Dictionary<Item, int>();
    void Start()
    {
        for(int i = 0; i < itemDataBase.GetItemLists().Count; i++)
        {
            numOfItem.Add(itemDataBase.GetItemLists()[i], i);
            Debug.Log(itemDataBase.GetItemLists()[i].GetItemName() + ": " + itemDataBase.GetItemLists()[i].GetInfomation());
        }
        Debug.Log(GetItem("Food").GetInfomation());
        Debug.Log(numOfItem[GetItem("Cloth")]);
    }

    public Item GetItem(string searchName)
    {
        return itemDataBase.GetItemLists().Find(itemName => itemName.GetItemName() == searchName);
    }


}
