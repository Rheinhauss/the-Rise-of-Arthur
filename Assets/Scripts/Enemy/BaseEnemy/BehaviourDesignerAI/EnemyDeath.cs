using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Enemy")]
public class EnemyDeath : Conditional
{
    private EnemyDeathEntity deathEntity;
    public override void OnStart()
    {
        deathEntity = gameObject.GetComponent<Enemy>().EnemyDeathEntity;
    }

    public override TaskStatus OnUpdate()
	{
        if (deathEntity.CheckDeath())
        {
            return TaskStatus.Success;
        }
		return TaskStatus.Failure;
	}
}