using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Money
{
    /// <summary>
    /// 钱的类型
    /// </summary>
    public enum MoneyType
    {
        [LabelText("金币")]
        GoldCoin,
        None
    }
    /// <summary>
    /// 数额
    /// </summary>
    private int amount = 0;
    /// <summary>
    /// 货币类型
    /// </summary>
    private MoneyType moneyType = MoneyType.GoldCoin;
    /// <summary>
    /// Money改变时触发事件,数值和类型
    /// </summary>
    public UnityEvent<int, MoneyType> OnMoneyChanged = new UnityEvent<int, MoneyType>();

    /// <summary>
    /// 设置货币类型
    /// </summary>
    /// <param name="type"></param>
    public void SetMoneyType(MoneyType type)
    {
        moneyType = type;
        OnMoneyChanged?.Invoke(amount, moneyType);
    }

    /// <summary>
    /// 获取此货币的类型
    /// </summary>
    /// <returns></returns>
    public MoneyType GetMoneyType()
    {
        return moneyType;
    }

    /// <summary>
    /// 货币增加
    /// </summary>
    /// <param name="amount"></param>
    public void AddMoney(int amount)
    {
        this.amount += amount;
        OnMoneyChanged?.Invoke(amount, moneyType);
    }

    /// <summary>
    /// 货币减少
    /// </summary>
    /// <param name="amount"></param>
    public void MinusMoney(int amount)
    {
        this.amount = Mathf.Max(0, this.amount - amount);
        OnMoneyChanged?.Invoke(amount, moneyType);
    }

    /// <summary>
    /// 获取当前货币值
    /// </summary>
    /// <returns></returns>
    public int GetMoneyAmount()
    {
        return amount;
    }

    /// <summary>
    /// 货币重置为amount
    /// </summary>
    /// <param name="amount"></param>
    public void Reset(int amount)
    {
        this.amount = Mathf.Max(0, amount);
        OnMoneyChanged?.Invoke(amount, moneyType);
    }

    /// <summary>
    /// 货币清0
    /// </summary>
    public void Reset0()
    {
        amount = 0;
        OnMoneyChanged?.Invoke(amount, moneyType);
    }

}
