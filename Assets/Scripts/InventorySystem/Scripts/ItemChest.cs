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
    [LabelText("使用ItemType创建默认Item")]
    public bool IsUseItemTypeCreate = false;
    [HideIf("IsUseItemTypeCreate")]
    public Item item;
    [ShowIf("IsUseItemTypeCreate")]
    public List<ItemObj> ItemObjs;
    [ShowIf("IsUseItemTypeCreate")]
    [Range(1, 99)]
    public int amount = 1;
    public Player Player = Player.Instance;
    //private Transform Player0 = Player.Instance.transform;
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
    //public void UseItem()
    //{
    //    player = Player.PlayerInventoryEntity;
    //    HarvestItem(player);
    //    //使用者->自己，作用者->自己
    //    inventory.UseItem(item, Player0, Player0);
    //}
}
