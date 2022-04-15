using EGamePlay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryEntity : Entity, HarvestInterface
{
    public Player Player = Player.Instance;

    public Inventory inventory;
    private PlayerMoneyEntity playerMoneyEntity => Player.PlayerMoneyEntity;

    public UI_Inventory UI_Inventory => Player.PlayerUIController.UI_Inventory;

    private InputEntity inputEntity;

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
        inputEntity = new InputEntity(KeyCode.F, KeyCodeType.DOWN);
        inputEntity.name = "PlayerInventoryEntity";
        //itemWorld拾取事件
        inputEntity.BindInputAction(TouchItem);

    }

    private void TouchItem()
    {
        //touching item
        if (itemWorld == null)
            return;
        HarvestItem(itemWorld.GetItem());
        itemWorld.DestroySelf();
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

    /// <summary>
    /// 所有Item的获取调用函数
    /// </summary>
    /// <param name="item"></param>
    public List<Item> HarvestItem(Item item)
    {
        //如果是货币，不进去背包，直接加入货币
        if (item.GetItemType() == ItemType.Coin)
        {
            playerMoneyEntity.AddMoney(item.amount);
            var list = new List<Item>();
            list.Add(item);
            return list;
        }
        //默认拾取进背包
        else
        {
            return inventory.AddItem(item);
        }
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        inputEntity.UnBindInputAction(TouchItem);
    }

}
