using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UnitUI_HP : BaseSliderCtrl
{
    //[SerializeField] private BaseJumpNumCtrl DamageText;
    //[SerializeField] private BaseJumpNumCtrl CureText;
    [SerializeField] private Image HPCount_Image;
    [SerializeField] private TextMeshProUGUI HPCount_Text;
    public void Init()
    {
    }

    public void UpdateHPCount(Item item)
    {
        if(item == null)
        {
            HPCount_Text.text = "0";
            HPCount_Image.sprite = ItemAssets.Instance.ItemSpriteDict[ItemType.HealthPotion_Little];
            return;
        }
        HPCount_Image.sprite = item.GetSprite();
        HPCount_Text.text = item.amount.ToString();
    }


    public void OnReceiveCure(float sliderCurValue, float cureValue)
    {
        UpdateValue(sliderCurValue);
        //CureText.JumpNum(Mathf.RoundToInt(cureValue));
    }

    public void OnReceiveDamage(float sliderCurValue, float dmgValue)
    {
        UpdateValue(sliderCurValue);
        //DamageText.JumpNum(Mathf.RoundToInt(-dmgValue));
    }
}
