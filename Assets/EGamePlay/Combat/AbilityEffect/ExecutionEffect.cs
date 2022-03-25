using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 执行效果事件
    /// </summary>
    public class ExecutionEffectEvent
    {
        public ExecutionEffect ExecutionEffect;
    }

    /// <summary>
    /// 执行体效果
    /// </summary>
    public partial class ExecutionEffect : Entity
    {
        /// <summary>
        /// 对应的能力效果
        /// </summary>
        public AbilityEffect AbilityEffect { get; set; }
        /// <summary>
        /// 父对象的能力执行体
        /// </summary>
        public AbilityExecution ParentExecution => GetParent<AbilityExecution>();


        public override void Awake(object initData)
        {
            AbilityEffect = initData as AbilityEffect;
            Name = AbilityEffect.Name;
            //Log.Debug($"ExecutionEffect Awake {AbilityEffect.OwnerAbility.Name} {AbilityEffect.EffectConfig}");

            foreach (var component in AbilityEffect.Components.Values)
            {
                //时间到直接应用能力给目标效果
                if (component is EffectExecutionApplyToTargetComponent applyToTargetComponent)
                {
                    var executionApplyToTargetComponent = AddComponent<ExecutionApplyToTargetComponent>();
                    executionApplyToTargetComponent.EffectApplyType = applyToTargetComponent.EffectApplyType;
                    if (applyToTargetComponent.TriggerTime > 0)
                    {
                        AddComponent<ExecutionTimeTriggerComponent>().TriggerTime = (float)applyToTargetComponent.TriggerTime;
                    }
                    else
                    {
                        ApplyEffect();
                    }
                }
                //时间到生成碰撞体，碰撞体再触发应用能力效果
                if (component is EffectExecutionSpawnItemComponent spawnItemComponent)
                {
                    AddComponent<ExecutionSpawnItemComponent>().EffectSpawnItemComponent = spawnItemComponent;
                    if (spawnItemComponent.ColliderSpawnData.ColliderSpawnEmitter.time > 0)
                    {
                        AddComponent<ExecutionTimeTriggerComponent>().TriggerTime = (float)spawnItemComponent.ColliderSpawnData.ColliderSpawnEmitter.time;
                    }
                    else
                    {
                        ApplyEffect();
                    }
                }
                //时间到播放动作
                if (component is EffectExecutionAnimationComponent animationComponent)
                {
                    AddComponent<ExecutionAnimationComponent>().EffectAnimationComponent = animationComponent;
                    if (animationComponent.AnimationData.StartTime > 0)
                    {
                        AddComponent<ExecutionTimeTriggerComponent>().TriggerTime = (float)animationComponent.AnimationData.StartTime;
                    }
                    else
                    {
                        ApplyEffect();
                    }
                }
            }

            foreach (var item in Components.Values)
            {
                item.Enable = true;
            }
        }
        /// <summary>
        /// 应用效果
        /// </summary>
        public void ApplyEffect()
        {
            //Log.Debug($"ExecutionEffect ApplyEffect");
            //AbilityEffect.ApplyEffectToOwner();
            this.Publish(new ExecutionEffectEvent() { ExecutionEffect = this });
        }
        /// <summary>
        /// 应用效果至目标战斗实体
        /// </summary>
        /// <param name="targetEntity">目标战斗实体</param>
        public void ApplyEffectTo(CombatEntity targetEntity)
        {
            AbilityEffect.ApplyEffectTo(targetEntity, this);
        }
    }
}
