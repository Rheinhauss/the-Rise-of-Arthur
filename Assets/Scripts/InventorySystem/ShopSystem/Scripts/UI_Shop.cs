using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop : MonoBehaviour
{
    private Shop shop;
    private Transform itemShopSlotContainer;
    private Transform itemShopSlotTemplate;
    private ItemBuyList itemBuyList;
    private Button CloseBtn;
    private TextMeshProUGUI messageTip;

    public event EventHandler OnOpenShop;
    public event EventHandler OnCloseShop;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        itemShopSlotContainer = transform.Find("ItemShopSlotContainer");
        itemShopSlotTemplate = itemShopSlotContainer.Find("ItemShopSlotTemplate");
        itemBuyList = transform.Find("ItemBuyList").gameObject.AddComponent<ItemBuyList>();
        CloseBtn = transform.Find("CloseBtn").GetComponent<Button>();
        CloseBtn.onClick.AddListener(() =>
        {
            CloseShop();
        });
        messageTip = transform.Find("MessageTip").GetComponent<TextMeshProUGUI>();
        itemBuyList.SetMessageTip(messageTip);
        this.gameObject.SetActive(false);
    }

    public void OpenShop(Inventory customer)
    {
        this.gameObject.SetActive(true);
        Player.StopController();
        RefreshShopItems();
        SetCustomer(customer);
        OnOpenShop?.Invoke(this, EventArgs.Empty);
    }
    public void CloseShop()
    {
        this.gameObject.SetActive(false);
        Player.StartController();
        SetCustomer(null);
        OnCloseShop?.Invoke(this, EventArgs.Empty);
        itemBuyList.gameObject.SetActive(false);
    }

    public void SetShop(Shop shop)
    {
        if(this.shop != null)
            this.shop.OnItemShopListChanged -= Inventory_OnItemShopListChanged;
        this.shop = shop;
        shop.OnItemShopListChanged += Inventory_OnItemShopListChanged;
        RefreshShopItems();
    }


    public Shop GetShop()
    {
        return shop;
    }

    public void SetCustomer(Inventory Customer)
    {
        itemBuyList.Customer = Customer;
        RefreshShopItems();
    }

    public Inventory GetCustomer()
    {
        return itemBuyList.Customer;
    }

    private void Inventory_OnItemShopListChanged(object sender, EventArgs e)
    {
        RefreshShopItems();
    }

    private void RefreshShopItems()
    {
        foreach (Transform child in itemShopSlotContainer)
        {
            if (child == itemShopSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        Dictionary<Item, int> itemShops = shop.GetItemShopDict();
        //items.Reverse();
        foreach (Item item in itemShops.Keys)
        {
            RectTransform itemSlotRectTransform = Instantiate(itemShopSlotTemplate, itemShopSlotContainer) as RectTransform;
            itemSlotRectTransform.gameObject.SetActive(true);

            Button buyBtn = itemSlotRectTransform.Find("BuyBtn").GetComponent<Button>();
            buyBtn.onClick.AddListener(() =>
            {
                itemBuyList.gameObject.SetActive(true);
                itemBuyList.SetItem(item);
            });

            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            TextMeshProUGUI AmountText = itemSlotRectTransform.Find("AmountText").GetComponent<TextMeshProUGUI>();
            if (item.amount > 0)
            {
                AmountText.SetText(item.amount.ToString());
            }
            else
            {
                AmountText.SetText("");
            }
            TextMeshProUGUI PriceText = itemSlotRectTransform.Find("Price").GetComponent<TextMeshProUGUI>();
            PriceText.text = "price:" + itemShops[item];
        }

    }

}
