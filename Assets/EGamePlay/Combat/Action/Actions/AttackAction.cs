using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EGamePlay;
using EGamePlay.Combat;
using ET;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 施法普攻行动能力
    /// </summary>
    public class AttackActionAbility : ActionAbility<AttackAction>
    {

    }

    /// <summary>
    /// 普攻行动
    /// </summary>
    public class AttackAction : ActionExecution
    {
        /// <summary>
        /// 前置处理
        /// </summary>
        private void PreProcess()
        {
            Creator.TriggerActionPoint(ActionPointType.PreGiveAttack, this);
            Target.TriggerActionPoint(ActionPointType.PreReceiveAttack, this);
        }
        /// <summary>
        /// 异步应用攻击等待
        /// </summary>
        /// <returns></returns>
        public async ETTask ApplyAttackAwait()
        {
            PreProcess();
            
            await TimeHelper.WaitAsync(1000);

            ApplyAttack();

            await TimeHelper.WaitAsync(300);

            PostProcess();

            ApplyAction();
        }
        /// <summary>
        /// 应用攻击
        /// </summary>
        public void ApplyAttack()
        {
            var attackExecution = Creator.AttackAbility.CreateExecution();
            attackExecution.AttackAction = this;
            attackExecution.BeginExecute();
        }

        /// <summary>
        /// 后置处理
        /// </summary>
        private void PostProcess()
        {
            Creator.TriggerActionPoint(ActionPointType.PostGiveAttack, this);
            Target.TriggerActionPoint(ActionPointType.PostReceiveAttack, this);
        }
    }
}