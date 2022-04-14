using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitResetTransformComponent : MonoBehaviour
{
    /// <summary>
    /// ������λ�ڶ�Ӧpos,rot,scale
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
    /// ������λ�ڶ�Ӧpos,rot,   scale����
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
    /// ������λ�ڶ�Ӧpos,  ��Ϊtrue,����ת��ΪQuaternion.identity
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
