using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UnitUI_HP : BaseSliderCtrl
{
    [SerializeField] private BaseJumpNumCtrl DamageText;
    [SerializeField] private BaseJumpNumCtrl CureText;

    public void Init()
    {
        DamageText.Init();
        CureText.Init();
    }


    public void OnReceiveCure(float sliderCurValue, float cureValue)
    {
        UpdateValue(sliderCurValue);
        CureText.JumpNum(Mathf.RoundToInt(cureValue));
    }

    public void OnReceiveDamage(float sliderCurValue, float dmgValue)
    {
        UpdateValue(sliderCurValue);
        DamageText.JumpNum(Mathf.RoundToInt(-dmgValue));
    }
}
