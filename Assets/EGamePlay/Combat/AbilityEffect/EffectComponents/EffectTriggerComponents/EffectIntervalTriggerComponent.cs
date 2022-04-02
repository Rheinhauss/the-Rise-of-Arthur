using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GameUtils;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 间隔触发组件
    /// </summary>
    public class EffectIntervalTriggerComponent : Component
    {
        /// <summary>
        /// 默认启用
        /// </summary>
        public override bool DefaultEnable { get; set; } = false;
        /// <summary>
        /// 间隔时间
        /// </summary>
        public string IntervalValue { get; set; }
        /// <summary>
        /// 间隔时间计时器
        /// </summary>
        public GameTimer IntervalTimer { get; set; }


        public override void Setup()
        {

        }
        /// <summary>
        /// 计时
        /// </summary>
        public override void Update()
        {
            if (IntervalTimer != null)
            {
                IntervalTimer.UpdateAsRepeat(Time.deltaTime, GetEntity<AbilityEffect>().ApplyEffectToParent);
            }
        }
        /// <summary>
        /// 启用时调用
        /// </summary>
        public override void OnEnable()
        {
            //Log.Debug(GetEntity<LogicEntity>().Effect.Interval);
            var intervalExpression = IntervalValue;
            var expression = ExpressionHelper.TryEvaluate(intervalExpression);
            if (expression.Parameters.ContainsKey("技能等级"))
            {
                expression.Parameters["技能等级"].Value = GetEntity<AbilityEffect>().GetParent<StatusAbility>().Level;
            }
#if EGAMEPLAY_EXCEL
            var interval = (float)expression.Value;
            if (interval > 10)
            {
                interval = interval / 1000f;
            }
            IntervalTimer = new GameTimer(interval);
#else
            var interval = (int)expression.Value / 1000f;
            IntervalTimer = new GameTimer(interval);
#endif
        }
    }
}