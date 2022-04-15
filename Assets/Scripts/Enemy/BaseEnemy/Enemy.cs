using BehaviorDesigner.Runtime;
using DG.Tweening;
using EGamePlay.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using EGamePlay;

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
    private Transform combatTarget = null;
    /// <summary>
    /// ��������ʵ��
    /// </summary>
    public EnemyAttackEntity EnemyAttackEntity;

    public string dataPath = "EnemyProperty.json";

    private bool IsExeStart = false;

    public int CoinAmount = 10;

    public void Start()
    {
        if (IsExeStart)
        {
            return;
        }
        IsExeStart = true;
        Rigidbody = GetComponent<Rigidbody>();
        //Rigidbody.isKinematic = false;
        Rigidbody.useGravity = true;
        // ��ʼ��CombatEntity
        // ���ظ������
        combatEntity = CombatContext.Instance.AddChild<CombatEntity>();
        CombatContext.Instance.Object2Entities.Add(gameObject, combatEntity);
        combatEntity.ModelObject = this.gameObject;
        LoadPropertyData();

        EnemyDeathEntity = combatEntity.AddChild<EnemyDeathEntity>();
        EnemyDeathEntity.AddAction(() =>
        {
            ItemWorld.SpawnItemWorld(transform.position + new Vector3(0, 0.5f, 0), Item_Factory.Instance.CreateItem(ItemType.Coin_A, CoinAmount));
        });


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

        //GetComponentInChildren<Canvas>().worldCamera = Camera.main;
        //var tmp = GetComponentInChildren<RotationConstraint>();
        //var t = tmp.GetSource(0);
        //t.sourceTransform = Camera.main.transform;
        //Debug.Log(t.sourceTransform.name);
    }

    private void ResetSelf()
    {
        GetComponent<BehaviorTree>().GetVariable("SeekTarget").SetValue(null);
        GetComponent<BehaviorTree>().GetVariable("BeHit").SetValue(false);
        StatusObject StatusObject = new StatusObject();
        StatusObject.Init(StatusType.AddStatus, "Status/BaseStatus/Status_5_ResetHP", AddSkillEffetTargetType.Self);
        combatEntity.unitSpellStatusToSelfComponent.SpellToSelf(StatusObject);
    }

    private void Update()
    {
        if(combatTarget != CombatTarget)
        {
            //��ʧս��Ŀ��
            if(CombatTarget == null)
            {
                combatTarget.GetComponent<Player>().PlayerResetEntity.timer.EndActions.Add(ResetSelf);
                combatTarget = null;
                ResetSelf();
            }
            //���ս��Ŀ��
            else if(combatTarget == null)
            {
                combatTarget = CombatTarget;
                combatTarget.GetComponent<Player>().PlayerResetEntity.timer.EndActions.Add(ResetSelf);
            }
            //����ս��Ŀ��
            else
            {
                combatTarget.GetComponent<Player>().PlayerResetEntity.timer.EndActions.Remove(ResetSelf);
                combatTarget = CombatTarget;
                combatTarget.GetComponent<Player>().PlayerResetEntity.timer.EndActions.Add(ResetSelf);
            }
        }
    }

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

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.collider.tag == "Player")
    //    {
    //        Rigidbody.isKinematic = true;
    //    }
    //}

    //private void OnCollisionStay(Collision collision)
    //{
    //    Rigidbody.useGravity = false;
    //}
    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.collider.tag == "Player")
    //    {
    //        Rigidbody.isKinematic = false;
    //    }
    //}
    public void CanBeAttacked()
    {
        combatEntity.IsInvincibel = false;
    }
    
    public void CantBeAttacked()
    {
        Start();
        combatEntity.IsInvincibel = true;
    }

    public virtual void LoadPropertyData()
    {
        combatEntity.InitProperty(Application.streamingAssetsPath + "/" + dataPath);
    }
    private void OnDestroy()
    {
        Entity.Destroy(combatEntity);
    }
}
