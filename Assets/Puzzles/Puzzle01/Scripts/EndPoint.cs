using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : ReiceiveRayHit, TagEnumInterface
{
    [LabelText("结束点类型"), SerializeField]
    private TagEnum tagEnum;
    public TagEnum TagEnum { get; set; }
    private Material material;
    private int lastFrameCount;
    private bool _isEnd = false;

    public bool IsEnd { get {
            return _isEnd;
        } private set {
            _isEnd = value;
            if (_isEnd)
            {
                material.EnableKeyword("_EMISSION");
            }
            else
            {
                material.DisableKeyword("_EMISSION");
            }
        } }

    private void Awake()
    {
        Init();
    }

    private void LateUpdate()
    {
        if(lastFrameCount != Time.frameCount)
            IsEnd = false;
    }

    private void Init()
    {
        material = this.GetComponent<Renderer>().material;
        material.SetColor("_EmissionColor", DrawManager.GetColorFromTag(tagEnum));
        material.SetColor("_BaseColor", DrawManager.GetColorFromTag(tagEnum));
        TagEnum = tagEnum;
        IsEnd = false;
    }
    public override void ExecuteOnReceiveRayHit(GameObject obj, Vector3 fromPos)
    {
        lastFrameCount = Time.frameCount;
        TagEnumInterface fromObj = obj.GetComponent<TagEnumInterface>();
        if (fromObj.TagEnum == TagEnum)
        {
            IsEnd = true;
        }
    }
}
