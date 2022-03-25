using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EGamePlay;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 动作行动
    /// </summary>
    public class MotionActionAbility : ActionAbility<MotionAction>
    {

    }

    /// <summary>
    /// 动作行动
    /// </summary>
    public class MotionAction : ActionExecution
    {
        /// <summary>
        /// 动作类型
        /// </summary>
        public int MotionType { get; set; }


        /// <summary>
        /// 前置处理
        /// </summary>
        private void PreProcess()
        {

        }
        /// <summary>
        /// 应用动作
        /// </summary>
        public void ApplyMotion()
        {
            PreProcess();

            PostProcess();
        }

        /// <summary>
        /// 后置处理
        /// </summary>
        private void PostProcess()
        {
            //Creator.TriggerActionPoint(ActionPointType.PostGiveCure, this);
            //Target.TriggerActionPoint(ActionPointType.PostReceiveCure, this);
        }
    }
}