using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseShopEntity : MonoBehaviour
{
    [System.Serializable]
    public class ItemShop
    {
        public ItemType ItemType;
        public int amount;
        public int price;
    }
    /// <summary>
    /// 消息提示
    /// </summary>
    [SerializeField] private TextMeshProUGUI messageTip;
    [SerializeField] private UI_Shop UI_Shop;
    /// <summary>
    /// 拥有的货品
    /// </summary>
    [SerializeField] private List<ItemShop> itemShops;

    private Shop shop;


    private Inventory customer;

    private void Awake()
    {
        UI_Shop.gameObject.SetActive(true);
        UI_Shop.OnCloseShop += UI_Shop_OnCloseShop;
        UI_Shop.OnOpenShop += UI_Shop_OnOpenShop;
        shop = new Shop();
        UI_Shop.SetShop(shop);
    }

    private void Start()
    {
        foreach (ItemShop itemShop in itemShops)
        {
            UI_Shop.GetShop().AddItemShop(Item_Factory.Instance.CreateItem(itemShop.ItemType, itemShop.amount), itemShop.price);
        }
    }
    private void UI_Shop_OnOpenShop(object sender, System.EventArgs e)
    {
        Player.Instance.IsShopping = true;
    }

    private void UI_Shop_OnCloseShop(object sender, System.EventArgs e)
    {
        Player.Instance.IsShopping = false;
    }
    /// <summary>
    /// 进货
    /// </summary>
    /// <param name="itemShop"></param>
    public void AddItemShop(ItemShop itemShop)
    {
        bool IsFind = false;
        foreach(ItemShop value in itemShops)
        {
            if(value.ItemType == itemShop.ItemType && value.price == itemShop.price)
            {
                IsFind = true;
                value.amount += itemShop.amount;
                var dict = UI_Shop.GetShop().GetItemShopDict();
                foreach (Item item in dict.Keys)
                {
                    if(item.itemType == itemShop.ItemType && dict[item] == itemShop.price)
                    {
                        item.amount += itemShop.amount;
                        break;
                    }
                }
                break;
            }
        }
        if (!IsFind)
        {
            itemShops.Add(itemShop);
            Item item = Item_Factory.Instance.CreateItem(itemShop.ItemType, itemShop.amount);
            UI_Shop.GetShop().AddItemShop(item, itemShop.price);
        }
    }

    /// <summary>
    /// 售空
    /// </summary>
    /// <param name="itemShop"></param>
    public void RemoveItemShop(ItemShop itemShop)
    {
        foreach (ItemShop value in itemShops)
        {
            if (value.ItemType == itemShop.ItemType && value.price == itemShop.price)
            {
                itemShops.Remove(value);
                var dict = UI_Shop.GetShop().GetItemShopDict();
                foreach (Item item in dict.Keys)
                {
                    if (item.itemType == itemShop.ItemType && dict[item] == itemShop.price)
                    {
                        dict.Remove(item);
                        break;
                    }
                }
                break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            UnitControllerComponent.inputComponent.BindInputAction(KeyCode.I, OpenShop, KeyCodeType.DOWN);
            messageTip.gameObject.SetActive(true);
            customer = other.GetComponent<Player>().PlayerInventoryEntity.inventory;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            UnitControllerComponent.inputComponent.UnBindInputAction(KeyCode.I, OpenShop, KeyCodeType.DOWN);
            messageTip.gameObject.SetActive(false);
            customer = null;
        }
    }

    private void OpenShop()
    {
        if (Player.Instance.IsOpenInventory)
            return;
        UI_Shop.OpenShop(customer);
    }

}
