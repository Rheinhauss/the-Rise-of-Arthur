using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 效果战斗属性数值修饰器组件
    /// </summary>
    public class EffectAttributeModifyComponent : Component
    {
        /// <summary>
        /// 加入状态效果对象
        /// </summary>
        public AddStatusEffect AddStatusEffect { get; set; }


        public override void Setup()
        {
            AddStatusEffect = GetEntity<AbilityEffect>().EffectConfig as AddStatusEffect;
        }

        public int GetNumericValue()
        {
            return 1;
        }
    }
}