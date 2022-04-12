using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CustomArrays<T>
{
    public T[] Array;
    public T this[int index]
    {
        get
        {
            return Array[index];
        }
    }
    public CustomArrays()
    {
        this.Array = new T[4];
    }
    public CustomArrays(int index)
    {
        this.Array = new T[index];
    }
}
