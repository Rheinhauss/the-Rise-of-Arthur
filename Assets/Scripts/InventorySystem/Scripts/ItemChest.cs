using Sirenix.OdinInspector;
using EGamePlay;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemChest : MonoBehaviour
{
    [System.Serializable]
    public struct ItemObj
    {
        public ItemType ItemType;
        public int amount;
    }
    public struct Items
    {
        public Item Item;
        public int amount;
    }
    [LabelText("使用ItemType创建默认Item")]
    public bool IsUseItemTypeCreate = false;
    [HideIf("IsUseItemTypeCreate")]
    public Item item;
    [ShowIf("IsUseItemTypeCreate")]
    public List<ItemObj> ItemObjs;
    public List<Items> items;
    [ShowIf("IsUseItemTypeCreate")]
    [Range(1, 99)]
    public int amount = 1;
    public Player Player = Player.Instance;
    public HarvestInterface player;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void GetItem()
    {
        player = Player.PlayerInventoryEntity;
        HarvestItem(player);
    }
    public void HarvestItem(HarvestInterface player)
    {
        foreach(ItemObj itemObj in ItemObjs)
        {
            player.HarvestItem(Item_Factory.Instance.CreateItem(itemObj.ItemType, itemObj.amount));
        }
    }
}
