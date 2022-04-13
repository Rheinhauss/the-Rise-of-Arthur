using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransObjectEntity : MonoBehaviour
{
    private Transform m_transform => this.transform;
    private Vector3 rot = new Vector3(0, 90, 0);

    private void OnMouseDown()
    {
        Rotate();
    }

    private void Rotate()
    {
        m_transform.Rotate(rot);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            UnitControllerComponent.inputComponent.BindInputAction(KeyCode.F, Rotate, KeyCodeType.DOWN);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            UnitControllerComponent.inputComponent.UnBindInputAction(KeyCode.F, Rotate, KeyCodeType.DOWN);
        }
    }
}
