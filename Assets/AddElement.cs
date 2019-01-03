using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddElement : MonoBehaviour
{
    public GameObject currentSelection;
    private GameObject previewedObject;


    private GameObject test;

    private int currentPlugIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        previewedObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        PreviewEdition();
        //CheckCursorCollision();
    }

    void PreviewEdition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            int layerPlug = LayerMask.NameToLayer("Plug");
            if (hit.collider.gameObject.layer == layerPlug)
            {
                if(previewedObject != null)
                {
                    return;
                }

                Transform attachedElement = hit.collider.gameObject.transform.parent;
                Transform plug = currentSelection.transform.GetChild(currentPlugIndex);
                Transform plugged = hit.collider.transform;
                Vector3 newElementPosition = plugged.position + plug.position;
                Vector3 normalPlug = plug.forward;
                Vector3 normalPlugged = plugged.forward;
                
                previewedObject = Instantiate(currentSelection, newElementPosition, attachedElement.rotation);

                foreach (Rigidbody r in previewedObject.GetComponentsInChildren<Rigidbody>())
                {
                    r.isKinematic = true;
                }
                Vector3 axisRotation = Vector3.Cross(normalPlug, normalPlugged);
                if (Vector3.Cross(normalPlug, normalPlugged) != Vector3.zero)
                    previewedObject.transform.RotateAround(plugged.position, axisRotation, Vector3.Angle(normalPlug, normalPlugged));
                else
                    previewedObject.transform.RotateAround(plugged.position, Vector3.up, Vector3.Angle(normalPlug, normalPlugged));
            }
            else
            {
                //ResetPreview();
            }
        }
        else
        {
            ResetPreview();
        }
    }

    void ResetPreview()
    {
        if (previewedObject != null)
        {
            Object.Destroy(previewedObject);
            previewedObject = null;
        }
    }

    void CheckCursorCollision()
    {
        if(Input.GetButton("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                int layerPlug = LayerMask.NameToLayer("Plug");
                if (hit.collider.gameObject.layer == layerPlug)
                {
                    Transform attachedElement = hit.collider.gameObject.transform.parent;
                    Transform plug = currentSelection.transform.GetChild(currentPlugIndex);
                    Transform plugged = hit.collider.transform;
                    Vector3 newElementPosition = plugged.position - plug.position;
                    Vector3 normalPlug = plug.forward;
                    Vector3 normalPlugged = plugged.forward;

                    GameObject newElement = Instantiate(currentSelection, newElementPosition, attachedElement.rotation);
                    newElement.transform.RotateAround(plugged.position, Vector3.Cross(normalPlug, normalPlugged), -Vector3.Angle(normalPlug, normalPlugged));
                    FixedJoint newJoint = newElement.AddComponent<FixedJoint>();

                    newJoint.connectedBody = hit.collider.attachedRigidbody;
                }
            }
        }
    }
}
