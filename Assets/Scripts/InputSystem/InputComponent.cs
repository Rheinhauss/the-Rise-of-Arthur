using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 按键控制类
/// 主要完成对于按键和Acion的绑定、移除、替换
/// </summary>
public class InputComponent : MonoBehaviour
{
    /// <summary>
    /// 输入实体
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
        // 每一帧都检查是否有按键触发
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
    /// 返回name相等的所有输入实体的List
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

    ////用于面板输入更改按键设置
    //void OnGUI()
    //{
    //    Event e = Event.current;//获取当前事件
    //    // 有事件、事件是按键、按键不是None
    //    if (e != null && e.isKey && e.keyCode != KeyCode.None)
    //    {
    //    }
    //}

}
