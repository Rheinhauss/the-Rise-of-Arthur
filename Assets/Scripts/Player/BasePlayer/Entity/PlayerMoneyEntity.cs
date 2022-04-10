using EGamePlay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoneyEntity : Entity 
{
    public Player Player = Player.Instance;
    private Money money => Player.PlayerInventoryEntity.inventory.GetMoney();
    private PlayerUIController PlayerUIController => Player.PlayerUIController;


    public void Init()
    {
        money.SetMoneyType(Money.MoneyType.GoldCoin);
        money.Reset0();
        money.OnMoneyChanged.AddListener(PlayerUIController.UpdateMoneyUI);
    }

    public void AddMoney(int amount)
    {
        money.AddMoney(amount);
    }

    public void MinusMoney(int amount)
    {
        money.MinusMoney(amount);
    }

    public int GetMoneyAmout()
    {
        return money.GetMoneyAmount();
    }

}
