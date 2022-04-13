using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// ��Ʒ
/// </summary>
[System.Serializable]
public class Item
{
    public ItemType itemType;
    /// <summary>
    /// �Ƿ���ʹ��
    /// </summary>
    public bool CanUse = true;
    /// <summary>
    /// �Ƿ��ܶ���
    /// </summary>
    public bool CanDrop = false;
    public int amount;
    [SerializeField]
    public List<UnityEvent<Transform,Transform, Item>> useEvents;
    /// <summary>
    /// ���ظ�ʹ����Ʒ -> �Ƿ�����ʹ��
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
    /// ��ȡͬһ����Ʒ������������
    /// </summary>
    /// <returns></returns>
    public ItemType GetItemType()
    {
        //ͷ��
        if(BetweenItemType(itemType, ItemType.Head_Armor, ItemType.Head_Armor_End))
        {
            return ItemType.Head_Armor;
        }
        //���ɿ���
        else if (BetweenItemType(itemType, ItemType.Torso_Armor, ItemType.Torso_Armor_End))
        {
            return ItemType.Torso_Armor;
        }
        //�Ȳ�����
        else if (BetweenItemType(itemType, ItemType.Leg_Armor, ItemType.Leg_Armor_End))
        {
            return ItemType.Leg_Armor;
        }
        //Ь��
        else if (BetweenItemType(itemType, ItemType.Foot_Armor, ItemType.Foot_Armor_End))
        {
            return ItemType.Foot_Armor;
        }
        //����
        else if (BetweenItemType(itemType, ItemType.Weapon, ItemType.Weapon_End))
        {
            return ItemType.Weapon;
        }
        //С��HPҩ��
        else if (BetweenItemType(itemType, ItemType.HealthPotion, ItemType.HealthPotion_End))
        {
            return ItemType.HealthPotion;
        }
        //С��MPҩ��
        else if (BetweenItemType(itemType, ItemType.ManaPotion, ItemType.ManaPotion_End))
        {
            return ItemType.ManaPotion;
        }
        //����
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
