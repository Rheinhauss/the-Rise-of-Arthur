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
        //// �����굱ǰλ�õ�X��Y
        //float mouseX = Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime;
        ////float mouseY = Input.GetAxis("Mouse Y") * rotSpeed;

        //// �����Y���ϵ��ƶ���תΪ������������˶�����������X�ᷴ����ת
        ////Camera.main.transform.localRotation = Camera.main.transform.localRotation * Quaternion.Euler(-mouseY, 0, 0);
        //// �����X���ϵ��ƶ�תΪ�������ҵ��ƶ���ͬʱ������������������������ƶ�
        //CameraTransform.localRotation = CameraTransform.localRotation * Quaternion.Euler(0, mouseX, 0);
    }
}
