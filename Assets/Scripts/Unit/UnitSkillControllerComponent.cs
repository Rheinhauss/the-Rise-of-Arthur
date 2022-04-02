using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EGamePlay.Combat;
using System;

public class UnitSkillControllerComponent
{
    public List<SkillObject> SkillObjectList = new List<SkillObject>();

    /// <summary>
    /// ���SkillObject
    /// </summary>
    /// <param name="skillObject"></param>
    public void AddSkillObject(SkillObject skillObject)
    {
        SkillObjectList.Add(skillObject);
    }
    /// <summary>
    /// �Ƴ�
    /// </summary>
    /// <param name="skillObject"></param>
    public void RemoveSkillObject(SkillObject skillObject)
    {
        SkillObjectList.Remove(skillObject);
    }
    /// <summary>
    /// ���SkillObject��Action
    /// </summary>
    /// <param name="skillObject"></param>
    /// <returns></returns>
    public Action GetSkillObjectAction(SkillObject skillObject)
    {
        return skillObject.GetAction();
    }
}
