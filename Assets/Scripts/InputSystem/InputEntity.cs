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
    ING,
    //ÿ֡�Զ����ã���������
    None
}

public class InputEntity
{
    public KeyCodeType keyCodeType { get; private set; }
    public KeyCode keyCode { get; private set; }
    public List<Action> actions { get; private set; }
    /// <summary>
    /// ����ʵ�������
    /// </summary>
    public string name = "";
    /// <summary>
    /// ����ʵ�������
    /// </summary>
    public string decoration = "";

    /// <summary>
    /// �Ƿ��ܸ��İ�������
    /// </summary>
    public bool CanChange = true;

    /// <summary>
    /// ����ʱ��Ĭ�ϰ���
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
    /// �󶨰���
    /// </summary>
    /// <param name="action"></param>
    public void BindInputAction(Action action)
    {
        actions.Add(action);
    }
    /// <summary>
    /// ȥ������
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
    /// ���İ���
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
    /// ���İ�������
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
    /// ��������Ĭ�ϰ���
    /// </summary>
    /// <param name="newKeyCode"></param>
    /// <returns></returns>
    public void SetOriginInputKeyCode(KeyCode newKeyCode)
    {
        this.originKeyCode = newKeyCode;
    }
    /// <summary>
    /// ��������Ĭ�ϰ�������
    /// </summary>
    /// <param name="newKeyCodeType"></param>
    public void SetOriginInputKeyCodeType(KeyCodeType newKeyCodeType)
    {
        this.originKeyCodeType = newKeyCodeType;
    }

    /// <summary>
    /// ִ�а󶨵������¼�
    /// </summary>
    public void Execute()
    {
        foreach (Action action in actions)
        {
            // Action��Ӧ
            action.Invoke();
        }
    }

    /// <summary>
    /// ���ð�������
    /// </summary>
    public void Reset()
    {
        keyCode = originKeyCode;
        keyCodeType = originKeyCodeType;
    }

}
