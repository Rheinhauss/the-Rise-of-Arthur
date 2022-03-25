using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 执行体效果组件
    /// </summary>
    public class ExecutionEffectComponent : Component
    {
        /// <summary>
        /// 需要执行的执行体效果列表
        /// </summary>
        public List<ExecutionEffect> ExecutionEffects { get; private set; } = new List<ExecutionEffect>();
        /// <summary>
        /// 伤害效果执行体
        /// </summary>
        public ExecutionEffect DamageExecutionEffect { get; set; }
        /// <summary>
        /// 回复效果执行体
        /// </summary>
        public ExecutionEffect CureExecutionEffect { get; set; }


        public override void Setup(object initData)
        {
            var abilityEffects = initData as List<AbilityEffect>;
            foreach (var abilityEffect in abilityEffects)
            {
                var executionEffect = Entity.AddChild<ExecutionEffect>(abilityEffect);
                AddEffect(executionEffect);

                if (abilityEffect.EffectConfig is DamageEffect)
                {
                    DamageExecutionEffect = executionEffect;
                }
                if (abilityEffect.EffectConfig is CureEffect)
                {
                    CureExecutionEffect = executionEffect;
                }
            }
        }
        /// <summary>
        /// 添加效果执行体
        /// </summary>
        /// <param name="executionEffect">需要添加的效果执行体</param>
        public void AddEffect(ExecutionEffect executionEffect)
        {
            ExecutionEffects.Add(executionEffect);
        }

        //public void SetOneEffect(ExecutionEffect executionEffect)
        //{
        //    ExecutionEffects.Clear();
        //    ExecutionEffects.Add(executionEffect);
        //}
        /// <summary>
        /// 覆盖效果执行体列表
        /// </summary>
        /// <param name="executionEffects"></param>
        public void FillEffects(List<ExecutionEffect> executionEffects)
        {
            this.ExecutionEffects.Clear();
            this.ExecutionEffects.AddRange(executionEffects);
        }

        //public ExecutionEffect GetEffect(int index = 0)
        //{
        //    return ExecutionEffects[index];
        //}
        /// <summary>
        /// 将所有效果应用到目标战斗实体
        /// </summary>
        /// <param name="targetEntity">目标战斗实体</param>
        public void ApplyAllEffectsTo(CombatEntity targetEntity)
        {
            if (ExecutionEffects.Count > 0)
            {
                foreach (var executionEffect in ExecutionEffects)
                {
                    executionEffect.ApplyEffectTo(targetEntity);
                }
            }
        }
        /// <summary>
        /// 将第index个效果应用到目标战斗实体
        /// </summary>
        /// <param name="targetEntity">目标战斗实体</param>
        /// <param name="index">序号</param>
        public void ApplyEffectByIndex(CombatEntity targetEntity, int index)
        {
            ExecutionEffects[index].ApplyEffectTo(targetEntity);
        }
    }
}