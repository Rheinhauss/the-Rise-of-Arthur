using EGamePlay;
using EGamePlay.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    public UnitUI_HP _HP;
    public UnitUI_MP _MP;
    public UI_Inventory UI_Inventory;
    public UnitUI_Equip UI_Equip;
    public UnitUI_Money UI_Money;
    private Transform MainUICavas;
    private Transform UI;
    private Button CloseBtn;


    private void Awake()
    {
        MainUICavas = transform.Find("MainUICanvas");
        UI = MainUICavas.Find("UI_Inventory");
        CloseBtn = UI.Find("CloseBtn").GetComponent<Button>();
    }

    private void Start()
    {
        _HP.Init();

        UI_Inventory.gameObject.SetActive(false);
        UI_Inventory.SetPlayer(Player.Instance.transform);
        UnitControllerComponent.inputComponent.BindInputAction(KeyCode.P, OpenInventory, KeyCodeType.DOWN);
        UI_Money.Init(ItemAssets.Instance.ItemSpriteDict[ItemType.Coin_A], 0, Money.MoneyType.GoldCoin);
        CloseBtn.onClick.AddListener(() =>
        {
            UI_Inventory.gameObject.SetActive(!UI_Inventory.gameObject.activeSelf);
            Player.Instance.IsOpenInventory = false;
            Player.StartController();
        });
    }

    private void OpenInventory()
    {
        if (Player.Instance.IsShopping || !Player.Instance.CanOpenInventory)
            return;
        UI_Inventory.gameObject.SetActive(!UI_Inventory.gameObject.activeSelf);
        if (UI_Inventory.gameObject.activeSelf)
        {
            Player.Instance.IsOpenInventory = true;
        }
        else
        {
            Player.Instance.IsOpenInventory = false;
            Player.StartController();
        }
    }

    private void Update()
    {
        if (Player.Instance.IsOpenInventory)
        {
            Player.StopController();
        }
    }

    public void UpdateUI()
    {
        _HP.UpdateHPCount(null);
        foreach (Item item in UI_Inventory.GetInventory().GetItemList())
        {
            if (item.GetItemType() == ItemType.HealthPotion)
            {
                _HP.UpdateHPCount(item);
            }
        }
    }

    public void UpdateMoneyUI(int amount, Money.MoneyType moneyType)
    {
        UI_Money.UpdateNum(amount, moneyType);
    }

    private void OnDestroy()
    {
        UnitControllerComponent.inputComponent.UnBindInputAction(KeyCode.P, OpenInventory, KeyCodeType.DOWN);
    }
}
