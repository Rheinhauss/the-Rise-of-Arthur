using EGamePlay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotateEntity : Entity
{
    private Player Player => Player.Instance;
    public Transform CameraTransform => Player.transform.GetChild(3);

    public float rotSpeed { get; private set; }

    public static bool RotEnable { get; set; }

    public void Init()
    {
        AddComponent<UpdateComponent>();
        rotSpeed = 200;
        RotEnable = true;
    }

    public override void Update()
    {
        if (!RotEnable)
        {
            return;
        }
        //// 获得鼠标当前位置的X和Y
        //float mouseX = Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime;
        ////float mouseY = Input.GetAxis("Mouse Y") * rotSpeed;

        //// 鼠标在Y轴上的移动号转为摄像机的上下运动，即是绕着X轴反向旋转
        ////Camera.main.transform.localRotation = Camera.main.transform.localRotation * Quaternion.Euler(-mouseY, 0, 0);
        //// 鼠标在X轴上的移动转为主角左右的移动，同时带动其子物体摄像机的左右移动
        //CameraTransform.localRotation = CameraTransform.localRotation * Quaternion.Euler(0, mouseX, 0);
    }
}
