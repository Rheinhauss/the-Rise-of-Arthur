using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using EGamePlay.Combat;
using UnityEngine;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 效果赋给行动能力
    /// </summary>
    public class EffectAssignAbility : ActionAbility<EffectAssignAction>
    {

    }

    /// <summary>
    /// 赋给效果行动
    /// </summary>
    public class EffectAssignAction : ActionExecution
    {
        /// <summary>
        /// 创建这个效果赋给行动的源能力
        /// </summary>
        public AbilityEntity SourceAbility { get; set; }
        /// <summary>
        /// 效果
        /// </summary>
        public Effect EffectConfig => AbilityEffect.EffectConfig;


        /// <summary>
        /// 前置处理
        /// </summary>
        private void PreProcess()
        {

        }
        /// <summary>
        /// 应用效果赋给
        /// </summary>
        public void ApplyEffectAssign()
        {
            //Log.Debug($"ApplyEffectAssign {EffectConfig}");
            PreProcess();

            if (EffectConfig is DamageEffect)
            {
                if (OwnerEntity.DamageAbility.TryMakeAction(out var damageAction))
                {
                    damageAction.Target = Target;
                    damageAction.AbilityEffect = AbilityEffect;
                    damageAction.ExecutionEffect = ExecutionEffect;
                    damageAction.DamageSource = DamageSource.Skill;
                    damageAction.ApplyDamage();
                }
            }
            // && Target.UnitPropertyEntity.HP.IsFull() == false
            else if (EffectConfig is CureEffect)
            {
                if (OwnerEntity.CureAbility.TryMakeAction(out var cureAction))
                {
                    cureAction.Target = Target;
                    cureAction.AbilityEffect = AbilityEffect;
                    cureAction.ExecutionEffect = ExecutionEffect;
                    cureAction.ApplyCure();
                }
            }

            else if (EffectConfig is AddStatusEffect)
            {
                if (OwnerEntity.AddStatusAbility.TryMakeAction(out var addStatusAction))
                {
                    addStatusAction.SourceAbility = SourceAbility;
                    addStatusAction.Target = Target;
                    addStatusAction.AbilityEffect = AbilityEffect;
                    addStatusAction.ExecutionEffect = ExecutionEffect;
                    addStatusAction.ApplyAddStatus();
                }
            }
            
            else if(EffectConfig is RemoveStatusEffect)
            {
                RemoveStatusEffect removeStatusEffect = EffectConfig as RemoveStatusEffect;
                string id = removeStatusEffect.RemoveStatus.ID;
                while (OwnerEntity.HasStatus(id))
                {
                    OwnerEntity.GetStatus(id).EndAbility();
                }
            }

            else if(EffectConfig is ClearAllStatusEffect)
            {
                string id;
                while(OwnerEntity.TypeIdStatuses.Count != 0)
                {
                    id = OwnerEntity.TypeIdStatuses.ElementAt(0).Key;
                    while (OwnerEntity.HasStatus(id))
                    {
                        OwnerEntity.GetStatus(id).EndAbility();
                    }
                }
            }
            else if(EffectConfig is CustomEffect)
            {
                CustomEffect customEffect = (EffectConfig as CustomEffect);
                customEffect.Event.Invoke(Creator, Target, customEffect.Params);
            }


            PostProcess();

            ApplyAction();
        }

        /// <summary>
        /// 后置处理
        /// </summary>
        private void PostProcess()
        {
            Creator.TriggerActionPoint(ActionPointType.AssignEffect, this);
            Target.TriggerActionPoint(ActionPointType.ReceiveEffect, this);
        }
    }
}