using Sirenix.OdinInspector;
using EGamePlay;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemRemove : MonoBehaviour
{
    public ItemType type;
    [Range(1, 99)]
    public int amount = 1;
    private Player Player => Player.Instance;

    public void RemoveItem()
    {
        if(type == ItemType.Coin_A)
        {
            Player.PlayerMoneyEntity.MinusMoney(amount);
            return;
        }
        foreach(var value in Player.PlayerInventoryEntity.inventory.GetItemList())
        {
            if(type == value.itemType)
            {
                Player.PlayerInventoryEntity.inventory.RemoveItem(value, amount);
                break;
            }
        }
    }
}
