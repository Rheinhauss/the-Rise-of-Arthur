using DG.Tweening;
using DG.Tweening.Plugins.Options;
using EGamePlay;
using EGamePlay.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ö�٣������ִ�еļ�������
/// վ�����ƶ������
/// </summary>
public enum PlayerAction
{
    Idle,
    Move,
    Death,
}
/// <summary>
/// ö�٣�Action���͵Ľ׶�
/// ǰҡ��ǿ�ƺ�ҡ����ҡ
/// </summary>
public enum AnimState
{
    None,
    Pre,
    ForcePost,
    Post
}

/// <summary>
/// ��һ��ࣻ����Ů��������Ƚ�ɫ�ű���̳�Player
/// �̳У�UnityControllerComponent
/// </summary>
public class Player : UnitControllerComponent
{
    /// <summary>
    /// ��ʼ��Player
    /// </summary>
    public static Player Instance;
    /// <summary>
    /// Player��ǰAction����
    /// </summary>
    public PlayerAction PlayerAction = PlayerAction.Idle;
    /// <summary>
    /// Player��Action���͵Ľ׶�
    /// </summary>
    public AnimState AnimState = AnimState.None;
    /// <summary>
    /// Player��ǰ���ŵ�Action�����Ľ׶�
    /// </summary>
    public Animancer.AnimancerState currentState;
    /// <summary>
    /// ���ܵ������
    /// </summary>
    public SpellComponent SpellComponent => combatEntity.GetComponent<SpellComponent>();
    /// <summary>
    /// Player�ƶ�Skillʵ��
    /// </summary>
    public PlayerMoveEntity PlayerMoveEntity;
    /// <summary>
    /// Player��תʵ��
    /// </summary>
    public PlayerRotateEntity PlayerRotateEntity;
    /// <summary>
    /// Player����UI�Ŀ���
    /// </summary>
    public PlayerUIController PlayerUIController;
    /// <summary>
    /// Player���������+����+�������¼�
    /// </summary>
    public PlayerDeathEntity PlayerDeathEntity;
    /// <summary>
    /// Player������
    /// </summary>
    public PlayerCursorEntity PlayerCursorEntity;

    private void Awake()
    {
        Instance = this;

    }

    private void Start()
    {
        // ��ʼ��CombatEntity
        combatEntity = CombatContext.Instance.AddChild<CombatEntity>();
        CombatContext.Instance.Object2Entities.Add(gameObject, combatEntity);

        currentState = unitAnimatorComponent.Play(unitAnimatorComponent.animationClipsDict["SwordsmanIdle"]);
        
        // �����ƶ�Skillʵ��
        PlayerMoveEntity = combatEntity.AddChild<PlayerMoveEntity>();
        PlayerMoveEntity.Init(this.transform, this.transform.GetChild(0));

        //������תʵ��
        PlayerRotateEntity = combatEntity.AddChild<PlayerRotateEntity>();
        PlayerRotateEntity.Init();

        // ����Deathʵ��
        PlayerDeathEntity = combatEntity.AddChild<PlayerDeathEntity>();

        //���ָ��ʵ��
        PlayerCursorEntity = combatEntity.AddChild<PlayerCursorEntity>();
        PlayerCursorEntity.Init();

        //�¼�����
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
