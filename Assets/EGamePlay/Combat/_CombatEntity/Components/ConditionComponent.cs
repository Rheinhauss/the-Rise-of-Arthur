using System.Collections.Generic;
using System;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 条件管理组件，在这里管理一个战斗实体所有条件达成事件的添加监听、移除监听、触发流程
    /// </summary>
    public sealed class ConditionComponent : Component
    {
        /// <summary>
        /// <监听事件,条件实体>
        /// </summary>
        private Dictionary<Action, ConditionEntity> Conditions { get; set; } = new Dictionary<Action, ConditionEntity>();


        public override void Setup()
        {
            base.Setup();
        }
        /// <summary>
        /// 添加监听
        /// </summary>
        /// <param name="conditionType">条件类型</param>
        /// <param name="action">委托事件</param>
        /// <param name="paramObj">condition的初始化参数</param>
        public void AddListener(ConditionType conditionType, Action action, object paramObj = null)
        {
            switch (conditionType)
            {
                case ConditionType.WhenInTimeNoDamage:
                    var time = (float)paramObj;
                    var condition = Entity.AddChild<WhenInTimeNoDamageCondition>(time);
                    Conditions.Add(action, condition);
                    condition.StartListen(action);
                    break;
                case ConditionType.WhenHPLower:

                    break;
                case ConditionType.WhenHPPctLower:

                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 移除监听
        /// </summary>
        /// <param name="conditionType">条件类型</param>
        /// <param name="action">需要移除的监听事件</param>
        public void RemoveListener(ConditionType conditionType, Action action)
        {
            if (Conditions.ContainsKey(action))
            {
                Entity.Destroy(Conditions[action]);
                Conditions.Remove(action);
            }
        }
    }
}