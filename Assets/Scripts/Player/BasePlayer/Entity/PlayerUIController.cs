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

    private void Start()
    {
        _HP.Init();

        UI_Inventory.gameObject.SetActive(false);
        UI_Inventory.SetPlayer(Player.Instance.transform);
        UnitControllerComponent.inputComponent.BindInputAction(KeyCode.P, () =>
        {
            UI_Inventory.gameObject.SetActive(!UI_Inventory.gameObject.activeSelf);
            if (UI_Inventory.gameObject.activeSelf)
            {
                Player.StopController();
            }
            else
            {
                Player.StartController();
            }
        }, KeyCodeType.DOWN);
        UI_Money.Init(ItemAssets.Instance.ItemSpriteDict[ItemType.Coin_A], 0, Money.MoneyType.GoldCoin);
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

}
