using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 能力效果组件
    /// </summary>
    public class AbilityEffectComponent : Component
    {
        /// <summary>
        /// 默认启用
        /// </summary>
        public override bool DefaultEnable { get; set; } = false;
        /// <summary>
        /// 能力效果列表
        /// </summary>
        public List<AbilityEffect> AbilityEffects { get; private set; } = new List<AbilityEffect>();
        /// <summary>
        /// 造成伤害的能力效果
        /// </summary>
        public AbilityEffect DamageAbilityEffect { get; set; }
        /// <summary>
        /// 回复的能力效果
        /// </summary>
        public AbilityEffect CureAbilityEffect { get; set; }


        public override void Setup(object initData)
        {
            var effects = initData as List<Effect>;
            foreach (var item in effects)
            {
                //Log.Debug($"AbilityEffectComponent Setup {item}");
                var abilityEffect = Entity.AddChild<AbilityEffect>(item);
                AddEffect(abilityEffect);

                if (abilityEffect.EffectConfig is DamageEffect)
                {
                    DamageAbilityEffect = abilityEffect;
                }
                if (abilityEffect.EffectConfig is CureEffect)
                {
                    CureAbilityEffect = abilityEffect;
                }
            }
        }
        /// <summary>
        /// 启用时调用
        /// </summary>
        public override void OnEnable()
        {
            foreach (var item in AbilityEffects)
            {
                item.EnableEffect();
            }
        }
        /// <summary>
        /// 禁用时调用
        /// </summary>
        public override void OnDisable()
        {
            foreach (var item in AbilityEffects)
            {
                item.DisableEffect();
            }
        }
        /// <summary>
        /// 添加效果->添加至能力效果列表
        /// </summary>
        /// <param name="abilityEffect"></param>
        public void AddEffect(AbilityEffect abilityEffect)
        {
            AbilityEffects.Add(abilityEffect);
        }

        public void AddEffect(Effect effect)
        {
            var abilityEffect = Entity.AddChild<AbilityEffect>(effect);
            AddEffect(abilityEffect);
        }

        public void RemoveEffect(Effect effect)
        {
            foreach(var item in AbilityEffects)
            {
                if(item.EffectConfig == effect)
                {
                    AbilityEffects.Remove(item);
                    break;
                }
            }
        }

        //public void SetOneEffect(AbilityEffect abilityEffect)
        //{
        //    AbilityEffects.Clear();
        //    AbilityEffects.Add(abilityEffect);
        //}

        //public void FillEffects(List<AbilityEffect> abilityEffects)
        //{
        //    AbilityEffects.Clear();
        //    AbilityEffects.AddRange(abilityEffects);
        //}
        /// <summary>
        /// 获取能力效果列表
        /// </summary>
        /// <param name="index">序号</param>
        /// <returns></returns>
        public AbilityEffect GetEffect(int index = 0)
        {
            return AbilityEffects[index];
        }

        //public void ApplyAllEffectsTo(CombatEntity targetEntity)
        //{
        //    if (AbilityEffects.Count > 0)
        //    {
        //        foreach (var abilityEffect in AbilityEffects)
        //        {
        //            abilityEffect.ApplyEffectTo(targetEntity);
        //        }
        //    }
        //}

        //public void ApplyEffectByIndex(CombatEntity targetEntity, int index)
        //{
        //    AbilityEffects[index].ApplyEffectTo(targetEntity);
        //}
    }
}