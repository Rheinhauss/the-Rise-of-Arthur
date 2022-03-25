using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 效果伤害组件
    /// </summary>
    public class EffectDamageComponent : Component
    {
        /// <summary>
        /// 伤害效果
        /// </summary>
        public DamageEffect DamageEffect { get; set; }
        /// <summary>
        /// 伤害值属性
        /// </summary>
        public string DamageValueProperty { get; set; }


        public override void Setup()
        {
            DamageEffect = GetEntity<AbilityEffect>().EffectConfig as DamageEffect;
            DamageValueProperty = DamageEffect.DamageValueFormula;
        }
        /// <summary>
        /// 获取伤害值
        /// </summary>
        /// <returns></returns>
        public int GetDamageValue()
        {
            return ParseDamage();
        }
        /// <summary>
        /// 计算伤害值
        /// </summary>
        /// <returns></returns>
        private int ParseDamage()
        {
            var expression = ExpressionHelper.ExpressionParser.EvaluateExpression(DamageValueProperty);
            if (expression.Parameters.ContainsKey("攻击力"))
            {
                expression.Parameters["攻击力"].Value = GetEntity<AbilityEffect>().OwnerEntity.UnitPropertyEntity.AttackPower.Value;
            }
            return Mathf.CeilToInt((float)expression.Value);
        }
    }
}