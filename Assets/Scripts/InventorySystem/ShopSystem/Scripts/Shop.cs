using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    /// <summary>
    /// 商品 - 价格
    /// </summary>
    private Dictionary<Item, int> itemShopDict;

    public event EventHandler OnItemShopListChanged;

    public Shop()
    {
        itemShopDict = new Dictionary<Item, int>();
    }

    /// <summary>
    /// 商店进货
    /// </summary>
    /// <param name="item"></param>
    /// <param name="price"></param>
    /// <returns></returns>
    public void AddItemShop(Item item,int price)
    {
        Item item1 = null;
        foreach (Item inventoryItem in itemShopDict.Keys)
        {
            //库存看具体类别
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
    /// 获取商店的货品清单
    /// </summary>
    /// <returns></returns>
    public Dictionary<Item, int> GetItemShopDict()
    {
        return itemShopDict;
    }

    /// <summary>
    /// 商店贩卖出商品 -> Customer
    /// </summary>
    /// <param name="Customer"></param>
    /// <param name="item"></param>
    /// <param name="amount"></param>
    /// <returns>true表示成功购买，false表示购买失败，即无该商品或存货不足</returns>
    public bool BuyItem(Inventory Customer, Item item, int amount)
    {
        Item itemInInventory = null;
        foreach (Item inventoryItem in itemShopDict.Keys)
        {
            if (inventoryItem == item)
            {
                //存货不足，购买失败
                if(inventoryItem.amount < amount)
                {
                    return false;
                }
                //金币不足，购买失败
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
        //无该商品
        else if(itemInInventory == null)
        {
            return false;
        }
        OnItemShopListChanged?.Invoke(this, EventArgs.Empty);
        return true;
    }

    /// <summary>
    /// 商店舍弃某商品
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
