using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EGamePlay;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 治疗行动能力
    /// </summary>
    public class CureActionAbility : ActionAbility<CureAction>
    {

    }

    /// <summary>
    /// 治疗行动
    /// </summary>
    public class CureAction : ActionExecution
    {
        /// <summary>
        /// 治疗效果
        /// </summary>
        public CureEffect CureEffect => AbilityEffect.EffectConfig as CureEffect;
        /// <summary>
        /// 治疗数值
        /// </summary>
        public int CureValue { get; set; }



        /// <summary>
        /// 前置处理
        /// </summary>
        private void PreProcess()
        {
            if (AbilityEffect != null)
            {
                CureValue = AbilityEffect.GetComponent<EffectCureComponent>().GetCureValue();
            }
        }
        /// <summary>
        /// 应用治疗
        /// </summary>
        public void ApplyCure()
        {
            //Log.Debug("CureAction ApplyCure");
            PreProcess();

            if (Target.UnitPropertyEntity.HP.IsFull() == false)
            {
                Target.ReceiveCure(this);
            }

            PostProcess();

            ApplyAction();
        }

        /// <summary>
        /// 后置处理
        /// </summary>
        private void PostProcess()
        {
            Creator.TriggerActionPoint(ActionPointType.PostGiveCure, this);
            Target.TriggerActionPoint(ActionPointType.PostReceiveCure, this);
        }
    }
}