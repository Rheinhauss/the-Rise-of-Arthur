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

    public void Init()
    {
        rotSpeedX = 200;
        rotSpeedY = 100.0f;
        radius = 1.2f;
        RotEnable = true;
    }

    public override void Start()
    {
        //Q����
        UnitControllerComponent.inputComponent.BindInputAction(KeyCode.Q, () =>
        {
            CameraTransform.RotateAround(CameraCenterPoint.position, CameraTransform.right, rotSpeedY * Time.deltaTime);
        }, KeyCodeType.ING);
        //E����
        UnitControllerComponent.inputComponent.BindInputAction(KeyCode.E, () =>
        {
            CameraTransform.RotateAround(CameraCenterPoint.position, CameraTransform.right, -rotSpeedY * Time.deltaTime);
        }, KeyCodeType.ING);

        UnitControllerComponent.inputComponent.BindInputAction(KeyCode.Mouse0, () =>
        {
            if (!RotEnable)
            {
                return;
            }
            // �����굱ǰλ�õ�X��Y
            float mouseX = Input.GetAxis("Mouse X") * rotSpeedX * Time.deltaTime;

            //float mouseY = Input.GetAxis("Mouse Y") * rotSpeedY * Time.deltaTime;
            //Debug.Log(mouseX + " " + Time.deltaTime + " " + Input.GetAxis("Mouse X"));
            // �����X���ϵ��ƶ�תΪ�������ҵ��ƶ���ͬʱ������������������������ƶ�
            CameraTransform.RotateAround(CameraCenterPoint.position, new Vector3(0, 1, 0), mouseX);
        }, KeyCodeType.None);
    }
}
