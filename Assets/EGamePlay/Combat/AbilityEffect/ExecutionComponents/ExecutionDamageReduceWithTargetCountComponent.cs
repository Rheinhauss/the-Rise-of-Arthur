﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GameUtils;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 
    /// </summary>
    public class ExecutionDamageReduceWithTargetCountComponent : Component
    {
        public int TargetCounter { get; set; }
        public float ReducePercent { get; set; }
        public float MinPercent { get; set; }


        public override void Setup(object initData)
        {
            var customEffect = (initData as AbilityEffect).EffectConfig as CustomEffect;
        }

        public void AddOneTarget()
        {
            TargetCounter++;
        }

        public float GetDamagePercent()
        {
            return Mathf.Max(MinPercent, 1 - ReducePercent * TargetCounter);
        }
    }
}