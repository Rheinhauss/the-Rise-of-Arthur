using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 枚举：当前位置
/// 主要是用于解决同时按下斜着走的按键时候，W和D
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
/// 移动Skill类
/// 完成玩家方向的移动
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
