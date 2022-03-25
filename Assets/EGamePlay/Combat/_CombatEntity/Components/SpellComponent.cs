using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using EGamePlay;
using EGamePlay.Combat;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 技能施法组件
    /// </summary>
    public class SpellComponent : EGamePlay.Component
    {
        /// <summary>
        /// 战斗实体
        /// 与GetEntity<CombatEntity>()同步
        /// </summary>
        private CombatEntity CombatEntity => GetEntity<CombatEntity>();
        /// <summary>
        /// 默认启用
        /// </summary>
        public override bool DefaultEnable { get; set; } = true;


        public override void Setup()
        {

        }
        /// <summary>
        /// 向目标释放技能,如果正在释放技能,则无法释放当前技能,即无法打断
        /// 可在释放前调用SkillExecution.EndExecution结束释放,即打断之前施法
        /// 调用方法:CombatEntity.CurrentSkillExecution.EndExecute()
        /// </summary>
        /// <param name="spellSkill">需要释放的技能</param>
        /// <param name="targetEntity">目标战斗实体</param>
        public void SpellWithTarget(SkillAbility spellSkill, CombatEntity targetEntity)
        {
            if (CombatEntity.CurrentSkillExecution != null)
                return;

            //Log.Debug($"OnInputTarget {combatEntity}");
            if (CombatEntity.SpellAbility.TryMakeAction(out var action))
            {
                action.SkillAbility = spellSkill;
                action.SkillExecution = spellSkill.CreateExecution() as SkillExecution;
                action.SkillTargets.Add(targetEntity);
                action.SkillExecution.InputTarget = targetEntity;
                action.SpellSkill();
            }
        }
        /// <summary>
        /// 向目标点释放技能,如果正在释放技能,则无法释放当前技能,即无法打断        
        /// 可在释放前调用SkillExecution.EndExecution结束释放,即打断之前施法
        /// 调用方法:CombatEntity.CurrentSkillExecution.EndExecute()
        /// </summary>
        /// <param name="spellSkill">需要释放的技能</param>
        /// <param name="point">目标点</param>
        public void SpellWithPoint(SkillAbility spellSkill, Vector3 point)
        {
            if (CombatEntity.CurrentSkillExecution != null)
                return;

            //Log.Debug($"OnInputPoint {point}");
            if (CombatEntity.SpellAbility.TryMakeAction(out var action))
            {
                action.SkillAbility = spellSkill;
                action.SkillExecution = spellSkill.CreateExecution() as SkillExecution;
                action.SkillExecution.InputPoint = point;
                action.SpellSkill();
            }
        }
        /// <summary>
        /// 向目标方向释放技能,如果正在释放技能,则无法释放当前技能,即无法打断        
        /// 可在释放前调用SkillExecution.EndExecution结束释放,即打断之前施法
        /// 调用方法:CombatEntity.CurrentSkillExecution.EndExecute()
        /// </summary>
        /// <param name="spellSkill">需要释放的技能</param>
        /// <param name="direction">方向</param>
        /// <param name="point">目标点</param>
        public void SpellWithDirect(SkillAbility spellSkill, Vector3 direction, Vector3 point)
        {
            if (CombatEntity.CurrentSkillExecution != null)
                return;
            //Log.Debug($"OnInputDirect {direction}");
            if (CombatEntity.SpellAbility.TryMakeAction(out var action))
            {
                action.SkillAbility = spellSkill;
                action.SkillExecution = spellSkill.CreateExecution() as SkillExecution;
                action.SkillExecution.InputPoint = point;
                action.SkillExecution.InputDirection = direction;
                action.SpellSkill();
            }
        }
    }
}