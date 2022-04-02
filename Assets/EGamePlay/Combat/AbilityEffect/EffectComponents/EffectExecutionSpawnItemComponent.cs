using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 碰撞生成点数据
    /// </summary>
    public class ColliderSpawnData
    {
        /// <summary>
        /// 是否开始
        /// </summary>
        public bool HasStart;
        /// <summary>
        /// 执行事件发出体
        /// </summary>
        public ExecutionEventEmitter ColliderSpawnEmitter;
    }

    /// <summary>
    /// 存放碰撞事件发出体数据的组件
    /// </summary>
    public class EffectExecutionSpawnItemComponent : Component
    {
        public ColliderSpawnData ColliderSpawnData { get; set; }
    }
}