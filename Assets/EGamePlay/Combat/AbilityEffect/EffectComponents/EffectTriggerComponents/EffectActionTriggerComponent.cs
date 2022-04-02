using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 行动点触发组件
    /// </summary>
    public class EffectActionTriggerComponent : Component
    {
        /// <summary>
        /// 添加行动点监听事件
        /// 行动点类型->对应能力效果的行动点类型
        /// 事件->对应能力效果
        /// </summary>
        public override void Setup()
        {
            var actionPointType = GetEntity<AbilityEffect>().EffectConfig.ActionPointType;
            GetEntity<AbilityEffect>().GetParent<StatusAbility>().OwnerEntity.ListenActionPoint(actionPointType, OnActionPointTrigger);
        }

        private void OnActionPointTrigger(ActionExecution combatAction)
        {
            GetEntity<AbilityEffect>().ApplyEffectToOwner();
        }
    }
}