using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Enemy")]
public class EnemyBeHit : Conditional
{
    private EnemyBeHitEntity entity;
    public SharedTransform Creator;

    private bool IsEnd = true;

    public override void OnStart()
    {
        entity = gameObject.GetComponent<Enemy>().EnemyBeHitEntity;
    }

    public override TaskStatus OnUpdate()
    {
        if (entity.CheckBehit())
        {
            IsEnd = false;
            Creator.Value = entity.Creator;
            return TaskStatus.Running;
        }
        else if (!IsEnd)
        {
            IsEnd = true;
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}