using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// 物品
/// </summary>
[System.Serializable]
public class Item
{
    public ItemType itemType;
    /// <summary>
    /// 是否能使用
    /// </summary>
    public bool CanUse = true;
    /// <summary>
    /// 是否能丢弃
    /// </summary>
    public bool CanDrop = false;
    public int amount;
    [SerializeField]
    public List<UnityEvent<Transform,Transform, Item>> useEvents;
    /// <summary>
    /// 可重复使用物品 -> 是否正在使用
    /// </summary>
    public bool IsUsing { get; set; }

    public Item()
    {
        amount = 0;
        IsUsing = false;
        useEvents = new List<UnityEvent<Transform, Transform, Item>>();
    }

    public Item(Item item)
    {
        IsUsing = false;
        this.itemType = item.itemType;
        this.amount = item.amount;
        useEvents = item.useEvents;
    }

    public Sprite GetSprite()
    {
        try
        {
            return ItemAssets.Instance.ItemSpriteDict[itemType];
        }
        catch
        {
            Debug.LogError(itemType);
            return null;
        }
    }

    public bool IsStackable()
    {
        switch (GetItemType())
        {
            default:
            case ItemType.Coin:
            case ItemType.HealthPotion:
            case ItemType.ManaPotion:
                return true;
            case ItemType.Head_Armor:
            case ItemType.Torso_Armor:
            case ItemType.Leg_Armor:
            case ItemType.Foot_Armor:
            case ItemType.Weapon:
                return false;
        }
    }

    private bool BetweenItemType(ItemType value, ItemType less, ItemType greater)
    {
        if(value <= greater && value >= less)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// 获取同一类物品的类型总名称
    /// </summary>
    /// <returns></returns>
    public ItemType GetItemType()
    {
        //头盔
        if(BetweenItemType(itemType, ItemType.Head_Armor, ItemType.Head_Armor_End))
        {
            return ItemType.Head_Armor;
        }
        //躯干盔甲
        else if (BetweenItemType(itemType, ItemType.Torso_Armor, ItemType.Torso_Armor_End))
        {
            return ItemType.Torso_Armor;
        }
        //腿部盔甲
        else if (BetweenItemType(itemType, ItemType.Leg_Armor, ItemType.Leg_Armor_End))
        {
            return ItemType.Leg_Armor;
        }
        //鞋子
        else if (BetweenItemType(itemType, ItemType.Foot_Armor, ItemType.Foot_Armor_End))
        {
            return ItemType.Foot_Armor;
        }
        //武器
        else if (BetweenItemType(itemType, ItemType.Weapon, ItemType.Weapon_End))
        {
            return ItemType.Weapon;
        }
        //小型HP药剂
        else if (BetweenItemType(itemType, ItemType.HealthPotion, ItemType.HealthPotion_End))
        {
            return ItemType.HealthPotion;
        }
        //小型MP药剂
        else if (BetweenItemType(itemType, ItemType.ManaPotion, ItemType.ManaPotion_End))
        {
            return ItemType.ManaPotion;
        }
        //货币
        else if (BetweenItemType(itemType, ItemType.Coin, ItemType.Coin_A))
        {
            return ItemType.Coin;
        }
        return ItemType.None;
    }

    public void Execute(Transform Creator, Transform Target)
    {
        foreach(UnityEvent<Transform,Transform, Item> unityEvent in useEvents)
        {
            unityEvent?.Invoke(Creator, Target, this);
        }
    }

}
