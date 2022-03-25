using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EGamePlay.Combat;
using EGamePlay;
using ET;
using GameUtils;
using System;

public class SkillObject
{
    private SkillConfigObject skillConfigObject;
    public SkillAbility SkillAbility;
    public Action action;
    /// <summary>
    /// 初始化SkillObject
    /// </summary>
    /// <param name="configpath"></param>
    /// <param name="combatEntity"></param>
    /// <param name="action"></param>
    public void InitSkillObject(string configpath,CombatEntity combatEntity,Action action)
    {
        SetSkillConfig(configpath);
        SetSkillAbility(combatEntity);
        
        SetAction(action);
    }
    /// <summary>
    /// 读取skill的config配置文件，初始化
    /// </summary>
    /// <param name="configpath"></param>
    public void SetSkillConfig(string configpath)
    {
        skillConfigObject = Resources.Load<SkillConfigObject>(configpath);
    }
    /// <summary>
    /// 初始化SkillAbility
    /// </summary>
    /// <param name="combatEntity"></param>
    public void SetSkillAbility(CombatEntity combatEntity)
    {
        //Debug.Log(skillConfigObject);
        SkillAbility = combatEntity.AttachSkill<SkillAbility>(skillConfigObject);
    }
    /// <summary>
    /// 设置Skill对应的Action
    /// </summary>
    /// <param name="action"></param>
    public void SetAction(Action action)
    {
        this.action = action;
    }
    /// <summary>
    /// 返回Action
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public Action GetAction()
    {
        return this.action;
    }

    /// <summary>
    /// 添加状态
    /// </summary>
    /// <param name="statusObject"></param>
    public void AddStatus(StatusObject statusObject)
    {
        SkillAbility.AddStatus(statusObject.effect);
    }

    /// <summary>
    /// 移除状态
    /// </summary>
    /// <param name="statusObject"></param>
    public void RemoveStatus(StatusObject statusObject)
    {
        SkillAbility.RemoveStatus(statusObject.effect);
    }
}
