using BehaviorDesigner.Runtime;
using DG.Tweening;
using EGamePlay.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ö�٣������ִ�еļ�������
/// վ�����ƶ������
/// </summary>
public enum EnemyAction
{
    Idle,
    Move,
    BeHit,
    Attack1,
    Death,
    Evade,
    AttackHeavy,
}


public class Enemy : UnitControllerComponent, MoveCtrlInterface, AttackCtrlInterface
{
    /// <summary>
    /// Enemy��ǰAction����
    /// </summary>
    public EnemyAction PlayerAction = EnemyAction.Idle;
    /// <summary>
    /// Enemy��Action���͵Ľ׶�
    /// </summary>
    public AnimState AnimState = AnimState.None;
    /// <summary>
    /// Enemy��ǰ���ŵ�Action�����Ľ׶�
    /// </summary>
    public Animancer.AnimancerState currentState;
    /// <summary>
    /// Enemy����UI�Ŀ���
    /// </summary>
    public EnemyUIController EnemyUIController;
    /// <summary>
    /// Enemy��������ʵ��
    /// </summary>
    public EnemyDeathEntity EnemyDeathEntity;
    /// <summary>
    /// �����ƶ�ʵ��
    /// </summary>
    public EnemyMoveEntity EnemyMoveEntity;
    /// <summary>
    /// ��������ʵ��
    /// </summary>
    public EnemyBeHitEntity EnemyBeHitEntity;
    /// <summary>
    /// �ƶ���ֹ
    /// </summary>
    public bool CanMove { get; set; }
    public bool CanAttack { get; set; }

    private Rigidbody Rigidbody;

    public Transform WayPoints;

    /// <summary>
    /// ս������
    /// </summary>
    public Transform CombatTarget => GetComponent<BehaviorTree>().GetVariable("SeekTarget").GetValue() as Transform;
    /// <summary>
    /// ��������ʵ��
    /// </summary>
    public EnemyAttackEntity EnemyAttackEntity;

    public void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Rigidbody.isKinematic = false;
        Rigidbody.useGravity = true;
        // ��ʼ��CombatEntity
        // ���ظ������
        combatEntity = CombatContext.Instance.AddChild<CombatEntity>();
        CombatContext.Instance.Object2Entities.Add(gameObject, combatEntity);
        combatEntity.ModelObject = this.gameObject;

        EnemyDeathEntity = combatEntity.AddChild<EnemyDeathEntity>();
        EnemyBeHitEntity = combatEntity.AddChild<EnemyBeHitEntity>();

        EnemyMoveEntity = combatEntity.AddChild<EnemyMoveEntity>();
        EnemyMoveEntity.Init();

        EnemyAttackEntity = combatEntity.AddChild<EnemyAttackEntity>();
        EnemyAttackEntity.Init();

        // Action��������
        combatEntity.ListenActionPoint(ActionPointType.PostReceiveDamage, OnReceiveDamage);
        combatEntity.ListenActionPoint(ActionPointType.PostReceiveCure, OnReceiveCure);
        combatEntity.ListenActionPoint(ActionPointType.PostReceiveStatus, OnReceiveStatus);
        combatEntity.Subscribe<RemoveStatusEvent>(OnRemoveStatus);
        CanAttack = true;
    }

    //private void Update()
    //{
    //    //this.transform.rotation = Quaternion.identity;
    //}

    /// <summary>
    /// ��������Action����������UI
    /// </summary>
    /// <param name="combatAction">Actionִ����</param>
    public override void OnReceiveCure(ActionExecution combatAction)
    {
        var cureAction = combatAction as CureAction;
        EnemyUIController._HP.OnReceiveCure(combatEntity.UnitPropertyEntity.HP.Percent(), cureAction.CureValue);
    }
    /// <summary>
    /// �����˺�Action����������UI
    /// </summary>
    /// <param name="combatAction">Actionִ����</param>
    public override void OnReceiveDamage(ActionExecution combatAction)
    {
        var damageAction = combatAction as DamageAction;
        EnemyUIController._HP.OnReceiveDamage(combatEntity.UnitPropertyEntity.HP.Percent(), damageAction.DamageValue);
        EnemyBeHitEntity.SetCreator(damageAction.Creator.ModelObject.transform);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            Rigidbody.isKinematic = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        Rigidbody.useGravity = false;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            Rigidbody.isKinematic = false;
        }
    }

}
