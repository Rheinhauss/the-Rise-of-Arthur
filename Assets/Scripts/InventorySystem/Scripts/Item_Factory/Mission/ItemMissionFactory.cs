using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMissionFactory : ItemFactoryInterface
{
    public ItemType itemType { get; set; }

    public Item CreateItem(int amount)
    {
        Item item = new Item();
        item.itemType = itemType;
        item.amount = amount;
        item.CanDrop = item.CanUse = false;
        return item;
    }
}
