using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransObject : ReiceiveRayHit, TagEnumInterface
{
    private Material material;
    [SerializeField] private EndObject endA;
    [SerializeField] private EndObject endB;
    public TagEnum TagEnum { get; set; }
    public bool IsRotating = false;

    [System.NonSerialized]
    public EndObject SendRayEnd;
    [System.NonSerialized]
    public EndObject ReceiveRayEnd;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        material = this.GetComponent<Renderer>().material;
        TagEnum = TagEnum.None;
    }

    public bool SetTagNum(TagEnum tag)
    {
        bool f = false;
        if(TagEnum == tag && TagEnum != TagEnum.None)
        {
            f = true;
        }
        else if(TagEnum == TagEnum.None)
        {
            TagEnum = tag;
            //material.SetColor("_BaseColor", DrawManager.GetColorFromTag(TagEnum));
            f = true;
        }
        else if(tag == TagEnum.Mixed)
        {
            TagEnum = TagEnum.None;
        }
        else
        {
            TagEnum = TagEnum.Mixed;
            f = false;
        }
        endA.TagEnum = endB.TagEnum = TagEnum;
        return f;
    }
    public void DrawLaser()
    {
        if(TagEnum == TagEnum.None || TagEnum == TagEnum.Mixed)
        {
            endA.laser.gameObject.SetActive(false);
            endB.laser.gameObject.SetActive(false);
            return;
        }
        ReceiveRayEnd.laser.gameObject.SetActive(false);
        GameObject laser = SendRayEnd.laser;
        Vector3 origin = SendRayEnd.GetOriginPos();
        Vector3 end = SendRayEnd.GetEndPos();
        if (laser == null)
        {
            laser = DrawManager.DrawLaser(origin, end);
        }
        else
        {
            laser.gameObject.SetActive(true);
            Vector3 dir = end - origin;
            laser.transform.position = origin;
            laser.transform.LookAt(end);
            if (SendRayEnd.ColorIsChanged)
            {
                laser.GetComponentInChildren<LineRenderer>().SetPosition(1, new Vector3(0, 0, dir.magnitude));
                laser.GetComponentInChildren<LineRenderer>().startColor = DrawManager.GetColorFromTag(TagEnum);
                laser.GetComponentInChildren<LineRenderer>().endColor = DrawManager.GetColorFromTag(TagEnum);
                //var ps = laser.GetComponentInChildren<ParticleSystem>();
                //var main = ps.main;
                //main.startColor = new ParticleSystem.MinMaxGradient(DrawManager.GetColorFromTag(TagEnum));
                SendRayEnd.ColorIsChanged = false;
            }
        }
    }
}
