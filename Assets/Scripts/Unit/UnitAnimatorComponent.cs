using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Animancer;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;

[Serializable]
public struct AnimationClipStruct
{
    public string Name;
    public AnimationClip animationClip;
}
public class UnitAnimatorComponent : MonoBehaviour
{
    public Animancer.AnimancerComponent AnimancerComponent;
    public AnimationClipStruct[] animationClipsStruct;
    public Dictionary<string, AnimationClip> animationClipsDict = new Dictionary<string, AnimationClip>();
    public float Speed { get; set; } = 1f;


    private void Start()
    {
        AnimancerComponent.Animator.fireEvents = false;
        foreach (var item in animationClipsStruct)
        {
            animationClipsDict.Add(item.Name, item.animationClip);
            AnimancerComponent.States.CreateIfNew(item.animationClip);
        }
    }

    public AnimancerState Play(AnimationClip clip)
    {
        var state = AnimancerComponent.States.GetOrCreate(clip);
        state.Speed = Speed;
        AnimancerComponent.Play(state);
        return state;
        
    }

    public AnimancerState PlayFade(AnimationClip clip)
    {
        var state = AnimancerComponent.States.GetOrCreate(clip);
        state.Speed = Speed;
        AnimancerComponent.Play(state, 0.25f);
        return state;
    }

    public AnimancerState TryPlayFade(AnimationClip clip)
    {
        var state = AnimancerComponent.States.GetOrCreate(clip);
        state.Speed = Speed;
        if (AnimancerComponent.IsPlaying(clip))
        {
            return null;
        }
        AnimancerComponent.Play(state, 0.25f);
        return state;
    }

}
