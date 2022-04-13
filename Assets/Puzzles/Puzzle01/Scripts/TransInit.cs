using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TransInit : MonoBehaviour
{
    public Transform TransParent => this.transform;
    [Header("Z为行,X为列")]
    public CustomArrays<TransEntityEditor>[] TransObjectEntitys;

    private void Awake()
    {
        Init();
    }

    [ContextMenu("初始化")]
    private void Init()
    {
        while (TransParent.childCount != 0)
        {
            DestroyImmediate(TransParent.GetChild(0).gameObject);
        }
        int delta = 2;
        int x = 0;
        int z = 0;
        foreach (CustomArrays<TransEntityEditor> arrays in TransObjectEntitys)
        {
            z = 0;
            foreach (TransEntityEditor value in arrays.Array)
            {
                value.Init(new Vector3(x * delta, 0, z * delta), TransParent);
                ++z;
            }
            ++x;
        }
    }

}
