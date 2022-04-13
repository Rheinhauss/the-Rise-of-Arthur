using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> itemList;

    public event EventHandler OnItemListChanged;

    private Money money;

    public Inventory()
    {
        itemList = new List<Item>();
        money = new Money();
        money.SetMoneyType(Money.MoneyType.GoldCoin);
        money.Reset0();
    }

    public List<Item> AddItem(Item item)
    {
        List<Item> items = new List<Item>();
        Item item1 = null;
        if (item.IsStackable())
        {
            foreach(Item inventoryItem in itemList)
            {
                //库存看具体类别
                if(inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    item1 = inventoryItem;
                    items.Add(item1);
                    break;
                }
            }
            if(item1 == null)
            {
                itemList.Add(item);
                items.Add(item);
            }
        }
        else
        {
            for(int i = 0; i < item.amount; ++i)
            {
                Item newItem = new Item(item);
                newItem.amount = 1;
                //库存看具体类别
                itemList.Add(newItem);
                items.Add(newItem);
            }
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
        return items;
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }

    public Item RemoveItem(Item item, int amount)
    {
        if (item.IsStackable())
        {
            Item itemInInventory = null;
            foreach (Item inventoryItem in itemList)
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
                itemList.Remove(itemInInventory);
            }
        }
        else
        {
            itemList.Remove(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
        return item;
    }

    public bool UseItem(Item item, Transform Creator, Transform Target)
    {
        if(item == null || item.amount <= 0)
        {
            return false;
        }
        RemoveItem(item, 1);
        item.Execute(Creator, Target);
        return true;
    }

    public void AddMoney(int amount)
    {
        money.AddMoney(amount);
    }

    public void MinusMoney(int amount)
    {
        money.MinusMoney(amount);
    }

    public int GetMoneyAmout()
    {
        return money.GetMoneyAmount();
    }

    public Money GetMoney()
    {
        return money;
    }

    public void SetMoney(Money money)
    {
        this.money = money;
    }

}
