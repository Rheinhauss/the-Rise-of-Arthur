using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ö�٣���ǰλ��
/// ��Ҫ�����ڽ��ͬʱ����б���ߵİ���ʱ��W��D
/// </summary>
[System.Flags]
public enum CurrentPosition
{
    None = 0,
    Forward = 1,
    Backward = 2,
    Left = 4,
    Right = 8,
    Cross = 16,
}
/// <summary>
/// �ƶ�Skill��
/// �����ҷ�����ƶ�
/// </summary>
public class Skill_1_Move : SkillObject
{
    /// <summary>
    /// 
    /// </summary>
    public Vector3 position;
    /// <summary>
    /// 
    /// </summary>
    public static Vector3 movePosition = new Vector3(0,0,0);
    /// <summary>
    /// 
    /// </summary>
    public static CurrentPosition CurrentPosition = CurrentPosition.None;

}
