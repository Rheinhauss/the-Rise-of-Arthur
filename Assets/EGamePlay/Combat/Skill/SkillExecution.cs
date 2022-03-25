﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using EGamePlay.Combat;
using ET;
using Log = EGamePlay.Log;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 技能执行体
    /// </summary>
    [EnableUpdate]
    public partial class SkillExecution : AbilityExecution
    {
        public SkillAbility SkillAbility { get { return AbilityEntity as SkillAbility; } }
        public List<CombatEntity> SkillTargets { get; set; } = new List<CombatEntity>();
        public CombatEntity InputTarget { get; set; }
        public Vector3 InputPoint { get; set; }
        public Vector3 InputDirection { get; set; }
        public long OriginTime { get; set; }
    }
}