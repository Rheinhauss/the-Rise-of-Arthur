using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 能力实体
    /// </summary>
    /// <typeparam name="T">AbilityExecution->能力执行体</typeparam>
    public abstract class AbilityEntity<T> : AbilityEntity where T : AbilityExecution
    {
        /// <summary>
        /// 创建能力执行体
        /// </summary>
        /// <returns></returns>
        public virtual new T CreateExecution() 
        {
            return base.CreateExecution() as T;
        }
    }

    /// <summary>
    /// 能力实体，存储着某个英雄某个能力的数据和状态
    /// </summary>
    public abstract class AbilityEntity : Entity
    {
        /// <summary>
        /// 所属战斗实体
        /// </summary>
        public virtual CombatEntity OwnerEntity { get => GetParent<CombatEntity>(); set { } }
        /// <summary>
        /// 父对象->战斗实体
        /// </summary>
        public CombatEntity ParentEntity => GetParent<CombatEntity>();
        /// <summary>
        /// 能力效果组件
        /// </summary>
        public AbilityEffectComponent AbilityEffectComponent => GetComponent<AbilityEffectComponent>();
        /// <summary>
        /// 能力效果列表->能力效果组件的列表
        /// </summary>
        public List<AbilityEffect> AbilityEffects => GetComponent<AbilityEffectComponent>().AbilityEffects;
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get; set; } = true;
        /// <summary>
        /// 配置物体
        /// </summary>
        public object ConfigObject { get; set; }
        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get; set; } = 1;


        public override void Awake(object initData)
        {
            ConfigObject = initData;
        }

        /// <summary>
        /// 尝试激活能力
        /// </summary>
        public virtual void TryActivateAbility()
        {
            //Log.Debug($"{GetType().Name}->TryActivateAbility");
            ActivateAbility();
        }

        /// <summary>
        /// 激活能力
        /// </summary>
        public virtual void ActivateAbility()
        {
            
        }

        /// <summary>
        /// 禁用能力
        /// </summary>
        public virtual void DeactivateAbility()
        {

        }

        /// <summary>
        /// 结束能力
        /// </summary>
        public virtual void EndAbility()
        {
            Destroy(this);
        }

        /// <summary>
        /// 创建能力执行体
        /// </summary>
        /// <returns></returns>
        public virtual AbilityExecution CreateExecution()
        {
            return null;
        }
    }
}
