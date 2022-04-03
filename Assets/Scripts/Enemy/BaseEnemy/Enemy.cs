using DG.Tweening;
using EGamePlay.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 枚举：人物会执行的技能类型
/// 站立、移动、轻击
/// </summary>
public enum EnemyAction
{
    Idle,
    Move,
    Attack1,
    Death,
    Evade,
    AttackHeavy,
}


public class Enemy : UnitControllerComponent
{
    /// <summary>
    /// Enemy当前Action类型
    /// </summary>
    public EnemyAction PlayerAction = EnemyAction.Idle;
    /// <summary>
    /// Enemy的Action类型的阶段
    /// </summary>
    public AnimState AnimState = AnimState.None;
    /// <summary>
    /// Enemy当前播放的Action动画的阶段
    /// </summary>
    public Animancer.AnimancerState currentState;
    /// <summary>
    /// Enemy所有UI的控制
    /// </summary>
    public EnemyUIController EnemyUIController;
    /// <summary>
    /// Enemy死亡控制实体
    /// </summary>
    public EnemyDeathEntity EnemyDeathEntity;
    /// <summary>
    /// 敌人移动实体
    /// </summary>
    public EnemyMoveEntity EnemyMoveEntity;

    public void Start()
    {

        // 初始化CombatEntity
        // 挂载各种组件
        combatEntity = CombatContext.Instance.AddChild<CombatEntity>();
        CombatContext.Instance.Object2Entities.Add(gameObject, combatEntity);
        combatEntity.ModelObject = this.gameObject;
        combatEntity.Position = transform.position;
        combatEntity.AddComponent<MotionComponent>();

        EnemyDeathEntity = combatEntity.AddChild<EnemyDeathEntity>();

        EnemyMoveEntity = combatEntity.AddChild<EnemyMoveEntity>();
        EnemyMoveEntity.Init();
        EnemyMoveEntity.NavMove(new Vector3(10, 0, 0));

        // Action监听函数
        combatEntity.ListenActionPoint(ActionPointType.PostReceiveDamage, OnReceiveDamage);
        combatEntity.ListenActionPoint(ActionPointType.PostReceiveCure, OnReceiveCure);
        combatEntity.ListenActionPoint(ActionPointType.PostReceiveStatus, OnReceiveStatus);
        combatEntity.Subscribe<RemoveStatusEvent>(OnRemoveStatus);
    }

    private void Update()
    {
        this.transform.rotation = Quaternion.identity;
    }

    /// <summary>
    /// 接受治愈Action，进行跳字UI
    /// </summary>
    /// <param name="combatAction">Action执行体</param>
    public override void OnReceiveCure(ActionExecution combatAction)
    {
        var cureAction = combatAction as CureAction;
        EnemyUIController._HP.OnReceiveCure(combatEntity.UnitPropertyEntity.HP.Percent(), cureAction.CureValue);
    }
    /// <summary>
    /// 接受伤害Action，进行跳字UI
    /// </summary>
    /// <param name="combatAction">Action执行体</param>
    public override void OnReceiveDamage(ActionExecution combatAction)
    {
        var damageAction = combatAction as DamageAction;
        EnemyUIController._HP.OnReceiveDamage(combatEntity.UnitPropertyEntity.HP.Percent(), damageAction.DamageValue);
        EnemyDeathEntity.CheckDeath();
    }

}
