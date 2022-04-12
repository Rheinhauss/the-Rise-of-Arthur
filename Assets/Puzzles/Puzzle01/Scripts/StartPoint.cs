using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour, TagEnumInterface
{
    [SerializeField] private Transform originPoint;

    [SerializeField] private LayerMask layerMask;
    private RaycastHit hit;
    [LabelText("开始点类型"), SerializeField]
    private TagEnum tagEnum;
    public TagEnum TagEnum { get; set; }
    private GameObject laser;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if (Physics.Raycast(originPoint.position, originPoint.forward, out hit, 1000, layerMask))
        {
            ReiceiveRayHit rayHit = hit.collider.GetComponent<ReiceiveRayHit>();
            if(rayHit != null)
                rayHit.ExecuteOnReceiveRayHit(this.gameObject, originPoint.position);
            DrawLaser(originPoint.position, hit.point);
        }
    }

    private void Init()
    {
        Material material = this.GetComponent<Renderer>().material;
        material.EnableKeyword("_EMISSION");
        material.SetColor("_EmissionColor", DrawManager.GetColorFromTag(tagEnum));
        material.SetColor("_BaseColor", DrawManager.GetColorFromTag(tagEnum));
        TagEnum = tagEnum;
    }

    public void DrawLaser(Vector3 origin, Vector3 end)
    {
        if(laser == null)
        {
            laser = DrawManager.DrawLaser(origin, end);
            laser.GetComponentInChildren<LineRenderer>().startColor = DrawManager.GetColorFromTag(tagEnum);
            laser.GetComponentInChildren<LineRenderer>().endColor = DrawManager.GetColorFromTag(tagEnum);
            //var main = laser.GetComponentInChildren<ParticleSystem>().main;
            //main.startColor = new ParticleSystem.MinMaxGradient(DrawManager.GetColorFromTag(tagEnum));
        }
        else
        {
            Vector3 dir = end - origin;
            laser.transform.position = origin;
            laser.transform.LookAt(end);
            laser.GetComponentInChildren<LineRenderer>().SetPosition(1, new Vector3(0, 0, dir.magnitude));
        }
    }
}
