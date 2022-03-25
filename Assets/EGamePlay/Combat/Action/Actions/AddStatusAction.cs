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
    /// ʩ��״̬�ж�����
    /// </summary>
    public class AddStatusActionAbility : ActionAbility<AddStatusAction>
    {

    }

    /// <summary>
    /// ʩ��״̬�ж�
    /// </summary>
    public class AddStatusAction : ActionExecution
    {
        /// <summary>
        /// ��Ӧ����ʵ��
        /// </summary>
        public AbilityEntity SourceAbility { get; set; }
        /// <summary>
        /// ʩ��״̬Ч��
        /// </summary>
        public AddStatusEffect AddStatusEffect => AbilityEffect.EffectConfig as AddStatusEffect;
        /// <summary>
        /// ״̬����
        /// </summary>
        public StatusAbility Status { get; set; }


        /// <summary>
        /// ǰ�ô���
        /// </summary>
        private void PreProcess()
        {

        }
        /// <summary>
        /// Ӧ��ʩ������
        /// </summary>
        public void ApplyAddStatus()
        {
            PreProcess();

#if EGAMEPLAY_EXCEL
            var statusConfig = AddStatusEffect.AddStatusConfig;
            var canStack = statusConfig.CanStack == "��";
            var enabledLogicTrigger = statusConfig.EnabledLogicTrigger();
#else
            var statusConfig = AddStatusEffect.AddStatus;
            var canStack = statusConfig.CanStack;
            var enabledLogicTrigger = statusConfig.EnabledLogicTrigger;
#endif
            //�Ѵ��ڶ�Ӧ״̬
            if (Target.HasStatus(statusConfig.ID))
            {
                //ʱ�����
                if (statusConfig.TimeStack)
                {
                    //�ܵ��ӳ���ʱ��
                    var status = Target.GetStatus(statusConfig.ID);
                    var statusLifeTimer = status.GetComponent<StatusLifeTimeComponent>().LifeTimer;
                    statusLifeTimer.MaxTime += AddStatusEffect.Duration / 1000f;
                }
                //״̬�滻
                else if (statusConfig.ReplaceStatus)
                {
                    //ֱ���滻֮ǰ��״̬
                    if (statusConfig.ReplaceType == ReplaceType.DirectReplace)
                    {
                        Target.GetStatus(statusConfig.ID).EndAbility(); ;
                        AddStatus(statusConfig);
                    }
                    //ѡ��״̬��ʣ��ʱ�����״̬
                    else if (statusConfig.ReplaceType == ReplaceType.DirectReplace)
                    {
                        StatusAbility originStatus = Target.GetStatus(statusConfig.ID);
                        GameTimer originTimer = originStatus.GetComponent<StatusLifeTimeComponent>().LifeTimer;
                        float originTime = originTimer.MaxTime - originTimer.Time;
                        ////origin����
                        //if(originTime > AddStatusEffect.Duration)
                        //{
                        //    //�����κδ���
                        //}
                        ////new����
                        //else
                        if (originTime <= AddStatusEffect.Duration)
                        {
                            //new �滻 origin
                            Target.GetStatus(statusConfig.ID).EndAbility(); ;
                            AddStatus(statusConfig);
                        }
                    }
                }
                //��������
                else if (canStack)
                {
                    //״̬֮����������Լ���ʱ��
                    if (statusConfig.IsIndep)
                    {
                        //���㣬�����һ��
                        if (Target.GetStatusNum(statusConfig.ID) > statusConfig.MaxStack)
                        {
                            Target.GetStatus(statusConfig.ID).EndAbility();
                        }
                        AddStatus(statusConfig);
                    }
                    //״̬֮�䲻������ͳһ����ʱ��
                    else
                    {
                        //���㣬�����һ��
                        if (Target.GetStatusNum(statusConfig.ID) > statusConfig.MaxStack)
                        {
                            Target.GetStatus(statusConfig.ID).EndAbility();
                        }
                        //����ʱ��
                        foreach (StatusAbility statusAbility in Target.TypeIdStatuses[statusConfig.ID])
                        {
                            GameTimer gameTimer = statusAbility.GetComponent<StatusLifeTimeComponent>().LifeTimer;
                            gameTimer.MaxTime = AddStatusEffect.Duration / 1000f;
                            gameTimer.Reset();
                        }
                        AddStatus(statusConfig);
                    }
                }
                //��δѡ��,�����ö�Ӧ״̬ʱ��Ϊ��ǰѡ���״̬��ʱ��
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
        /// ���ô���
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