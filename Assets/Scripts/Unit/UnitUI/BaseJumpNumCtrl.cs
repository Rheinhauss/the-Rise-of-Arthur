using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
public class BaseJumpNumCtrl
{
    [SerializeField] private Text text;
    private CountDownTimer CountDownTimer;

    public void Init()
    {
        CountDownTimer = new CountDownTimer(2);
        CountDownTimer.EndActions.Add(() =>
        {
            text.gameObject.SetActive(false);
            text.text = "0";
        });
    }

    public void SetShowTime(float time)
    {
        CountDownTimer.Reset(time, true);
    }

    public void JumpNum(int num)
    {
        text.gameObject.SetActive(true);
        text.GetComponent<DOTweenAnimation>().DORestart();
        num = (int.Parse(text.text) + num);
        if(num > 0)
        {
            text.text = "+" + num.ToString();
        }
        else
        {
            text.text = num.ToString();
        }
        CountDownTimer.Start();
    }
}
