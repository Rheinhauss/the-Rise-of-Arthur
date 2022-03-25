﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GameUtils;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 时间触发组件
    /// </summary>
    public class EffectTimeTriggerComponent : Component
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
        /// 时间表达式
        /// </summary>
        public string TimeValueExpression { get; set; }
        /// <summary>
        /// 触发计时器
        /// </summary>
        public GameTimer TriggerTimer { get; set; }


        //public override void Setup()
        //{

        //}

        //public override void Update()
        //{
        //    if (TriggerTimer != null && TriggerTimer.IsFinished == false)
        //    {
        //        TriggerTimer.UpdateAsFinish(Time.deltaTime, GetEntity<ExecutionEffect>().ApplyEffect);
        //    }
        //}

        //public override void OnEnable()
        //{
        //    //Log.Debug(GetEntity<LogicEntity>().Effect.Interval);

        //    if (!string.IsNullOrEmpty(TimeValueExpression))
        //    {
        //        var expression = ExpressionHelper.TryEvaluate(TimeValueExpression);
        //        TriggerTime = (int)expression.Value / 1000f;
        //        TriggerTimer = new GameTimer(TriggerTime);
        //    }
        //    else if (TriggerTime > 0)
        //    {
        //        TriggerTimer = new GameTimer(TriggerTime);
        //    }
        //    else
        //    {

        //    }
        //}
    }
}