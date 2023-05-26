using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "CreateItem")]
public class Item : ScriptableObject
{
    public enum KindOfItem
    {
        Weapon,
        UseItem,
        Bag,
        ValuablesItem,
        Key,
        Material
    }

    [SerializeField]
    private KindOfItem kindOfItem;

    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private string itemName;

    [SerializeField]
    private string infomation;

    public KindOfItem GetKindOfItem()
    {
        return kindOfItem;
    }
    public Sprite GetIcon()
    {
        return icon;
    }
    public string GetItemName()
    {
        return itemName;
    }
    public string GetInfomation()
    {
        return infomation;
    }
}
