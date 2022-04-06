using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Values")]
public class ComparedSharedVar : Conditional
{
	public SharedVariable Value1;
	public SharedVariable Value2;
	public override TaskStatus OnUpdate()
	{
		if(Value1.GetValue() == null && Value2.GetValue() == null)
        {
			return TaskStatus.Success;
		}
		else if(Value1.GetValue() == null || Value2.GetValue() == null)
        {
			return TaskStatus.Failure;
        }
		if (Value1.GetValue().Equals(Value2.GetValue()))
			return TaskStatus.Success;
		return TaskStatus.Failure;
	}
}