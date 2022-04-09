using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Money
{
    /// <summary>
    /// Ǯ������
    /// </summary>
    public enum MoneyType
    {
        [LabelText("���")]
        GoldCoin,
        None
    }
    /// <summary>
    /// ����
    /// </summary>
    private int amount = 0;
    /// <summary>
    /// ��������
    /// </summary>
    private MoneyType moneyType = MoneyType.GoldCoin;
    /// <summary>
    /// Money�ı�ʱ�����¼�,��ֵ������
    /// </summary>
    public UnityEvent<int, MoneyType> OnMoneyChanged = new UnityEvent<int, MoneyType>();

    /// <summary>
    /// ���û�������
    /// </summary>
    /// <param name="type"></param>
    public void SetMoneyType(MoneyType type)
    {
        moneyType = type;
        OnMoneyChanged?.Invoke(amount, moneyType);
    }

    /// <summary>
    /// ��ȡ�˻��ҵ�����
    /// </summary>
    /// <returns></returns>
    public MoneyType GetMoneyType()
    {
        return moneyType;
    }

    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="amount"></param>
    public void AddMoney(int amount)
    {
        this.amount += amount;
        OnMoneyChanged?.Invoke(amount, moneyType);
    }

    /// <summary>
    /// ���Ҽ���
    /// </summary>
    /// <param name="amount"></param>
    public void MinusMoney(int amount)
    {
        this.amount = Mathf.Max(0, this.amount - amount);
        OnMoneyChanged?.Invoke(amount, moneyType);
    }

    /// <summary>
    /// ��ȡ��ǰ����ֵ
    /// </summary>
    /// <returns></returns>
    public int GetMoneyAmount()
    {
        return amount;
    }

    /// <summary>
    /// ��������Ϊamount
    /// </summary>
    /// <param name="amount"></param>
    public void Reset(int amount)
    {
        this.amount = Mathf.Max(0, amount);
        OnMoneyChanged?.Invoke(amount, moneyType);
    }

    /// <summary>
    /// ������0
    /// </summary>
    public void Reset0()
    {
        amount = 0;
        OnMoneyChanged?.Invoke(amount, moneyType);
    }

}
