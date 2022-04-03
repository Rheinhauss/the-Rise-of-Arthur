using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTerrainCol : MonoBehaviour
{
    public List<Action> actions = new List<Action>();

    private void OnCollisionStay(Collision collision)
    {
        foreach(Action action in actions)
        {
            action?.Invoke();
        }
    }
}
