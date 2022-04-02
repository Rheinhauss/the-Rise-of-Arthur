﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EGamePlay;
using System;
using B83.ExpressionParser;
using GameUtils;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 伤害行动能力
    /// </summary>
    public class DamageActionAbility : ActionAbility<DamageAction>
    {
        public override void Awake()
        {
            AddComponent<DamageBloodSuckComponent>();
        }
    }

    /// <summary>
    /// 伤害行动
    /// </summary>
    public class DamageAction : ActionExecution
    {
        /// <summary>
        /// 伤害行动能力
        /// </summary>
        public DamageActionAbility DamageAbility => ActionAbility as DamageActionAbility;
        /// <summary>
        /// 伤害效果
        /// </summary>
        public DamageEffect DamageEffect => AbilityEffect.EffectConfig as DamageEffect;
        /// <summary>
        /// 伤害来源
        /// </summary>
        public DamageSource DamageSource { get; set; }
        /// <summary>
        /// 伤害攻势
        /// </summary>
        public int DamagePotential { get; set; }
        /// <summary>
        /// 防御架势
        /// </summary>
        public int DefensePosture { get; set; }
        /// <summary>
        /// 伤害数值
        /// </summary>
        public int DamageValue { get; set; }
        /// <summary>
        /// 是否是暴击
        /// </summary>
        public bool IsCritical { get; set; }


        /// <summary>
        /// 前置处理
        /// </summary>
        private void PreProcess()
        {
            if (DamageSource == DamageSource.Attack)
            {
                IsCritical = (RandomHelper.RandomRate() / 100f) < Creator.GetComponent<AttributeComponent>().CriticalProbability.Value;
                DamageValue = Mathf.CeilToInt(Mathf.Max(1, Creator.GetComponent<AttributeComponent>().Attack.Value - Target.GetComponent<AttributeComponent>().Defense.Value));
                if (IsCritical)
                {
                    DamageValue = Mathf.CeilToInt(DamageValue * 1.5f);
                }
            }

            if (DamageSource == DamageSource.Skill)
            {
                if (DamageEffect.CanCrit)
                {
                    IsCritical = (RandomHelper.RandomRate() / 100f) < Creator.GetComponent<AttributeComponent>().CriticalProbability.Value;
                }
                DamageValue = AbilityEffect.GetComponent<EffectDamageComponent>().GetDamageValue();
                if (IsCritical)
                {
                    DamageValue = Mathf.CeilToInt(DamageValue * 1.5f);
                }
            }

            if (DamageSource == DamageSource.Buff)
            {
                if (DamageEffect.CanCrit)
                {
                    IsCritical = (RandomHelper.RandomRate() / 100f) < Creator.GetComponent<AttributeComponent>().CriticalProbability.Value;
                }
                DamageValue = AbilityEffect.GetComponent<EffectDamageComponent>().GetDamageValue();
            }

            if (ExecutionEffect != null)
            {
                var executionDamageReduceWithTargetCountComponent = ExecutionEffect.GetComponent<ExecutionDamageReduceWithTargetCountComponent>();
                if (executionDamageReduceWithTargetCountComponent != null)
                {
                    var damagePercent = executionDamageReduceWithTargetCountComponent.GetDamagePercent();
                    DamageValue = Mathf.CeilToInt(DamageValue * damagePercent);
                    executionDamageReduceWithTargetCountComponent.AddOneTarget();
                }
            }

            //触发 造成伤害前 行动点
            Creator.TriggerActionPoint(ActionPointType.PreCauseDamage, this);
            //触发 承受伤害前 行动点
            Target.TriggerActionPoint(ActionPointType.PreReceiveDamage, this);
        }

        /// <summary>
        /// 应用伤害
        /// </summary>
        public void ApplyDamage()
        {
            PreProcess();

            Target.ReceiveDamage(this);

            PostProcess();

            if (Target.CheckDead())
            {
                var deadEvent = new EntityDeadEvent() { DeadEntity = Target };
                Target.Publish(deadEvent);
                CombatContext.Instance.Publish(deadEvent);
            }

            ApplyAction();
        }

        /// <summary>
        /// 后置处理
        /// </summary>
        private void PostProcess()
        {
            //触发 造成伤害后 行动点
            Creator.TriggerActionPoint(ActionPointType.PostCauseDamage, this);
            //触发 承受伤害后 行动点
            Target.TriggerActionPoint(ActionPointType.PostReceiveDamage, this);
        }
    }
    /// <summary>
    /// 伤害来源
    /// </summary>
    public enum DamageSource
    {
        Attack,//普攻
        Skill,//技能
        Buff,//Buff
    }
}