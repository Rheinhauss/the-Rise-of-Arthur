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
    Attack1,
    Death,
    Evade,
    AttackHeavy,
}


public class Enemy : UnitControllerComponent
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

    public void Start()
    {

        // ��ʼ��CombatEntity
        // ���ظ������
        combatEntity = CombatContext.Instance.AddChild<CombatEntity>();
        CombatContext.Instance.Object2Entities.Add(gameObject, combatEntity);
        combatEntity.ModelObject = this.gameObject;
        combatEntity.Position = transform.position;
        combatEntity.AddComponent<MotionComponent>();

        EnemyDeathEntity = combatEntity.AddChild<EnemyDeathEntity>();

        EnemyMoveEntity = combatEntity.AddChild<EnemyMoveEntity>();
        EnemyMoveEntity.Init();
        EnemyMoveEntity.NavMove(new Vector3(10, 0, 0));

        // Action��������
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
        EnemyDeathEntity.CheckDeath();
    }

}
