using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToPosrForce : MonoBehaviour
{
    private void Awake()
    {
        Player.Instance.AnimState = AnimState.ForcePost;
    }
}
