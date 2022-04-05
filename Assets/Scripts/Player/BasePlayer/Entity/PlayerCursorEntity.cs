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
        //��סZ����ʾ���ָ��
        input.BindInputAction(KeyCode.Z,() => {
            CursorUnLock();
        }, KeyCodeType.DOWN);
        input.BindInputAction(KeyCode.Z, () => {
            CursorLock();
        }, KeyCodeType.UP);
        //���ָ����ʾ֮�������������Ļ����ָ��
        input.BindInputAction(KeyCode.Mouse0, () => {
            if (!isEnabled)
            {
                return;
            }
            CursorLock();
        }, KeyCodeType.DOWN);
        //Ĭ���������ָ��
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
}
