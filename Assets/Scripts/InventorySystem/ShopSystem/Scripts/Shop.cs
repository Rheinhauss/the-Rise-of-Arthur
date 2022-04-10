using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    /// <summary>
    /// ��Ʒ - �۸�
    /// </summary>
    private Dictionary<Item, int> itemShopDict;

    public event EventHandler OnItemShopListChanged;

    public Shop()
    {
        itemShopDict = new Dictionary<Item, int>();
    }

    /// <summary>
    /// �̵����
    /// </summary>
    /// <param name="item"></param>
    /// <param name="price"></param>
    /// <returns></returns>
    public void AddItemShop(Item item,int price)
    {
        Item item1 = null;
        foreach (Item inventoryItem in itemShopDict.Keys)
        {
            //��濴�������
            if (inventoryItem.itemType == item.itemType && itemShopDict[inventoryItem] == price)
            {
                inventoryItem.amount += item.amount;
                item1 = inventoryItem;
                break;
            }
        }
        if (item1 == null)
        {
            itemShopDict.Add(item, price);
        }
        OnItemShopListChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// ��ȡ�̵�Ļ�Ʒ�嵥
    /// </summary>
    /// <returns></returns>
    public Dictionary<Item, int> GetItemShopDict()
    {
        return itemShopDict;
    }

    /// <summary>
    /// �̵귷������Ʒ -> Customer
    /// </summary>
    /// <param name="Customer"></param>
    /// <param name="item"></param>
    /// <param name="amount"></param>
    /// <returns>true��ʾ�ɹ�����false��ʾ����ʧ�ܣ����޸���Ʒ��������</returns>
    public bool BuyItem(Inventory Customer, Item item, int amount)
    {
        Item itemInInventory = null;
        foreach (Item inventoryItem in itemShopDict.Keys)
        {
            if (inventoryItem == item)
            {
                //������㣬����ʧ��
                if(inventoryItem.amount < amount)
                {
                    return false;
                }
                //��Ҳ��㣬����ʧ��
                if (Customer.GetMoneyAmout() < itemShopDict[inventoryItem] * amount)
                {
                    return false;
                }
                item = new Item(item);
                item.amount = amount;
                inventoryItem.amount -= amount;
                itemInInventory = inventoryItem;
                Customer.AddItem(item);
                Customer.MinusMoney(itemShopDict[inventoryItem] * amount);
                break;
            }
        }
        if (itemInInventory != null && itemInInventory.amount <= 0)
        {
            RemoveItem(itemInInventory, item.amount);
        }
        //�޸���Ʒ
        else if(itemInInventory == null)
        {
            return false;
        }
        OnItemShopListChanged?.Invoke(this, EventArgs.Empty);
        return true;
    }

    /// <summary>
    /// �̵�����ĳ��Ʒ
    /// </summary>
    /// <param name="item"></param>
    /// <param name="amount"></param>
    public bool RemoveItem(Item item, int amount)
    {
        Item itemInInventory = null;
        bool IsNull = false;
        foreach (Item inventoryItem in itemShopDict.Keys)
        {
            if (inventoryItem == item)
            {
                item = new Item(item);
                item.amount = amount;
                inventoryItem.amount -= amount;
                itemInInventory = inventoryItem;
                break;
            }
        }
        if (itemInInventory != null && itemInInventory.amount <= 0)
        {
            IsNull = itemShopDict.Remove(itemInInventory);
        }
        OnItemShopListChanged?.Invoke(this, EventArgs.Empty);
        return IsNull;
    }
}
