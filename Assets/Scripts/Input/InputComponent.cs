using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 枚举：按键操作类型
/// 弹起、按下、按住
/// </summary>
public enum KeyCodeType
{
    UP,
    DOWN,
    ING,
    //每帧自动调用，不管输入
    None
}
/// <summary>
/// 按键类
/// 按键操作类型、按键Code
/// </summary>
public class InputKeyCode
{
    public KeyCodeType keyCodeType;
    public KeyCode keyCode;
}
/// <summary>
/// 按键控制类
/// 主要完成对于按键和Acion的绑定、移除、替换
/// </summary>
public class InputComponent : MonoBehaviour
{
    /// <summary>
    /// 字典：KeyCode--Action
    /// </summary>
    public Dictionary<InputKeyCode, List<Action>> InputActionDict = new Dictionary<InputKeyCode, List<Action>>();

    private InputComponent Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            Instance = this;
            UnitControllerComponent.inputComponent = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Update()
    {
        // 每一帧都检查是否有按键触发
        foreach (var item in InputActionDict)
        {
            switch (item.Key.keyCodeType){
                case KeyCodeType.UP:
                    if (Input.GetKeyUp(item.Key.keyCode))
                    {
                        foreach (Action action in item.Value)
                        {
                            // Action响应
                            action.Invoke();
                        }
                    }
                    break;
                case KeyCodeType.DOWN:
                    if (Input.GetKeyDown(item.Key.keyCode))
                    {
                        foreach (Action action in item.Value)
                        {
                            action.Invoke();
                        }
                    }
                    break;
                case KeyCodeType.ING:
                    if (Input.GetKey(item.Key.keyCode))
                    {
                        foreach (Action action in item.Value)
                        {
                            action.Invoke();
                        }
                    }
                    break;
                case KeyCodeType.None:
                    foreach (Action action in item.Value)
                    {
                        action.Invoke();
                    }
                    break;
            }

        }

    }
    /// <summary>
    /// 绑定按键
    /// </summary>
    /// <param name="keyCode">按键类型</param>
    /// <param name="action"></param>
    /// <param name="keyCodeType">按键操作类型</param>
    public void BindInputAction(KeyCode keyCode, Action action,KeyCodeType keyCodeType)
    {
        foreach(var item in InputActionDict)
        {
            if (item.Key.keyCode == keyCode && item.Key.keyCodeType == keyCodeType)
            {
                InputActionDict[item.Key].Add(action);
                return;
            }
        }
        InputKeyCode inputKeyCode = new InputKeyCode();
        inputKeyCode.keyCode = keyCode;
        inputKeyCode.keyCodeType = keyCodeType;
        InputActionDict.Add(inputKeyCode, new List<Action>());
        InputActionDict[inputKeyCode].Add(action);
    }
    /// <summary>
    /// 去除按键
    /// </summary>
    /// <param name="keyCode"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public bool UnBindInputAction(KeyCode keyCode,Action action,KeyCodeType keyCodeType)
    {
        foreach (var item in InputActionDict)
        {
            if (item.Key.keyCode == keyCode && item.Key.keyCodeType == keyCodeType)
            {
                InputActionDict[item.Key].Remove(action);
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 更改按键
    /// </summary>
    /// <param name="keyCode"></param>
    /// <param name="newKeyCode"></param>
    /// <returns></returns>
    public bool SetInputKeyCode(KeyCode keyCode,KeyCode newKeyCode)
    {
        bool flag = false;
        foreach (var item in InputActionDict)
        {
            if (item.Key.keyCode == keyCode)
            {
                item.Key.keyCode = newKeyCode;
                flag = true;
            }
        }
        return flag;
    }

    /// <summary>
    /// 更改某个事件的按键
    /// </summary>
    /// <param name="keyCode">事件对应的原按键</param>
    /// <param name="action">事件</param>
    /// <param name="keyCodeType">事件对应的原按键的类型</param>
    /// <param name="newKeyCode">事件对应的新按键</param>
    /// <returns></returns>
    public bool SetActionKeyCode(KeyCode keyCode, Action action, KeyCodeType keyCodeType, KeyCode newKeyCode)
    {
        UnBindInputAction(keyCode, action, keyCodeType);
        BindInputAction(newKeyCode, action, keyCodeType);
        return false;
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
