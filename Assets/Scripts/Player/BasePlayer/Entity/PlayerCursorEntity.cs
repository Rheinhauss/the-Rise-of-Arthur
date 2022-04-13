using EGamePlay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursorEntity : Entity
{
    InputComponent input = UnitControllerComponent.inputComponent;
    PlayerRotateEntity PlayerRotateEntity = Player.Instance.PlayerRotateEntity;

    public static bool isEnabled = true;
    public void Init()
    {
        ////按住Z键显示鼠标指针
        //input.BindInputAction(KeyCode.Z,() => {
        //    CursorUnLock();
        //}, KeyCodeType.DOWN);
        //input.BindInputAction(KeyCode.Z, () => {
        //    CursorLock();
        //}, KeyCodeType.UP);
        //鼠标指针显示之后，鼠标左键点击屏幕隐藏指针
        input.BindInputAction(KeyCode.Mouse0, Execute, KeyCodeType.DOWN);
        //默认隐藏鼠标指针
        CursorLock();
    }

    private void Execute()
    {
        if (!isEnabled || Player.Instance.IsOpenInventory)
        {
            return;
        }
        CursorLock();
    }

    public static void CursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        PlayerRotateEntity.RotEnable = true;
    }

    public static void CursorUnLock()
    {
        Cursor.lockState = CursorLockMode.None;
        PlayerRotateEntity.RotEnable = false;
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        input.UnBindInputAction(KeyCode.Mouse0, Execute, KeyCodeType.DOWN);
    }
}
