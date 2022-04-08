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

        //部分特殊的拾取，比如HP,MP,COIN
        inventory.OnItemListChanged += (sender,e) =>
        {
            Player.PlayerUIController.UpdateUI();
            Player.PlayerEquipEntity.UpdateEquip();
        };
        Player.PlayerUIController.UpdateUI();


        //拾取事件
        UnitControllerComponent.inputComponent.BindInputAction(KeyCode.X, () =>
        {
            //touching item
            if (itemWorld == null)
                return;
            //默认拾取进背包
            List<Item> items = inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }, KeyCodeType.DOWN);

    }
    private ItemWorld itemWorld = null;
    public void OperateItem(Collider collider)
    {
        if (itemWorld != null)
            return;
        itemWorld = collider.GetComponentInParent<ItemWorld>();
        if(itemWorld != null)
        {
            //显示text
            itemWorld.textMeshPro.gameObject.SetActive(true);
        }
    }

    public void OperateItemExit(Collider collider)
    {
        if (itemWorld == null)
            return;
        ItemWorld itemWorld1 = collider.GetComponentInParent<ItemWorld>();
        if (itemWorld == itemWorld1)
        {
            //隐藏text
            itemWorld.textMeshPro.gameObject.SetActive(false);
            itemWorld = null;
        }
    }

}
