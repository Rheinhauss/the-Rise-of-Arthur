using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private Transform Player;

    private void Awake()
    {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    public Inventory GetInventory()
    {
        return inventory;
    }

    public void SetPlayer(Transform player)
    {
        this.Player = player;
    }

    private void Inventory_OnItemListChanged(object sender, EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        foreach(Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        List<Item> items = inventory.GetItemList();
        //items.Reverse();
        foreach (Item item in items)
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer) as RectTransform;
            itemSlotRectTransform.gameObject.SetActive(true);

            Transform Menu = itemSlotRectTransform.Find("Menu");
            Button_UI button = itemSlotRectTransform.GetComponent<Button_UI>();
            button.rightClick.AddListener(() =>
            {
                Menu.gameObject.SetActive(true);
            });
            button.leaveEvent.AddListener(() =>
            {
                Menu.gameObject.SetActive(false);
            });
            //Use Item
            Button_UI useBtn = Menu.GetChild(0).GetComponent<Button_UI>();
            if (item.CanUse)
            {
                useBtn.leftClick.AddListener(() =>
                {
                    //使用者->自己，作用者->自己
                    inventory.UseItem(item, Player, Player);
                });
            }
            else
            {
                useBtn.enabled = false;
                useBtn.GetComponent<Image>().color = Color.gray;
            }

            //Drop Item
            Button_UI dropBtn = Menu.GetChild(1).GetComponent<Button_UI>();
            if (item.CanDrop)
            {
                dropBtn.leftClick.AddListener(() =>
                {
                    ItemWorld.DropItem(this.Player.position, inventory.RemoveItem(item, item.amount));
                });
            }
            else
            {
                dropBtn.enabled = false;
                dropBtn.GetComponent<Image>().color = Color.gray;
            }


            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            TextMeshProUGUI uiText = itemSlotRectTransform.Find("AmountText").GetComponent<TextMeshProUGUI>();
            if(item.amount > 1)
            {
                uiText.SetText(item.amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }
        }

    }

}
