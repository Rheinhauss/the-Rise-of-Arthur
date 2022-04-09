using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCoin_Factory : ItemFactoryInterface
{
    public ItemType itemType { get; set; }

    public ItemCoin_Factory(ItemType type)
    {
        itemType = type;
    }
    public Item CreateItem(int amount)
    {
        Item item = new Item();
        item.itemType = itemType;
        item.amount = amount;
        return item;
    }
}
