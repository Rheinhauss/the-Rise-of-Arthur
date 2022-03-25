using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 伤害吸血组件
    /// </summary>
    public class DamageBloodSuckComponent : Component
    {
        /// <summary>
        /// 吸血率,最大为100%
        /// </summary>
        private float bloodSuckPct = 0;
        public float BloodSuckPct { 
            get { 
                return bloodSuckPct; 
            } 
            set {
                bloodSuckPct = Mathf.Max(0, value);
                bloodSuckPct = Mathf.Min(bloodSuckPct, 1);
            } 
        }

        /// <summary>
        /// 添加组件所属战斗实体造成伤害后触发的监听事件
        /// </summary>
        public override void Setup()
        {
            var combatEntity = Entity.GetParent<CombatEntity>();
            combatEntity.ListenActionPoint(ActionPointType.PostCauseDamage, OnCauseDamage);
        }
        /// <summary>
        /// 吸血率->BloodSuckPct,最大为100%
        /// </summary>
        /// <param name="action">所造成的伤害</param>
        private void OnCauseDamage(ActionExecution action)
        {
            var damageAction = action as DamageAction;
            var value = damageAction.DamageValue * BloodSuckPct;
            var combatEntity = Entity.GetParent<CombatEntity>();
            if (combatEntity.CureAbility.TryMakeAction(out var cureAction))
            {
                cureAction.Creator = combatEntity;
                cureAction.Target = combatEntity;
                cureAction.CureValue = (int)value;
                cureAction.ApplyCure();
            }
        }
    }
}