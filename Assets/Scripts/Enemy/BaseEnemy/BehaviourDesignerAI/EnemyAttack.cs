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
        //��������
        if (!IsAttacking)
        {
            entity.Attack();
        }
        //���ڹ���
        if (entity.IsAttacking)
        {
            IsAttacking = true;
            return TaskStatus.Running;
        }
        //��������
        else if(IsAttacking)
        {
            IsAttacking = false;
            return TaskStatus.Success;
        }
        //û�н��й��� / ������ȴ
        return TaskStatus.Failure;
    }
}