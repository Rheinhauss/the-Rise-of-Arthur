using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Values")]
public class SetSharedVar : Action
{
	public SharedVariable SetVar;
	public SharedVariable Value;
	public SharedBool returnValue = true;
	public override void OnStart()
	{
		SetVar.SetValue(Value.GetValue());
	}

	public override TaskStatus OnUpdate()
	{
		return returnValue.Value ? TaskStatus.Success : TaskStatus.Failure;
	}
}