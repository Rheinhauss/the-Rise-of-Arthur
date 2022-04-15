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

        //���������ʰȡ������HP,MP,COIN
        inventory.OnItemListChanged += (sender,e) =>
        {
            Player.PlayerUIController.UpdateUI();
            Player.PlayerEquipEntity.UpdateEquip();
        };
        Player.PlayerUIController.UpdateUI();
        inputEntity = new InputEntity(KeyCode.F, KeyCodeType.DOWN);
        inputEntity.name = "PlayerInventoryEntity";
        //itemWorldʰȡ�¼�
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
    public List<Item> HarvestItem(Item item)
    {
        //����ǻ��ң�����ȥ������ֱ�Ӽ������
        if (item.GetItemType() == ItemType.Coin)
        {
            playerMoneyEntity.AddMoney(item.amount);
            var list = new List<Item>();
            list.Add(item);
            return list;
        }
        //Ĭ��ʰȡ������
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
