using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndObject : ReiceiveRayHit, TagEnumInterface
{
    [SerializeField] private EndObject otherEnd;
    [SerializeField] private TransObject Owner;
    [SerializeField] private Transform originPoint;
    [System.NonSerialized] public GameObject laser;
    private Vector3 ToPoint;
    public TagEnum TagEnum { get; set; }
    private RaycastHit hit;
    [SerializeField] private LayerMask layerMask;
    public bool ColorIsChanged { get;  set; }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        TagEnum = TagEnum.None;
        laser = DrawManager.DrawLaser(Vector3.zero, Vector3.forward);
        ColorIsChanged = false;
    }

    public override void ExecuteOnReceiveRayHit(GameObject obj, Vector3 fromPos)
    {
        TagEnumInterface fromObj = obj.GetComponent<TagEnumInterface>();
        if (Owner.SetTagNum(fromObj.TagEnum))
        {
            Owner.SendRayEnd = otherEnd;
            Owner.ReceiveRayEnd = this;
            otherEnd.ColorIsChanged = true;
        }
        otherEnd.SendRay();
    }

    public void SendRay()
    {
        if (Physics.Raycast(originPoint.position, originPoint.forward, out hit, 1000, layerMask))
        {
            ReiceiveRayHit rayHit = hit.collider.GetComponent<ReiceiveRayHit>();
            if(rayHit != null)
                rayHit.ExecuteOnReceiveRayHit(this.gameObject, originPoint.position);
            ToPoint = hit.point;
        }
        else
        {
            ToPoint = originPoint.position + originPoint.forward * 1000;
        }
    }

    public Vector3 GetOriginPos()
    {
        return originPoint.position;
    }

    public Vector3 GetEndPos()
    {
        return ToPoint;
    }


}
