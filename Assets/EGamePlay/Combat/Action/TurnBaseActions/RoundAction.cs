using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EGamePlay;
using EGamePlay.Combat;
using ET;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 回合行动能力
    /// </summary>
    public class RoundActionAbility : ActionAbility<RoundAction>
    {

    }

    /// <summary>
    /// 回合行动
    /// </summary>
    public class RoundAction : ActionExecution
    {
        /// <summary>
        /// 回合行动类型
        /// </summary>
        public int RoundActionType { get; set; }


        /// <summary>
        /// 前置处理
        /// </summary>
        private void PreProcess()
        {

        }
        /// <summary>
        /// 异步执行
        /// </summary>
        /// <returns></returns>
        public async ETTask ApplyRound()
        {
            PreProcess();

            if (Creator.JumpToAbility.TryMakeAction(out var jumpToAction))
            {
                jumpToAction.Target = Target;
                await jumpToAction.ApplyJumpTo();
            }

            PostProcess();
        }

        /// <summary>
        /// 后置处理
        /// </summary>
        private void PostProcess()
        {

        }
    }
}