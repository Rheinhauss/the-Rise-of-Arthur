using System;
using GameUtils;
using ET;
using System.Collections.Generic;
using UnityEngine;

#if !EGAMEPLAY_EXCEL
namespace EGamePlay.Combat
{
    public partial class SkillAbility : AbilityEntity
    {
        public SkillConfigObject SkillConfig { get; set; }
        public bool Spelling { get; set; }
        public GameTimer CooldownTimer { get; } = new GameTimer(1f);
        private List<StatusAbility> ChildrenStatuses { get; set; } = new List<StatusAbility>();
        private List<AbilityEffect> toSelfEffects = new List<AbilityEffect>();
        private AbilityEffectComponent toTargetEffects;

        public void ApplyEffectsToSelf()
        {
            foreach(var item in toSelfEffects)
            {
                item.ApplyEffectToOwner();
            }
        }

        public override void Awake(object initData)
        {
            base.Awake(initData);
            SkillConfig = initData as SkillConfigObject;
            Name = SkillConfig.Name;
            toTargetEffects = AddComponent<AbilityEffectComponent>();
            foreach (var item in SkillConfig.Effects)
            {
                if(item.AddSkillEffectTargetType == AddSkillEffetTargetType.Self)
                {
                    toSelfEffects.Add(AddChild<AbilityEffect>(item));
                }
                else
                {
                    toTargetEffects.AddEffect(item);
                }
            }

#if !SERVER
            ParseAbilityEffects();
#endif
            if (SkillConfig.SkillSpellType == SkillSpellType.Passive)
            {
                TryActivateAbility();
            }
        }
        /// <summary>
        /// Config附加状态效果->被动
        /// </summary>
        public override void ActivateAbility()
        {
            base.ActivateAbility();
            //子状态效果
            if (SkillConfig.EnableChildrenStatuses)
            {
                foreach (var item in SkillConfig.ChildrenStatuses)
                {
                    var status = OwnerEntity.AttachStatus<StatusAbility>(item.StatusConfigObject);
                    status.OwnerEntity = OwnerEntity;
                    status.IsChildStatus = true;
                    status.ChildStatusData = item;
                    status.ProccessInputKVParams(item.Params);
                    status.TryActivateAbility();
                    ChildrenStatuses.Add(status);
                }
            }
        }

        public override void EndAbility()
        {
            base.EndAbility();
            //子状态效果
            if (SkillConfig.EnableChildrenStatuses)
            {
                foreach (var item in ChildrenStatuses)
                {
                    item.EndAbility();
                }
                ChildrenStatuses.Clear();
            }
        }

        public override AbilityExecution CreateExecution()
        {
            var execution = OwnerEntity.AddChild<SkillExecution>(this);
            execution.AddComponent<UpdateComponent>();
            return execution;
        }

        public void AddStatus(Effect effect)
        {
            if(effect.AddSkillEffectTargetType == AddSkillEffetTargetType.Self)
            {
                toSelfEffects.Add(AddChild<AbilityEffect>(effect));
            }
            else
            {
                toTargetEffects.AddEffect(effect);
            }
        }

        public void RemoveStatus(Effect effect)
        {
            if(effect.AddSkillEffectTargetType == AddSkillEffetTargetType.Self)
            {
                foreach(var item in toSelfEffects)
                {
                    if(item.EffectConfig == effect)
                    {
                        toSelfEffects.Remove(item);
                        break;
                    }
                }
            }
            else
            {
                toTargetEffects.RemoveEffect(effect);
            }
        }

    }
}
#endif