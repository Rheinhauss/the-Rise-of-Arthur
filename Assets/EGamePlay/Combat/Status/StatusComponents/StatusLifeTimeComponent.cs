using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtils;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 状态的生命周期组件
    /// </summary>
    public class StatusLifeTimeComponent : Component
    {
        public override bool DefaultEnable { get; set; } = true;
        public GameTimer LifeTimer { get; set; }

        public StatusAbility OwnerStatus => GetEntity<StatusAbility>();

        public bool IsTemp => OwnerStatus.StatusConfig.IsTemp;



        public override void Setup()
        {
            var lifeTime = OwnerStatus.GetDuration() / 1000f;
            LifeTimer = new GameTimer(lifeTime);
        }

        public override void Update()
        {
            if (IsTemp)
                return;
            if (LifeTimer.IsRunning)
            {
                LifeTimer.UpdateAsFinish(Time.deltaTime, OnLifeTimeFinish);
            }
        }

        private void OnLifeTimeFinish()
        {
            OwnerStatus.EndAbility();
        }
    }
}