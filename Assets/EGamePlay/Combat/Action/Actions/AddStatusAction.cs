using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using EGamePlay.Combat;
using UnityEngine;
using GameUtils;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 施加状态行动能力
    /// </summary>
    public class AddStatusActionAbility : ActionAbility<AddStatusAction>
    {

    }

    /// <summary>
    /// 施加状态行动
    /// </summary>
    public class AddStatusAction : ActionExecution
    {
        /// <summary>
        /// 对应能力实体
        /// </summary>
        public AbilityEntity SourceAbility { get; set; }
        /// <summary>
        /// 施加状态效果
        /// </summary>
        public AddStatusEffect AddStatusEffect => AbilityEffect.EffectConfig as AddStatusEffect;
        /// <summary>
        /// 状态能力
        /// </summary>
        public StatusAbility Status { get; set; }


        /// <summary>
        /// 前置处理
        /// </summary>
        private void PreProcess()
        {

        }
        /// <summary>
        /// 应用施加能力
        /// </summary>
        public void ApplyAddStatus()
        {
            PreProcess();

#if EGAMEPLAY_EXCEL
            var statusConfig = AddStatusEffect.AddStatusConfig;
            var canStack = statusConfig.CanStack == "是";
            var enabledLogicTrigger = statusConfig.EnabledLogicTrigger();
#else
            var statusConfig = AddStatusEffect.AddStatus;
            var canStack = statusConfig.CanStack;
            var enabledLogicTrigger = statusConfig.EnabledLogicTrigger;
#endif
            //已存在对应状态
            if (Target.HasStatus(statusConfig.ID))
            {
                //时间叠加
                if (statusConfig.TimeStack)
                {
                    //能叠加持续时间
                    var status = Target.GetStatus(statusConfig.ID);
                    var statusLifeTimer = status.GetComponent<StatusLifeTimeComponent>().LifeTimer;
                    statusLifeTimer.MaxTime += AddStatusEffect.Duration / 1000f;
                }
                //状态替换
                else if (statusConfig.ReplaceStatus)
                {
                    //直接替换之前的状态
                    if (statusConfig.ReplaceType == ReplaceType.DirectReplace)
                    {
                        Target.GetStatus(statusConfig.ID).EndAbility(); ;
                        AddStatus(statusConfig);
                    }
                    //选择状态中剩余时间最长的状态
                    else if (statusConfig.ReplaceType == ReplaceType.DirectReplace)
                    {
                        StatusAbility originStatus = Target.GetStatus(statusConfig.ID);
                        GameTimer originTimer = originStatus.GetComponent<StatusLifeTimeComponent>().LifeTimer;
                        float originTime = originTimer.MaxTime - originTimer.Time;
                        ////origin更长
                        //if(originTime > AddStatusEffect.Duration)
                        //{
                        //    //不做任何处理
                        //}
                        ////new更长
                        //else
                        if (originTime <= AddStatusEffect.Duration)
                        {
                            //new 替换 origin
                            Target.GetStatus(statusConfig.ID).EndAbility(); ;
                            AddStatus(statusConfig);
                        }
                    }
                }
                //层数叠加
                else if (canStack)
                {
                    //状态之间独立，各自计算时间
                    if (statusConfig.IsIndep)
                    {
                        //满层，清除第一层
                        if (Target.GetStatusNum(statusConfig.ID) > statusConfig.MaxStack)
                        {
                            Target.GetStatus(statusConfig.ID).EndAbility();
                        }
                        AddStatus(statusConfig);
                    }
                    //状态之间不独立，统一计算时间
                    else
                    {
                        //满层，清除第一层
                        if (Target.GetStatusNum(statusConfig.ID) > statusConfig.MaxStack)
                        {
                            Target.GetStatus(statusConfig.ID).EndAbility();
                        }
                        //重置时间
                        foreach (StatusAbility statusAbility in Target.TypeIdStatuses[statusConfig.ID])
                        {
                            GameTimer gameTimer = statusAbility.GetComponent<StatusLifeTimeComponent>().LifeTimer;
                            gameTimer.MaxTime = AddStatusEffect.Duration / 1000f;
                            gameTimer.Reset();
                        }
                        AddStatus(statusConfig);
                    }
                }
                //都未选择,则重置对应状态时间为当前选择的状态的时间
                else
                {
                    var status = Target.GetStatus(statusConfig.ID);
                    var statusLifeTimer = status.GetComponent<StatusLifeTimeComponent>().LifeTimer;
                    statusLifeTimer.MaxTime = AddStatusEffect.Duration / 1000f;
                    statusLifeTimer.Reset();
                }
            }
            else
            {
                AddStatus(statusConfig);
            }




            PostProcess();

            ApplyAction();
        }

        /// <summary>
        /// 后置处理
        /// </summary>
        private void PostProcess()
        {
            Creator.TriggerActionPoint(ActionPointType.PostGiveStatus, this);
            Target.TriggerActionPoint(ActionPointType.PostReceiveStatus, this);
        }

        private void AddStatus(StatusConfigObject statusConfig)
        {
            var enabledLogicTrigger = statusConfig.EnabledLogicTrigger;
            Status = Target.AttachStatus<StatusAbility>(statusConfig);
            Status.OwnerEntity = Creator;
            Status.Level = SourceAbility.Level;
            Status.Duration = (int)AddStatusEffect.Duration;
            //Log.Debug($"ApplyEffectAssign AddStatusEffect {Status}");

            if (enabledLogicTrigger)
            {
                Status.ProccessInputKVParams(AddStatusEffect.Params);
            }

            Status.AddComponent<StatusLifeTimeComponent>();
            Status.TryActivateAbility();
        }
    }
}