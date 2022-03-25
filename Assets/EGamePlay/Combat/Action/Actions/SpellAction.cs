using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EGamePlay;
using EGamePlay.Combat;
using ET;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 施法行动能力
    /// </summary>
    public class SpellActionAbility : ActionAbility<SpellAction>
    {

    }

    /// <summary>
    /// 施法行动
    /// </summary>
    public class SpellAction : ActionExecution
    {
        /// <summary>
        /// 技能能力
        /// </summary>
        public SkillAbility SkillAbility { get; set; }
        /// <summary>
        /// 技能执行体
        /// </summary>
        public SkillExecution SkillExecution { get; set; }
        /// <summary>
        /// 技能目标战斗实体列表
        /// </summary>
        public List<CombatEntity> SkillTargets { get; set; } = new List<CombatEntity>();
        /// <summary>
        /// 输入目标点
        /// </summary>
        public Vector3 InputPoint { get; set; }
        /// <summary>
        /// 输入目标方位
        /// </summary>
        public float InputDirection { get; set; }


        /// <summary>
        /// 前置处理
        /// </summary>
        private void PreProcess()
        {
            Creator.TriggerActionPoint(ActionPointType.PreSpell, this);
        }
        /// <summary>
        /// 施法
        /// </summary>
        public void SpellSkill()
        {
            PreProcess();
            if (SkillTargets.Count > 0)
            {
                SkillExecution.SkillTargets.AddRange(SkillTargets);
            }
            SkillExecution.Name = SkillAbility.Name;
            SkillExecution.BeginExecute();
            AddComponent<UpdateComponent>();
        }
        /// <summary>
        /// 更新
        /// </summary>
        public override void Update()
        {
            if (SkillExecution != null)
            {
                if (SkillExecution.IsDisposed)
                {
                    PostProcess();
                    ApplyAction();
                }
            }
        }

        /// <summary>
        /// 后置处理
        /// </summary>
        private void PostProcess()
        {
            Creator.TriggerActionPoint(ActionPointType.PostSpell, this);
        }
    }
}