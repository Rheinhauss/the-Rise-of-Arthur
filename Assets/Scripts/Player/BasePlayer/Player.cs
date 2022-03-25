using DG.Tweening;
using DG.Tweening.Plugins.Options;
using EGamePlay;
using EGamePlay.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 枚举：人物会执行的技能类型
/// 站立、移动、轻击
/// </summary>
public enum PlayerAction
{
    Idle,
    Move,
    Death,
}
/// <summary>
/// 枚举：Action类型的阶段
/// 前摇、强制后摇、后摇
/// </summary>
public enum AnimState
{
    None,
    Pre,
    ForcePost,
    Post
}

/// <summary>
/// 玩家基类；后续女侠、老外等角色脚本会继承Player
/// 继承：UnityControllerComponent
/// </summary>
public class Player : UnitControllerComponent
{
    /// <summary>
    /// 初始化Player
    /// </summary>
    public static Player Instance;
    /// <summary>
    /// Player当前Action类型
    /// </summary>
    public PlayerAction PlayerAction = PlayerAction.Idle;
    /// <summary>
    /// Player的Action类型的阶段
    /// </summary>
    public AnimState AnimState = AnimState.None;
    /// <summary>
    /// Player当前播放的Action动画的阶段
    /// </summary>
    public Animancer.AnimancerState currentState;
    /// <summary>
    /// 技能调用组件
    /// </summary>
    public SpellComponent SpellComponent => combatEntity.GetComponent<SpellComponent>();
    /// <summary>
    /// Player移动Skill实体
    /// </summary>
    public PlayerMoveEntity PlayerMoveEntity;
    /// <summary>
    /// Player旋转实体
    /// </summary>
    public PlayerRotateEntity PlayerRotateEntity;
    /// <summary>
    /// Player所有UI的控制
    /// </summary>
    public PlayerUIController PlayerUIController;
    /// <summary>
    /// Player的死亡检测+控制+死亡后事件
    /// </summary>
    public PlayerDeathEntity PlayerDeathEntity;
    /// <summary>
    /// Player鼠标控制
    /// </summary>
    public PlayerCursorEntity PlayerCursorEntity;

    private void Awake()
    {
        Instance = this;

    }

    private void Start()
    {
        // 初始化CombatEntity
        combatEntity = CombatContext.Instance.AddChild<CombatEntity>();
        CombatContext.Instance.Object2Entities.Add(gameObject, combatEntity);

        currentState = unitAnimatorComponent.Play(unitAnimatorComponent.animationClipsDict["SwordsmanIdle"]);
        
        // 挂载移动Skill实体
        PlayerMoveEntity = combatEntity.AddChild<PlayerMoveEntity>();
        PlayerMoveEntity.Init(this.transform, this.transform.GetChild(0));

        //挂载旋转实体
        PlayerRotateEntity = combatEntity.AddChild<PlayerRotateEntity>();
        PlayerRotateEntity.Init();

        // 挂载Death实体
        PlayerDeathEntity = combatEntity.AddChild<PlayerDeathEntity>();

        //鼠标指针实体
        PlayerCursorEntity = combatEntity.AddChild<PlayerCursorEntity>();
        PlayerCursorEntity.Init();

        //事件监听
        combatEntity.ListenActionPoint(ActionPointType.PostReceiveDamage, OnReceiveDamage);
        combatEntity.ListenActionPoint(ActionPointType.PostReceiveCure, OnReceiveCure);

    }


    public override void OnReceiveCure(ActionExecution combatAction)
    {
        var cureAction = combatAction as CureAction;
        PlayerUIController._HP.OnReceiveCure(combatEntity.UnitPropertyEntity.HP.Percent(), cureAction.CureValue);
    }

    public override void OnReceiveDamage(ActionExecution combatAction)
    {
        var damageAction = combatAction as DamageAction;
        PlayerUIController._HP.OnReceiveDamage(combatEntity.UnitPropertyEntity.HP.Percent(), damageAction.DamageValue);
        PlayerDeathEntity.CheckDeath();
    }
}
