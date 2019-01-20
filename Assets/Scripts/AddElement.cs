using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddElement : MonoBehaviour
{
    public TrapElement currentSelection;
    private TrapElement previewedObject;

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
        if (!PreviewEdition())
            ResetPreview();
        EventHandler();
    }

    bool PreviewEdition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            int layerPlug = LayerMask.NameToLayer("Plug");
            if (hit.collider.gameObject.layer == layerPlug)
            {
                if (previewedObject != null)
                {
                    return true;
                }

                Transform attachedElement = hit.collider.gameObject.transform.parent;
                Transform plug = currentSelection.transform.GetChild(currentPlugIndex);
                Transform plugged = hit.collider.transform;
                Vector3 newElementPosition = plugged.position + plug.position;
                Vector3 normalPlug = plug.forward;
                Vector3 normalPlugged = plugged.forward;

                previewedObject = Instantiate(currentSelection, newElementPosition, attachedElement.rotation);

                SetObjectKinematic(previewedObject, true);

                Vector3 axisRotation = Vector3.Cross(normalPlug, normalPlugged);
                if (Vector3.Cross(normalPlug, normalPlugged) == Vector3.zero)//if the forward vectors are in the same direction, any axis is valid
                    axisRotation = Vector3.up;

                previewedObject.transform.RotateAround(plugged.position, axisRotation, Vector3.Angle(normalPlug, normalPlugged));
                return true;
            }
            return true;
        }
        return false;
    }

    void ResetPreview()
    {
        if (previewedObject != null)
        {
            Object.Destroy(previewedObject.gameObject);
            previewedObject = null;
        }
    }

    void SetObjectKinematic(TrapElement obj, bool isKinematic)
    {
        foreach (Rigidbody r in obj.GetComponentsInChildren<Rigidbody>())
        {
            r.isKinematic = isKinematic;
        }
        BoxCollider box = obj.gameObject.AddComponent<BoxCollider>();
        box.isTrigger = isKinematic;
    }

    void EventHandler()
    {
        if(Input.GetButton("Fire1"))
        {
            if(previewedObject != null)
            {
                if (previewedObject.CanInstantiate())
                {
                    previewedObject.instantiateTrap();
                    previewedObject = null;
                }
            }
        }
    }
}
