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

public class InputEntity
{
    public KeyCodeType keyCodeType { get; private set; }
    public KeyCode keyCode { get; private set; }
    public List<Action> actions { get; private set; }
    /// <summary>
    /// 输入实体的名称
    /// </summary>
    public string name = "";
    /// <summary>
    /// 输入实体的描述
    /// </summary>
    public string decoration = "";

    /// <summary>
    /// 是否能更改按键设置
    /// </summary>
    public bool CanChange = true;

    /// <summary>
    /// 重置时的默认按键
    /// </summary>
    private KeyCodeType originKeyCodeType;
    private KeyCode originKeyCode;

    public InputEntity()
    {
        InputComponent.Instance.InputEntityList.Add(this);
        actions = new List<Action>();
    }

    public InputEntity(KeyCode keyCode, KeyCodeType keyCodeType, bool CanChange = true)
    {
        InputComponent.Instance.InputEntityList.Add(this);
        actions = new List<Action>();
        originKeyCode = this.keyCode = keyCode;
        originKeyCodeType = this.keyCodeType = keyCodeType;
        this.CanChange = CanChange;
    }

    ~InputEntity()
    {
        InputComponent.Instance.InputEntityList.Remove(this);
    }

    /// <summary>
    /// 绑定按键
    /// </summary>
    /// <param name="action"></param>
    public void BindInputAction(Action action)
    {
        actions.Add(action);
    }
    /// <summary>
    /// 去除按键
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public bool UnBindInputAction(Action action)
    {
        if (actions.Contains(action))
        {
            return actions.Remove(action);
        }
        return false;
    }

    /// <summary>
    /// 更改按键
    /// </summary>
    /// <param name="newKeyCode"></param>
    /// <returns></returns>
    public bool SetInputKeyCode(KeyCode newKeyCode)
    {
        if (CanChange)
        {
            return CanChange;
        }
        this.keyCode = newKeyCode;
        return CanChange;
    }
    /// <summary>
    /// 更改按键类型
    /// </summary>
    /// <param name="newKeyCodeType"></param>
    public bool SetInputKeyCodeType(KeyCodeType newKeyCodeType)
    {
        if (CanChange)
        {
            return CanChange;
        }
        this.keyCodeType = newKeyCodeType;
        return CanChange;
    }

    /// <summary>
    /// 更改重置默认按键
    /// </summary>
    /// <param name="newKeyCode"></param>
    /// <returns></returns>
    public void SetOriginInputKeyCode(KeyCode newKeyCode)
    {
        this.originKeyCode = newKeyCode;
    }
    /// <summary>
    /// 更改重置默认按键类型
    /// </summary>
    /// <param name="newKeyCodeType"></param>
    public void SetOriginInputKeyCodeType(KeyCodeType newKeyCodeType)
    {
        this.originKeyCodeType = newKeyCodeType;
    }

    /// <summary>
    /// 执行绑定的所有事件
    /// </summary>
    public void Execute()
    {
        foreach (Action action in actions)
        {
            // Action响应
            action.Invoke();
        }
    }

    /// <summary>
    /// 重置按键类型
    /// </summary>
    public void Reset()
    {
        keyCode = originKeyCode;
        keyCodeType = originKeyCodeType;
    }

}
