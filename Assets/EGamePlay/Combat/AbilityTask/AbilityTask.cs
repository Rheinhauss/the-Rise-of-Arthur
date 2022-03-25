using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using ET;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 能力任务状态:准备，执行，结束
    /// </summary>
    public enum AbilityTaskState { Ready, Executing, Ended }
    /// <summary>
    /// 能力任务
    /// </summary>
    public abstract class AbilityTask : Entity
    {
        /// <summary>
        /// 任务初始化数据
        /// </summary>
        public object taskInitData { get; set; }
        /// <summary>
        /// 任务所处状态
        /// </summary>
        public AbilityTaskState TaskState { get; set; }


        public override void Awake(object initData)
        {
            taskInitData = initData;
        }
        /// <summary>
        /// 异步执行任务
        /// </summary>
        /// <returns></returns>
        public virtual async ETTask ExecuteTaskAsync()
        {
            await Task.Delay(1000);
        }
    }
}