using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GameUtils;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 执行体应用目标效果能力效果组件
    /// </summary>
    public class EffectExecutionApplyToTargetComponent : Component
    {
        /// <summary>
        /// 默认启用
        /// </summary>
        public override bool DefaultEnable { get; set; } = false;
        /// <summary>
        /// 触发时间
        /// </summary>
        public float TriggerTime { get; set; }
        /// <summary>
        /// 时间值表达式
        /// </summary>
        public string TimeValueExpression { get; set; }
        /// <summary>
        /// 应用效果的类型
        /// </summary>
        public EffectApplyType EffectApplyType { get; set; }
        /// <summary>
        /// 能力效果列表
        /// </summary>
        public List<AbilityEffect> ApplyAbilityEffects { get; set; } = new List<AbilityEffect>();
    }
}