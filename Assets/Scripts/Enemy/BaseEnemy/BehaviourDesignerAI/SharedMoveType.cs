using UnityEngine;
using BehaviorDesigner.Runtime;

[System.Serializable]
public class SharedMoveType : SharedVariable<MoveType>
{
	public override string ToString() { return  mValue.ToString(); }
	public static implicit operator SharedMoveType(MoveType value) { return new SharedMoveType { mValue = value }; }
}