using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ����������
/// ��Ҫ��ɶ��ڰ�����Acion�İ󶨡��Ƴ����滻
/// </summary>
public class InputComponent : MonoBehaviour
{
    /// <summary>
    /// ����ʵ��
    /// </summary>
    public List<InputEntity> InputEntityList { get; private set; }

    public static InputComponent Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
            UnitControllerComponent.inputComponent = this;
            DontDestroyOnLoad(this.gameObject);
            InputEntityList = new List<InputEntity>();
        }
    }

    private void Update()
    {
        // ÿһ֡������Ƿ��а�������
        foreach (var item in InputEntityList)
        {
            switch (item.keyCodeType){
                case KeyCodeType.UP:
                    if (Input.GetKeyUp(item.keyCode))
                    {
                        item.Execute();
                    }
                    break;
                case KeyCodeType.DOWN:
                    if (Input.GetKeyDown(item.keyCode))
                    {
                        item.Execute();
                    }
                    break;
                case KeyCodeType.ING:
                    if (Input.GetKey(item.keyCode))
                    {
                        item.Execute();
                    }
                    break;
                case KeyCodeType.None:
                    item.Execute();
                    break;
            }

        }

    }

    /// <summary>
    /// ����name��ȵ���������ʵ���List
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public List<InputEntity> GetInputEntity(string name)
    {
        List<InputEntity> list = new List<InputEntity>();
        foreach(InputEntity inputEntity in InputEntityList)
        {
            if(inputEntity.name == name)
            {
                list.Add(inputEntity);
            }
        }
        return list;
    }

    ////�������������İ�������
    //void OnGUI()
    //{
    //    Event e = Event.current;//��ȡ��ǰ�¼�
    //    // ���¼����¼��ǰ�������������None
    //    if (e != null && e.isKey && e.keyCode != KeyCode.None)
    //    {
    //    }
    //}

}
