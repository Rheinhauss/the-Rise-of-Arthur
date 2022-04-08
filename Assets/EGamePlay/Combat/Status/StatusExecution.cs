using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using EGamePlay.Combat;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 状态能力执行体
    /// </summary>
    public abstract class StatusExecution : AbilityExecution
    {
        public StatusAbility StatusAbility { get { return AbilityEntity as StatusAbility; } }
        public CombatEntity InputTarget { get; set; }
        public CombatEntity InputCombatEntity { get; set; }
    }
}