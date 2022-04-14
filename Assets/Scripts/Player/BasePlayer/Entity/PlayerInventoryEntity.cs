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

    public void Init()
    {
        inventory = new Inventory();
        UI_Inventory.SetInventory(inventory);

        //���������ʰȡ������HP,MP,COIN
        inventory.OnItemListChanged += (sender,e) =>
        {
            Player.PlayerUIController.UpdateUI();
            Player.PlayerEquipEntity.UpdateEquip();
        };
        Player.PlayerUIController.UpdateUI();

        //itemWorldʰȡ�¼�
        UnitControllerComponent.inputComponent.BindInputAction(KeyCode.F, TouchItem, KeyCodeType.DOWN);

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
            //��ʾtext
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
            //����text
            itemWorld.textMeshPro.gameObject.SetActive(false);
            itemWorld = null;
        }
    }

    /// <summary>
    /// ����Item�Ļ�ȡ���ú���
    /// </summary>
    /// <param name="item"></param>
    public void HarvestItem(Item item)
    {
        //����ǻ��ң�����ȥ������ֱ�Ӽ������
        if (item.GetItemType() == ItemType.Coin)
        {
            playerMoneyEntity.AddMoney(item.amount);
        }
        //Ĭ��ʰȡ������
        else
        {
            inventory.AddItem(item);
        }
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        UnitControllerComponent.inputComponent.UnBindInputAction(KeyCode.X, TouchItem, KeyCodeType.DOWN);
    }

}
