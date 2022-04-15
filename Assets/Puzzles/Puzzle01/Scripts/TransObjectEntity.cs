using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransObjectEntity : MonoBehaviour
{
    private Transform m_transform => this.transform;
    private Vector3 rot = new Vector3(0, 90, 0);

    private Transform[] childTransform => m_transform.GetComponentsInChildren<Transform>();
    private List<Outline> outlines = new List<Outline>();

    private static Transform TriggetObject;

    private static InputEntity inputEntity;

    private void Awake()
    {
        if(inputEntity == null)
        {
            inputEntity = new InputEntity(KeyCode.F, KeyCodeType.DOWN);
            inputEntity.name = "TransObjectEntity";
        }

        foreach (Transform child in childTransform)
        {
            var outline = child.gameObject.AddComponent<Outline>();

            outline.OutlineMode = Outline.Mode.OutlineAll;
            outline.OutlineColor = Color.green;
            outline.OutlineWidth = 20f;
            outline.enabled = false;
            outlines.Add(outline);
        }
    }

    private void OnMouseDown()
    {
        Rotate();
    }

    private void SelectedEnter()
    {
        foreach(Outline outline in outlines)
        {
            outline.enabled = true;
        }
    }

    private void SelectedExit()
    {
        foreach (Outline outline in outlines)
        {
            outline.enabled = false;
        }
    }

    private void Rotate()
    {
        m_transform.Rotate(rot);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(TriggetObject == null)
            {
                TriggetObject = m_transform;
                SelectedEnter();
                inputEntity.BindInputAction(Rotate);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if(TriggetObject == m_transform)
            {
                TriggetObject = null;
                SelectedExit();
                inputEntity.UnBindInputAction(Rotate);
            }
        }
    }
}
