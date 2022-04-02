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
/// ״̬����->���븽����SkillObject����ʹ��
/// </summary>
public class StatusObject
{
    /// <summary>
    /// ��״̬�����Ӧ��Ч������->Ψһ
    /// ��������SkillObject��SkillAbility��Ч��
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
