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

    private void Start()
    {
        _HP.Init();
    }

}
