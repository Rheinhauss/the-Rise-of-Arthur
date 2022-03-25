using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 条件触发组件
    /// </summary>
    public class EffectConditionTriggerComponent : Component
    {
        /// <summary>
        /// 默认启动
        /// </summary>
        public override bool DefaultEnable { get; set; } = false;
        /// <summary>
        /// 条件参数值
        /// </summary>
        public string ConditionParamValue { get; set; }


        public override void Setup()
        {

        }
        /// <summary>
        /// 启用时调用
        /// 添加条件监听事件       
        /// 条件类型->对应能力效果的条件类型
        /// 事件->对应能力效果
        /// </summary>
        public override void OnEnable()
        {
            var conditionType = GetEntity<AbilityEffect>().EffectConfig.ConditionType;
            var conditionParam = ConditionParamValue;
            Entity.GetParent<StatusAbility>().OwnerEntity.ListenerCondition(conditionType, OnConditionTrigger, conditionParam);
        }

        private void OnConditionTrigger()
        {
            GetEntity<AbilityEffect>().ApplyEffectToOwner();
        }
    }
}