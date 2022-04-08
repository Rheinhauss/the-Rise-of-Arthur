using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 效果回复值组件
    /// </summary>
    public class EffectCureComponent : Component
    {
        /// <summary>
        /// 回复效果
        /// </summary>
        public CureEffect CureEffect { get; set; }
        /// <summary>
        /// 回复值属性
        /// </summary>
        public string CureValueProperty { get; set; }


        public override void Setup()
        {
            CureEffect = GetEntity<AbilityEffect>().EffectConfig as CureEffect;
            CureValueProperty = CureEffect.CureValueFormula;
        }
        /// <summary>
        /// 获取回复值
        /// </summary>
        /// <returns></returns>
        public int GetCureValue()
        {
            return ParseValue();
        }
        /// <summary>
        /// 计算回复值
        /// </summary>
        /// <returns></returns>
        private int ParseValue()
        {
            var expression = ExpressionHelper.ExpressionParser.EvaluateExpression(CureValueProperty);
            if (expression.Parameters.ContainsKey("MaxHP"))
            {
                expression.Parameters["MaxHP"].Value = GetEntity<AbilityEffect>().OwnerEntity.GetChild<UnitPropertyEntity>().HP.MaxValue;
            }
            return Mathf.CeilToInt((float)expression.Value);
        }
    }
}