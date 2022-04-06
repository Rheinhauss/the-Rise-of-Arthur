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
    Evade,
    Attack1,
    Death,
    AttackHeavy,
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
public class Player : UnitControllerComponent, MoveCtrlInterface, AttackCtrlInterface
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
    /// <summary>
    /// ����Skillʵ��
    /// </summary>
    public PlayerEvadeEntity PlayerEvadeEntity;
    /// <summary>
    /// ���Skillʵ��
    /// </summary>
    public Skill_2_Attack1_Entity Skill_2_Attack1_Entity;
    /// <summary>
    /// �ػ�Skillʵ��
    /// </summary>
    public Skill_6_AttackHeavy Skill_6_AttackHeavy;
    /// <summary>
    /// �ƶ�����
    /// </summary>
    public bool CanMove { get; set; }
    /// <summary>
    /// ��������
    /// </summary>
    public bool CanAttack { get; set; }
    /// <summary>
    /// �������ת��ֹ
    /// </summary>
    public bool CanCameraRot { get { return PlayerRotateEntity.RotEnable; } set { PlayerRotateEntity.RotEnable = value; } }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // ��ʼ��CombatEntity
        combatEntity = CombatContext.Instance.AddChild<CombatEntity>();
        CombatContext.Instance.Object2Entities.Add(gameObject, combatEntity);
        combatEntity.ModelObject = this.gameObject;
        currentState = unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["SwordsmanIdle"]);
        
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

        // �������Skillʵ�壬�Ѿ�����CanAttack
        Skill_2_Attack1_Entity = combatEntity.AddChild<Skill_2_Attack1_Entity>();
        Skill_2_Attack1_Entity.Init();

        // �����ػ�Skillʵ�壬�Ѿ�����CanAttack
        Skill_6_AttackHeavy = combatEntity.AddChild<Skill_6_AttackHeavy>();
        Skill_6_AttackHeavy.Init();

        // ��������Skillʵ��
        PlayerEvadeEntity = combatEntity.AddChild<PlayerEvadeEntity>();
        PlayerEvadeEntity.Init();

        //�¼�����
        combatEntity.ListenActionPoint(ActionPointType.PostReceiveDamage, OnReceiveDamage);
        combatEntity.ListenActionPoint(ActionPointType.PostReceiveCure, OnReceiveCure);
        CanAttack = true;
    }

    //private void Update()
    //{
    //    Debug.Log(PlayerAction + " " + AnimState);
    //}

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

    public void CursorLock()
    {
        PlayerCursorEntity.CursorLock();
        PlayerCursorEntity.isEnabled = true;
    }
    public void CursorUnLock()
    {
        PlayerCursorEntity.CursorUnLock();
        PlayerCursorEntity.isEnabled = false;
    }
}
