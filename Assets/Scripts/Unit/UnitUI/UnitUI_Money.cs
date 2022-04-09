using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UnitUI_Money
{
    public Image Image;
    public TextMeshProUGUI NumText;
    public Money.MoneyType type;

    public void Init(Sprite sprite, int num, Money.MoneyType type)
    {
        Image.sprite = sprite;
        num = Mathf.Max(0, num);
        NumText.text = num.ToString();
        this.type = type;
    }

    public void UpdateNum(int num, Money.MoneyType type)
    {
        if (this.type != type)
            return;
        num = Mathf.Max(0, num);
        NumText.text = num.ToString();
    }
}
