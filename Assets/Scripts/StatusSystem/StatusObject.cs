using EGamePlay.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusType
{
    AddStatus,
    RemoveStatus,
    ClearAllStatus
}

/// <summary>
/// 状态对象->必须附加于SkillObject才能使用
/// </summary>
public class StatusObject
{
    /// <summary>
    /// 此状态对象对应的效果对象->唯一
    /// 即附加于SkillObject的SkillAbility的效果
    /// </summary>
    public Effect effect;

    public void Init(StatusType statusType, string configPath, AddSkillEffetTargetType target,uint Duration = 1000, string TriggerProbability = "100%")
    {
        if(statusType == StatusType.AddStatus)
        {
            InitAddStatus(configPath, target, Duration, TriggerProbability);
        }
        else if (statusType == StatusType.RemoveStatus)
        {
            InitRemoveStatus(configPath, target, TriggerProbability);
        }
        else if(statusType == StatusType.ClearAllStatus)
        {
            InitClearAllStatus(target);
        }
    }

    private void InitAddStatus(string configPath, AddSkillEffetTargetType target, uint Duration, string TriggerProbability)
    {
        AddStatusEffect statusEffect = new AddStatusEffect();
        statusEffect.AddSkillEffectTargetType = target;
        statusEffect.AddStatus = Resources.Load<StatusConfigObject>(configPath);
        statusEffect.Duration = Duration;
        statusEffect.TriggerProbability = TriggerProbability;
        effect = statusEffect;
    }

    private void InitRemoveStatus(string configPath, AddSkillEffetTargetType target, string TriggerProbability)
    {
        RemoveStatusEffect statusEffect = new RemoveStatusEffect();
        statusEffect.AddSkillEffectTargetType = target;
        statusEffect.RemoveStatus = Resources.Load<StatusConfigObject>(configPath);
        effect = statusEffect;
    }

    private void InitClearAllStatus(AddSkillEffetTargetType target)
    {
        ClearAllStatusEffect statusEffect = new ClearAllStatusEffect();
        statusEffect.AddSkillEffectTargetType = target;
        effect = statusEffect;
    }
}
