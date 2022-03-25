﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using ET;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 能力单元体
    /// </summary>
    public class AbilityItem : Entity, IPosition
    {
        /// <summary>
        /// 对应能力实体
        /// </summary>
        public AbilityEntity AbilityEntity => AbilityExecution.AbilityEntity;
        /// <summary>
        /// 对应能力执行体
        /// </summary>
        public AbilityExecution AbilityExecution { get; set; }
        /// <summary>
        /// 对应效果执行体组件
        /// </summary>
        public ExecutionEffectComponent ExecutionEffectComponent { get; private set; }
        /// <summary>
        /// 位置
        /// </summary>
        public Vector3 Position { get; set; }
        /// <summary>
        /// 方位
        /// </summary>
        public Vector3 Direction { get; set; }
        /// <summary>
        /// 目标实体
        /// </summary>
        public CombatEntity TargetEntity { get; set; }


        public override void Awake(object initData)
        {
            AbilityExecution = initData as AbilityExecution;
            ExecutionEffectComponent = AddComponent<ExecutionEffectComponent>();
            var abilityEffects = AbilityEntity.AbilityEffects;
            foreach (var abilityEffect in abilityEffects)
            {
                if (abilityEffect.GetComponent<EffectExecutionSpawnItemComponent>() != null)
                {
                    continue;
                }
                if (abilityEffect.GetComponent<EffectExecutionAnimationComponent>() != null)
                {
                    continue;
                }

                var executionEffect = AddChild<ExecutionEffect>(abilityEffect);
                ExecutionEffectComponent.AddEffect(executionEffect);

                if (abilityEffect.EffectConfig is DamageEffect)
                {
                    ExecutionEffectComponent.DamageExecutionEffect = executionEffect;
                }
                if (abilityEffect.EffectConfig is CureEffect)
                {
                    ExecutionEffectComponent.CureExecutionEffect = executionEffect;
                }
            }
        }

        /// <summary>
        /// 结束单元体
        /// </summary>
        public void DestroyItem()
        {
            Destroy(this);
        }

        //public void FillExecutionEffects(AbilityExecution abilityExecution)
        //{
        //    //AbilityExecution = abilityExecution;
        //    ExecutionEffectComponent.FillEffects(abilityExecution.ExecutionEffects);
        //}
        /// <summary>
        /// 碰撞事件
        /// </summary>
        /// <param name="otherCombatEntity">所碰撞的战斗实体</param>
        public void OnCollision(CombatEntity otherCombatEntity)
        {
            if (TargetEntity == null)
            {
                ExecutionEffectComponent.ApplyAllEffectsTo(otherCombatEntity);
            }

            if (TargetEntity != null)
            {
                if (otherCombatEntity != TargetEntity)
                {
                    return;
                }
                ExecutionEffectComponent.ApplyAllEffectsTo(otherCombatEntity);
                DestroyItem();
            }
        }
    }
}