using EGamePlay.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BaseSliderCtrl
{
    [SerializeField] private Slider Slider;
    private List<Action<float>> actions = new List<Action<float>>();
    public void UpdateValue(float value)
    {
        foreach(var action in actions)
        {
            action?.Invoke(value);
        }

        if (value >= Slider.maxValue)
        {
            value = Slider.maxValue;
        }
        else if (value <= Slider.minValue)
        {
            value = Slider.minValue;
        }
        Slider.value = value;
    }

    public void AddSliderAction(Action<float> action)
    {
        actions.Add(action);
    }

    public void RemoveSliderAction(Action<float> action)
    {
        actions.Remove(action);
    }

}
