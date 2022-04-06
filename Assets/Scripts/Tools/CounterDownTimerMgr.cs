using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterDownTimerMgr : MonoBehaviour
{
    public List<CountDownTimer> countDownTimers = new List<CountDownTimer>();
    public static CounterDownTimerMgr Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void FixedUpdate()
    {
        foreach(CountDownTimer countDownTimer in countDownTimers)
        {
            countDownTimer.UpdateCurrentTime();
        }
    }

    public void AddTimer(CountDownTimer countDownTimer)
    {
        countDownTimers.Add(countDownTimer);
    }

    public void RemoveTimer(CountDownTimer countDownTimer)
    {
        countDownTimers.Remove(countDownTimer);
    }

}
