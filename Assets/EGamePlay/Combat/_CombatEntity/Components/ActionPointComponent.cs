using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 行动点，一次战斗行动<see cref="ActionExecution"/>会触发战斗实体一系列的行动点<see cref="ActionPoint"/>
    /// </summary>
    public sealed class ActionPoint
    {
        /// <summary>
        /// 战斗行动的监听器->Action委托,参数ActionExecution
        /// </summary>
        public List<Action<ActionExecution>> Listeners { get; set; } = new List<Action<ActionExecution>>();

        /// <summary>
        /// 添加委托事件
        /// </summary>
        /// <param name="action">委托事件</param>
        public void AddListener(Action<ActionExecution> action)
        {
            Listeners.Add(action);
        }

        /// <summary>
        /// 移除委托事件
        /// </summary>
        /// <param name="action">委托事件</param>
        public void RemoveListener(Action<ActionExecution> action)
        {
            Listeners.Remove(action);
        }

        /// <summary>
        /// 触发所有监听器->执行所有监听委托事件
        /// </summary>
        /// <param name="actionExecution">战斗行动事件</param>
        public void TriggerAllActions(ActionExecution actionExecution)
        {
            if (Listeners.Count == 0)
            {
                return;
            }
            for (int i = Listeners.Count - 1; i >= 0; i--)
            {
                var item = Listeners[i];
                item.Invoke(actionExecution);
            }
        }
    }

    /// <summary>
    /// Flags关键字允许我们在使用.net 枚举变量时,使用多个组合值
    /// 行动点类型
    /// </summary>
    [Flags]
    public enum ActionPointType
    {
        [LabelText("（空）")]
        None = 0,

        [LabelText("造成伤害前")]
        PreCauseDamage = 1 << 1,
        [LabelText("承受伤害前")]
        PreReceiveDamage = 1 << 2,

        [LabelText("造成伤害后")]
        PostCauseDamage = 1 << 3,
        [LabelText("承受伤害后")]
        PostReceiveDamage = 1 << 4,

        [LabelText("给予治疗后")]
        PostGiveCure = 1 << 5,
        [LabelText("接受治疗后")]
        PostReceiveCure = 1 << 6,

        [LabelText("赋给效果--生效")]
        AssignEffect = 1 << 7,
        [LabelText("接受效果--生效")]
        ReceiveEffect = 1 << 8,

        [LabelText("赋加状态后--添加")]
        PostGiveStatus = 1 << 9,
        [LabelText("承受状态后--添加")]
        PostReceiveStatus = 1 << 10,

        [LabelText("给予普攻前")]
        PreGiveAttack = 1 << 11,
        [LabelText("给予普攻后")]
        PostGiveAttack = 1 << 12,

        [LabelText("遭受普攻前")]
        PreReceiveAttack = 1 << 13,
        [LabelText("遭受普攻后")]
        PostReceiveAttack = 1 << 14,

        [LabelText("起跳前")]
        PreJumpTo= 1 << 15,
        [LabelText("起跳后")]
        PostJumpTo = 1 << 16,

        [LabelText("施法前")]
        PreSpell = 1 << 17,
        [LabelText("施法后")]
        PostSpell = 1 << 18,

        Max,
    }

    /// <summary>
    /// 行动点管理器，在这里管理一个战斗实体所有行动点的添加监听、移除监听、触发流程
    /// </summary>
    public sealed class ActionPointComponent : Component
    {
        /// <summary>
        /// 行动点字典->(行动点类型,行动点对象)
        /// </summary>
        private Dictionary<ActionPointType, ActionPoint> ActionPoints { get; set; } = new Dictionary<ActionPointType, ActionPoint>();


        public override void Setup()
        {
            base.Setup();
        }

        /// <summary>
        /// 对应行动点添加战斗行动委托事件
        /// </summary>
        /// <param name="actionPointType">行动点类型</param>
        /// <param name="action">委托事件</param>
        public void AddListener(ActionPointType actionPointType, Action<ActionExecution> action)
        {
            if (!ActionPoints.ContainsKey(actionPointType))
            {
                ActionPoints.Add(actionPointType, new ActionPoint());
            }
            ActionPoints[actionPointType].AddListener(action);
        }

        /// <summary>
        /// 对应行动点移除战斗行动委托事件
        /// </summary>
        /// <param name="actionPointType">行动点类型</param>
        /// <param name="action">委托事件</param>
        public void RemoveListener(ActionPointType actionPointType, Action<ActionExecution> action)
        {
            if (ActionPoints.ContainsKey(actionPointType))
            {
                ActionPoints[actionPointType].RemoveListener(action);
            }
        }
        /// <summary>
        /// 获取对应行动点类型对应的行动点对象s
        /// </summary>
        /// <param name="actionPointType">行动点类型</param>
        /// <returns></returns>
        public ActionPoint GetActionPoint(ActionPointType actionPointType)
        {
            if (ActionPoints.TryGetValue(actionPointType, out var actionPoint)) 
                return actionPoint;
            return actionPoint;
        }

        /// <summary>
        /// 触发对应行动点类型的所有委托事件
        /// </summary>
        /// <param name="actionPointType">需触发的行动点类型</param>
        /// <param name="actionExecution">委托事件的参数</param>
        public void TriggerActionPoint(ActionPointType actionPointType, ActionExecution actionExecution)
        {
            if (ActionPoints.TryGetValue(actionPointType, out var actionPoint))
            {
                actionPoint.TriggerAllActions(actionExecution);
            }
        }
    }
}