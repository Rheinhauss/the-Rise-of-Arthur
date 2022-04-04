using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Values")]
public class GetSharedVar : Action
{
	public SharedVariable GetVar;
	public SharedVariable Value;
	public SharedBool returnValue = true;
	public override void OnStart()
	{
		Value.SetValue(Value.GetValue());
	}

	public override TaskStatus OnUpdate()
	{
		return returnValue.Value ? TaskStatus.Success : TaskStatus.Failure;
	}
}