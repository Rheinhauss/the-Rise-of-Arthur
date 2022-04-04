using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Enemy")]
public class EnemyAttack : Action
{
    private EnemyAttackEntity entity;
    private bool IsAttacking = false;
    public override void OnStart()
    {
        entity = gameObject.GetComponent<Enemy>().EnemyAttackEntity;
    }

    public override TaskStatus OnUpdate()
    {
        //发动攻击
        if (!IsAttacking)
        {
            entity.Attack();
        }
        //正在攻击
        if (entity.IsAttacking)
        {
            IsAttacking = true;
            return TaskStatus.Running;
        }
        //攻击结束
        else if(IsAttacking)
        {
            IsAttacking = false;
            return TaskStatus.Success;
        }
        //没有进行攻击 / 攻击冷却
        return TaskStatus.Failure;
    }
}