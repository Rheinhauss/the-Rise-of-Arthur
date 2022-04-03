using DG.Tweening;
using EGamePlay;
using EGamePlay.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MoveType
{
    Walk,
    Run,
    None
}

public class EnemyMoveEntity : Entity
{
    public CombatEntity combatEntity => GetParent<CombatEntity>();
    public Transform Owner => combatEntity.ModelObject.transform;
    public NavMeshAgent agent => Owner.GetComponent<NavMeshAgent>();
    public MoveType MoveType = MoveType.None;
    public float walkSpeed => combatEntity.UnitPropertyEntity.MoveSpeed.Value * 0.2f;
    public float runSpeed => combatEntity.UnitPropertyEntity.MoveSpeed.Value;

    public Transform ModelTransform => Owner.GetChild(0);
    public Enemy Enemy => Owner.GetComponent<Enemy>();
    private UnitAnimatorComponent unitAnimatorComponent => Enemy.unitAnimatorComponent;

    public string MoveAnim;

    public void Init()
    {
        ChangeMoveType(MoveType.Walk);
        AddComponent<UpdateComponent>();
    }

    public override void Update()
    {
        if (!IsMoving())
        {
            Enemy.currentState = unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["EnemyIdle"]);
            Enemy.PlayerAction = EnemyAction.Idle;
            Enemy.AnimState = AnimState.None;
        }
    }
    public bool IsMoving()
    {
        if (!agent.enabled)
            return false;
        bool r = agent.pathPending ||
        agent.remainingDistance > agent.stoppingDistance ||
        agent.velocity != Vector3.zero;
        r = agent.enabled ? r : false;
        return r;
    }

    public void ChangeMoveType(MoveType moveType)
    {
        MoveType = moveType;
        if(MoveType == MoveType.Walk)
        {
            agent.speed = walkSpeed;
            MoveAnim = "EnemyWalk";
        }
        else if(MoveType == MoveType.Run)
        {
            agent.speed = runSpeed;
            MoveAnim = "EnemyRun";
        }
    }

    public void NavMove(Vector3 targetPos)
    {
        Quaternion origin = ModelTransform.rotation;
        ModelTransform.LookAt(targetPos);
        Quaternion quaternion = ModelTransform.rotation;
        ModelTransform.rotation = origin;
        ModelTransform.DORotateQuaternion(quaternion, 0.15f);

        agent.destination = targetPos;
        Enemy.currentState = unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict[MoveAnim]);
        Enemy.PlayerAction = EnemyAction.Move;
        Enemy.AnimState = AnimState.None;
    }

}
