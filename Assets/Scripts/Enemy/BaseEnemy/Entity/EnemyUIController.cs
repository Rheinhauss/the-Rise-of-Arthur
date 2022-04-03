using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUIController : MonoBehaviour
{
    public UnitUI_HP _HP;

    private void Start()
    {
        _HP.Init();
    }
}
