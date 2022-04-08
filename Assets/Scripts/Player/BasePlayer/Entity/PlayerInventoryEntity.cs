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

        //���������ʰȡ������HP,MP,COIN
        inventory.OnItemListChanged += (sender,e) =>
        {
            Player.PlayerUIController.UpdateUI();
            Player.PlayerEquipEntity.UpdateEquip();
        };
        Player.PlayerUIController.UpdateUI();


        //ʰȡ�¼�
        UnitControllerComponent.inputComponent.BindInputAction(KeyCode.X, () =>
        {
            //touching item
            if (itemWorld == null)
                return;
            //Ĭ��ʰȡ������
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

}
