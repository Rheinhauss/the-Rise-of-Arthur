using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransObjectEntity : MonoBehaviour
{
    private Transform m_transform => this.transform;
    private Vector3 rot = new Vector3(0, 90, 0);

    private void OnMouseDown()
    {
        m_transform.Rotate(rot);
    }
}
