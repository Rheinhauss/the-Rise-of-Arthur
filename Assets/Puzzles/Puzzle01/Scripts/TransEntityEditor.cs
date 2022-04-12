using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TransEntityEditor
{
    public enum TransEntityType
    {
        [LabelText("ÊúÖ±")]
        _1,
        [LabelText("ÊúÕÛ")]
        _2,
        [LabelText("Ê®×Ö")]
        _3
    }

    public enum TransRotAngle
    {
        _0 = 0,
        _90 = 90,
        _180 = 180,
        _270 = 270,
    }

    public static GameObject TransObjectEntity_1;
    public static GameObject TransObjectEntity_2;
    public static GameObject TransObjectEntity_3;


    public TransEntityType transEntityType;
    public TransRotAngle transRotAngle;

    public GameObject Init(Vector3 pos, Transform parent = null)
    {
        LoadResource();
        switch (transEntityType)
        {
            case TransEntityType._1:
                return Object.Instantiate(TransObjectEntity_1, pos, Quaternion.Euler(0, (int)transRotAngle,0), parent);
            case TransEntityType._2:
                return Object.Instantiate(TransObjectEntity_2, pos, Quaternion.Euler(0, (int)transRotAngle, 0), parent);
            case TransEntityType._3:
                return Object.Instantiate(TransObjectEntity_3, pos, Quaternion.Euler(0, (int)transRotAngle, 0), parent);
            default:
                return null;
        }
    }

    public void LoadResource()
    {
        if (TransObjectEntity_1 == null || TransObjectEntity_2 == null || TransObjectEntity_3 == null)
        {
            TransObjectEntity_1 = Resources.Load("TransObjectEntity_1") as GameObject;
            TransObjectEntity_2 = Resources.Load("TransObjectEntity_2") as GameObject;
            TransObjectEntity_3 = Resources.Load("TransObjectEntity_3") as GameObject;
        }
    }

}
