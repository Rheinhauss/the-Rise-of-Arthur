using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 动画数据
    /// </summary>
    public class AnimationData
    {
        /// <summary>
        /// 是否开始
        /// </summary>
        public bool HasStart;
        /// <summary>
        /// 是否结束
        /// </summary>
        public bool HasEnded;
        /// <summary>
        /// 开始时间
        /// </summary>
        public float StartTime;
        /// <summary>
        /// 结束时间
        /// </summary>
        public float EndTime;
        /// <summary>
        /// 持续时间
        /// </summary>
        public float Duration;
        /// <summary>
        /// 动画序列
        /// </summary>
        public AnimationClip AnimationClip;
    }

    /// <summary>
    /// 存放效果播放的动画数据的组件
    /// </summary>
    public class EffectExecutionAnimationComponent : Component
    {
        public AnimationData AnimationData { get; set; }
    }
}