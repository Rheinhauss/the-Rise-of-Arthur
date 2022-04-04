using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Enemy")]
public class EnemySeek : Action
{
    public SharedTransform targetTransform;
    public SharedVector3 targetPosition;
    public SharedMoveType moveType;

    // True if the target is a transform
    private bool dynamicTarget;
    // A cache of the NavMeshAgent
    private EnemyMoveEntity moveEntity;

    public override void OnAwake()
    {
        // cache for quick lookup
    }

    public override void OnStart()
    {
        moveEntity = gameObject.GetComponent<Enemy>().EnemyMoveEntity;
        moveEntity.ChangeMoveType(moveType.Value);
        // the target is dynamic if the target transform is not null and has a valid
        dynamicTarget = (targetTransform != null && targetTransform.Value != null);

        // set the speed, angular speed, and destination then enable the agent
        moveEntity.NavMove(Target());
    }

    // Seek the destination. Return success once the agent has reached the destination.
    // Return running if the agent hasn't reached the destination yet
    public override TaskStatus OnUpdate()
    {
        if (!moveEntity.IsMoving())
        {
            return TaskStatus.Success;
        }

        // Update the destination if the target is a transform because that agent could move
        if (dynamicTarget)
        {
            moveEntity.NavMove(Target());
        }
        return TaskStatus.Running;
    }

    // Return targetPosition if targetTransform is null
    private Vector3 Target()
    {
        if (dynamicTarget)
        {
            return targetTransform.Value.position;
        }
        return targetPosition.Value;
    }

    public override void OnEnd()
    {
        moveEntity.agent.enabled = false;
    }

    // Reset the public variables
    public override void OnReset()
    {
    }
}