using EGamePlay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotateEntity : Entity
{
    private Player Player => Player.Instance;
    public Transform CameraTransform => Camera.main.transform;
    public Transform CameraCenterPoint => Player.transform.GetChild(2);

    public float rotSpeedX { get; private set; }
    public float rotSpeedY { get; private set; }

    public static bool RotEnable { get; set; }

    public float radius { get; set; }

    private float angleX;
    private InputEntity inputEntity;

    public void Init()
    {
        rotSpeedX = 200;
        rotSpeedY = 100.0f;
        radius = 1.2f;
        RotEnable = true;
        angleX = CameraTransform.eulerAngles.x;
    }

    public override void Start()
    {
        ////Q往上
        //UnitControllerComponent.inputComponent.BindInputAction(KeyCode.Q, () =>
        //{
        //    CameraTransform.RotateAround(CameraCenterPoint.position, CameraTransform.right, rotSpeedY * Time.deltaTime);
        //}, KeyCodeType.ING);
        ////E往下
        //UnitControllerComponent.inputComponent.BindInputAction(KeyCode.E, () =>
        //{
        //    CameraTransform.RotateAround(CameraCenterPoint.position, CameraTransform.right, -rotSpeedY * Time.deltaTime);
        //}, KeyCodeType.ING);
        inputEntity = new InputEntity(KeyCode.Mouse0, KeyCodeType.None, false);
        inputEntity.name = "PlayerRotateEntity";
        inputEntity.BindInputAction(Execute);
    }

    private void Execute()
    {
        if (!RotEnable)
        {
            return;
        }
        // 获得鼠标当前位置的X和Y
        float mouseX = Input.GetAxis("Mouse X") * rotSpeedX * Time.deltaTime;
        CameraTransform.RotateAround(CameraCenterPoint.position, new Vector3(0, 1, 0), mouseX);

        float mouseY = -Input.GetAxis("Mouse Y") * rotSpeedY * Time.deltaTime;
        float rotX = angleX + mouseY;
        rotX = Mathf.Clamp(rotX, -30, 60) - rotX;
        mouseY += rotX;
        CameraTransform.RotateAround(CameraCenterPoint.position, CameraTransform.right, mouseY);
        angleX += mouseY;

    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        inputEntity.UnBindInputAction(Execute);
    }
}
