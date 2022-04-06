using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToPosrForce : MonoBehaviour
{
    private void Awake()
    {
        if(Player.Instance.AnimState == AnimState.Pre)
            Player.Instance.AnimState = AnimState.ForcePost;
    }
}
