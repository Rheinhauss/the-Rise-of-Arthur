using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ö�٣�������������
/// ���𡢰��¡���ס
/// </summary>
public enum KeyCodeType
{
    UP,
    DOWN,
    ING
}
/// <summary>
/// ������
/// �����������͡�����Code
/// </summary>
public class InputKeyCode
{
    public KeyCodeType keyCodeType;
    public KeyCode keyCode;
}
/// <summary>
/// ����������
/// ��Ҫ��ɶ��ڰ�����Acion�İ󶨡��Ƴ����滻
/// </summary>
public class InputComponent : MonoBehaviour
{
    /// <summary>
    /// �ֵ䣺KeyCode--Action
    /// </summary>
    public Dictionary<InputKeyCode, List<Action>> InputActionDict = new Dictionary<InputKeyCode, List<Action>>();

    private void Update()
    {
        // ÿһ֡������Ƿ��а�������
        foreach (var item in InputActionDict)
        {
            switch (item.Key.keyCodeType){
                case KeyCodeType.UP:
                    if (Input.GetKeyUp(item.Key.keyCode))
                    {
                        foreach (Action action in item.Value)
                        {
                            // Action��Ӧ
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
    /// �󶨰���
    /// </summary>
    /// <param name="keyCode">��������</param>
    /// <param name="action"></param>
    /// <param name="keyCodeType">������������</param>
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
    /// ȥ������
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
    /// ���İ���
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
