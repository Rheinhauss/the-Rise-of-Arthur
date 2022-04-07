using EGamePlay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryEntity : Entity
{
    public Player Player = Player.Instance;

    public Inventory inventory;

    public UI_Inventory UI_Inventory => Player.PlayerUIController.UI_Inventory;

    public void Init()
    {
        inventory = new Inventory();
        UI_Inventory.SetInventory(inventory);
    }

    public void OperateItem(Collider collider)
    {
        ItemWorld itemWorld = collider.GetComponentInParent<ItemWorld>();
        if(itemWorld != null)
        {
            //touching item
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }
}
