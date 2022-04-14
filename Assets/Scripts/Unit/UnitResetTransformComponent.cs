using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitResetTransformComponent : MonoBehaviour
{
    /// <summary>
    /// 将对象复位于对应pos,rot,scale
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="pos"></param>
    /// <param name="rot"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    public static Transform Reset(Transform unit, Vector3 pos, Quaternion rot, Vector3 scale)
    {
        unit.position = pos;
        unit.rotation = rot;
        unit.localScale = scale;
        return unit;
    }

    /// <summary>
    /// 将对象复位于对应pos,rot,   scale不变
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="pos"></param>
    /// <param name="rot"></param>
    /// <returns></returns>
    public static Transform Reset(Transform unit, Vector3 pos, Quaternion rot)
    {
        unit.position = pos;
        unit.rotation = rot;
        return unit;
    }

    /// <summary>
    /// 将对象复位于对应pos,  若为true,则旋转置为Quaternion.identity
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="pos"></param>
    /// <param name="resetRot"></param>
    /// <returns></returns>
    public static Transform Reset(Transform unit, Vector3 pos, bool resetRot = true)
    {
        unit.position = pos;
        if(resetRot)
            unit.rotation = Quaternion.identity;
        return unit;
    }
}
