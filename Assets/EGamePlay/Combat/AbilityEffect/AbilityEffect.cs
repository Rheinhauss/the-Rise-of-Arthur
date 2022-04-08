using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 应用效果事件
    /// </summary>
    public class ApplyEffectEvent { public AbilityEffect AbilityEffect; }
    /// <summary>
    /// 效果资源类型:能力/执行
    /// </summary>
    public enum EffectSourceType { Ability, Execution }

    /// <summary>
    /// 能力效果
    /// </summary>
    public partial class AbilityEffect : Entity
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get; set; }
        /// <summary>
        /// 所属的能力实体
        /// </summary>
        public AbilityEntity OwnerAbility => GetParent<AbilityEntity>();
        /// <summary>
        /// 所属的战斗实体
        /// </summary>
        public CombatEntity OwnerEntity => OwnerAbility.OwnerEntity;
        /// <summary>
        /// 效果配置
        /// </summary>
        public Effect EffectConfig { get; set; }
        /// <summary>
        /// 效果资源类型
        /// </summary>
        public EffectSourceType EffectSourceType { get; set; }


        public override void Awake(object initData)
        {
            this.EffectConfig = initData as Effect;
            Name = EffectConfig.GetType().Name;
            //Log.Debug($"AbilityEffect Awake {OwnerAbility.Name} {EffectConfig}");

            //伤害效果
            if (this.EffectConfig is DamageEffect damageEffect)
            {
                AddComponent<EffectDamageComponent>();
            }
            //治疗效果
            if (this.EffectConfig is CureEffect cureEffect)
            {
                AddComponent<EffectCureComponent>();
            }
            //施加状态效果
            if (this.EffectConfig is AddStatusEffect addStatusEffect)
            {
                AddComponent<EffectAddStatusComponent>();
            }
            //自定义效果
            if (this.EffectConfig is CustomEffect customEffect)
            {
                AddComponent<EffectCustomComponent>();
            }

            //立即触发
            if (EffectConfig.EffectTriggerType == EffectTriggerType.Instant)
            {
                ApplyEffectToParent();
            }
            //行动点触发
            if (EffectConfig.EffectTriggerType == EffectTriggerType.Action)
            {
                AddComponent<EffectActionTriggerComponent>();
            }
            //间隔触发
            if (EffectConfig.EffectTriggerType == EffectTriggerType.Interval)
            {
                if (!string.IsNullOrEmpty(EffectConfig.Interval))
                {
                    AddComponent<EffectIntervalTriggerComponent>();
                }
            }
            //条件触发
            if (EffectConfig.EffectTriggerType == EffectTriggerType.Condition)
            {
                if (!string.IsNullOrEmpty(EffectConfig.ConditionParam))
                {
                    AddComponent<EffectConditionTriggerComponent>();
                }
            }
        }
        /// <summary>
        /// 启用效果
        /// </summary>
        public void EnableEffect()
        {
            Enable = true;
            foreach (var item in Components.Values)
            {
                item.Enable = true;
            }
        }
        /// <summary>
        /// 禁用效果
        /// </summary>
        public void DisableEffect()
        {
            Enable = false;
            foreach (var item in Components.Values)
            {
                item.Enable = false;
            }
        }

        //public void ApplyEffect()
        //{
        //    Publish(new ApplyEffectEvent() { AbilityEffect = this });
        //}
        /// <summary>
        /// 应用效果到所属战斗实体
        /// </summary>
        public void ApplyEffectToOwner()
        {
            ApplyEffectTo(OwnerAbility.OwnerEntity);
        }
        /// <summary>
        /// 应用效果到父对象的战斗实体
        /// </summary>
        public void ApplyEffectToParent()
        {
            ApplyEffectTo(OwnerAbility.ParentEntity);
        }
        /// <summary>
        /// 应用效果到目标战斗实体
        /// </summary>
        /// <param name="targetEntity">目标战斗实体</param>
        public void ApplyEffectTo(CombatEntity targetEntity)
        {
            if (OwnerEntity.EffectAssignAbility.TryMakeAction(out var action))
            {
                //Log.Debug($"AbilityEffect ApplyEffectTo {targetEntity} {EffectConfig}");
                action.Target = targetEntity;
                action.SourceAbility = OwnerAbility;
                action.AbilityEffect = this;
                action.ApplyEffectAssign();
            }
        }
        /// <summary>
        /// 将效果应用到目标战斗实体
        /// </summary>
        /// <param name="targetEntity">目标战斗实体</param>
        /// <param name="executionEffect">需要应用的效果</param>
        public void ApplyEffectTo(CombatEntity targetEntity, ExecutionEffect executionEffect)
        {
            if (OwnerEntity.EffectAssignAbility.TryMakeAction(out var action))
            {
                //Log.Debug($"AbilityEffect ApplyEffectTo {targetEntity} {EffectConfig}");
                action.Target = targetEntity;
                action.SourceAbility = OwnerAbility;
                action.AbilityEffect = this;
                action.ExecutionEffect = executionEffect;
                action.ApplyEffectAssign();
            }
        }
    }
}
