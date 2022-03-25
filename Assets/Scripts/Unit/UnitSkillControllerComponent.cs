using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EGamePlay.Combat;
using System;

public class UnitSkillControllerComponent
{
    public List<SkillObject> SkillObjectList = new List<SkillObject>();

    /// <summary>
    /// 添加SkillObject
    /// </summary>
    /// <param name="skillObject"></param>
    public void AddSkillObject(SkillObject skillObject)
    {
        SkillObjectList.Add(skillObject);
    }
    /// <summary>
    /// 移除
    /// </summary>
    /// <param name="skillObject"></param>
    public void RemoveSkillObject(SkillObject skillObject)
    {
        SkillObjectList.Remove(skillObject);
    }
    /// <summary>
    /// 获得SkillObject的Action
    /// </summary>
    /// <param name="skillObject"></param>
    /// <returns></returns>
    public Action GetSkillObjectAction(SkillObject skillObject)
    {
        return skillObject.GetAction();
    }
}
