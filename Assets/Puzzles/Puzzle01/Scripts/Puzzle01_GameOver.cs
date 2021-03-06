using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle01_GameOver : MonoBehaviour
{
    private EndPoint[] endPoints;
    public Transform Door;

    private void Awake()
    {
        endPoints = GetComponentsInChildren<EndPoint>();
    }
    private void Update()
    {
        if (IsPassed())
        {
            Door.gameObject.SetActive(false);
        }
    }
    public bool IsPassed()
    {
        bool b = true;
        foreach(EndPoint endPoint in endPoints)
        {
            b &= endPoint.IsEnd;
        }
        return b;
    }

}
