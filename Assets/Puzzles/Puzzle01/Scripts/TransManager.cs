using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransManager : MonoBehaviour
{
    private TransObject[] transObjects;

    private void Awake()
    {
        Init();
    }

    private void LateUpdate()
    {
        foreach(TransObject transObject in transObjects)
        {
            transObject.DrawLaser();
            transObject.TagEnum = TagEnum.None;
        }
    }

    private void Init()
    {
        transObjects = GetComponentsInChildren<TransObject>();
    }
}
