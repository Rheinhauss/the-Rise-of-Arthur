using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 能力执行体，能力执行体是实际创建、执行能力表现，触发能力效果应用的地方
    /// 这里可以存一些表现执行相关的临时的状态数据
    /// </summary>
    public abstract class AbilityExecution : Entity
    {
        /// <summary>
        /// 对应的能力实体
        /// </summary>
        public AbilityEntity AbilityEntity { get; set; }
        /// <summary>
        /// 能力效果组件
        /// </summary>
        public AbilityEffectComponent AbilityEffectComponent => AbilityEntity.GetComponent<AbilityEffectComponent>();
        /// <summary>
        /// 执行效果组件
        /// </summary>
        public ExecutionEffectComponent ExecutionEffectComponent { get; set; }
        /// <summary>
        /// 能力效果列表
        /// </summary>
        public List<AbilityEffect> AbilityEffects => GetComponent<AbilityEffectComponent>().AbilityEffects;
        /// <summary>
        /// 执行效果列表
        /// </summary>
        public List<ExecutionEffect> ExecutionEffects => GetComponent<ExecutionEffectComponent>().ExecutionEffects;
        /// <summary>
        /// 所属战斗实体
        /// </summary>
        public CombatEntity OwnerEntity => GetParent<CombatEntity>();


        public override void Awake(object initData)
        {
            AbilityEntity = initData as AbilityEntity;
        }

        /// <summary>
        /// 开始执行
        /// </summary>
        public virtual void BeginExecute()
        {

        }

        /// <summary>
        /// 结束执行
        /// </summary>
        public virtual void EndExecute()
        {
            Destroy(this);
        }
        /// <summary>
        /// 获取能力实体
        /// </summary>
        /// <typeparam name="T">AbilityEntity</typeparam>
        /// <returns></returns>
        public T GetAbility<T>() where T : AbilityEntity
        {
            return AbilityEntity as T;
        }
    }
}