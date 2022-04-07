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
    public UI_Inventory UI_Inventory;

    private void Start()
    {
        _HP.Init();
        UI_Inventory.gameObject.SetActive(false);
        UnitControllerComponent.inputComponent.BindInputAction(KeyCode.P, () =>
        {
            UI_Inventory.gameObject.SetActive(!UI_Inventory.gameObject.activeSelf);
        }, KeyCodeType.DOWN);
    }

}
