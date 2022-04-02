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
    ING
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
}
