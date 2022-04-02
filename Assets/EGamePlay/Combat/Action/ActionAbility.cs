using EGamePlay.Combat;
using System;
using GameUtils;
using ET;
using System.Collections.Generic;
using UnityEngine;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 战斗行动能力
    /// </summary>
    public class ActionAbility : AbilityEntity
    {
        /// <summary>
        /// 生成对应的能力执行体
        /// </summary>
        /// <returns></returns>
        public AbilityExecution MakeAction()
        {
            return CreateExecution();
        }
        /// <summary>
        /// 生成对应的能力执行体
        /// 如果禁用则返回null
        /// </summary>
        /// <returns></returns>
        public AbilityExecution TryMakeAction()
        {
            if (Enable == false)
            {
                return null;
            }
            return CreateExecution();
        }
        /// <summary>
        /// 生成对应的能力执行体abilityExecution
        /// </summary>
        /// <param name="abilityExecution"></param>
        /// <returns>是否启用</returns>
        public bool TryMakeAction(out AbilityExecution abilityExecution)
        {
            if (Enable == false)
            {
                abilityExecution = null;
            }
            else
            {
                abilityExecution = CreateExecution();
            }
            return Enable;
        }
    }
    /// <summary>
    /// 战斗行动能力
    /// </summary>
    /// <typeparam name="T">ActionExecution</typeparam>
    public class ActionAbility<T> : ActionAbility where T : ActionExecution
    {
        /// <summary>
        /// 生成对应能力执行体
        /// </summary>
        /// <returns></returns>
        public override AbilityExecution CreateExecution()
        {
            var execution = OwnerEntity.MakeAction<T>();
            execution.ActionAbility = this;
            return execution;
        }

        /// <summary>
        /// 发动行动
        /// </summary>
        /// <returns></returns>
        public new T MakeAction()
        {
            return CreateExecution() as T;
        }

        /// <summary>
        /// 尝试发动行动
        /// </summary>
        /// <returns></returns>
        public new T TryMakeAction()
        {
            if (Enable == false)
            {
                return null;
            }
            return CreateExecution() as T;
        }

        /// <summary>
        /// 尝试发动行动
        /// </summary>
        /// <param name="abilityExecution"></param>
        /// <returns></returns>
        public bool TryMakeAction(out T abilityExecution)
        {
            if (Enable == false)
            {
                abilityExecution = null;
            }
            else
            {
                abilityExecution = CreateExecution() as T;
            }
            return Enable;
        }
    }
}