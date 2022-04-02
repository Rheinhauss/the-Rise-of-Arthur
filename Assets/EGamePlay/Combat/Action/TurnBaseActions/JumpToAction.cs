using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EGamePlay;
using EGamePlay.Combat;
using ET;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 起跳行动能力
    /// </summary>
    public class JumpToActionAbility : ActionAbility<JumpToAction>
    {

    }
    /// <summary>
    /// 起跳行动
    /// </summary>
    public class JumpToAction : ActionExecution
    {
        /// <summary>
        /// 前置处理
        /// </summary>
        private void PreProcess()
        {
            Creator.TriggerActionPoint(ActionPointType.PreJumpTo, this);
        }
        /// <summary>
        /// 异步应用跳跃
        /// </summary>
        /// <returns></returns>
        public async ETTask ApplyJumpTo()
        {
            PreProcess();

            await TimeHelper.WaitAsync(Creator.JumpToTime);

            PostProcess();

            if (Creator.SpellAttackAbility.TryMakeAction(out var attackAction))
            {
                attackAction.Target = Target;
                await attackAction.ApplyAttackAwait();
            }

            ApplyAction();
        }

        /// <summary>
        /// 后置处理
        /// </summary>
        private void PostProcess()
        {
            Creator.TriggerActionPoint(ActionPointType.PostJumpTo, this);
        }
    }
}