using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddElement : MonoBehaviour
{
    public TrapElement[] availablePrefabs;
    private TrapElement currentSelection;
    private int indexCurrentSelection = 0;
    private TrapElement previewedObject;
    private List<TrapElement> generatedTrap = new List<TrapElement>();

    private GameObject test;

    private int currentPlugIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(availablePrefabs.Length <= 0)
        {
            Debug.Log("Error: no prefabs available");
            return;
        }

        currentSelection = availablePrefabs[indexCurrentSelection];
        previewedObject = null;
        TrapElement firstElement = Instantiate(currentSelection, new Vector3(0, 0, 0), Quaternion.identity);
        SetObjectKinematic(firstElement, true);
        generatedTrap.Add(firstElement);
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
                Transform plugPrefab = currentSelection.transform.GetChild(currentPlugIndex);
                Transform plugged = hit.collider.transform;
                Vector3 newElementPosition = plugged.position - plugPrefab.position;
                Vector3 normalPlug = plugPrefab.forward;
                Vector3 normalPlugged = plugged.forward;

                Vector3 axisRotation = Vector3.Cross(normalPlug, normalPlugged);
                if (Vector3.Cross(normalPlug, normalPlugged) == Vector3.zero)//if the forward vectors are in the same direction, any orthogonal axis is valid
                {
                    /*if(normalPlug != Vector3.up && normalPlug != -Vector3.up)
                        axisRotation = -Vector3.up;
                    else
                        axisRotation = -Vector3.left;*/
                    
                    if(normalPlug.x != 0)
                    {
                        axisRotation = new Vector3(normalPlug.z / normalPlug.x, 0, 1);
                    }
                    else if (normalPlug.y != 0)
                    {
                        axisRotation = new Vector3(0, normalPlug.z / normalPlug.y, 1);
                    }
                    else//normalPlug.z has to be != 0
                    {
                        axisRotation = new Vector3(0, 1, normalPlug.y / normalPlug.z);
                    }
                }

                previewedObject = Instantiate(currentSelection, newElementPosition, Quaternion.identity);
                SetObjectKinematic(previewedObject, true);
                previewedObject.transform.RotateAround(plugged.position, axisRotation, -Vector3.Angle(-normalPlugged, normalPlug));
                Transform plug = previewedObject.transform.GetChild(currentPlugIndex);
                FixedJoint newJoint = plug.gameObject.AddComponent<FixedJoint>();
                newJoint.connectedBody = hit.collider.attachedRigidbody;

                return true;
            }
            else
            {
                if (previewedObject != null)
                    if (!previewedObject.CanInstantiate())
                        return false;

                if (hit.collider.gameObject.name == "Terrain")
                    return false;

                return true;
            }
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

    void EnableTrap(List<TrapElement> trap)
    {
        foreach (TrapElement e in trap)
        {
            SetObjectKinematic(e, false);
        }
    }

    int GetNbPlugs(TrapElement trap)
    {
        int res = 0;
        for(int i = 0; i < trap.transform.childCount; i++)
        {
            Transform child = trap.transform.GetChild(i);
            if(child.tag == "Plug")
            {
                res += 1;
            }
        }

        return res;
    }

    void EventHandler()
    {
        if(Input.GetButton("Fire1"))
        {
            if(previewedObject != null)
            {
                if (previewedObject.CanInstantiate())
                {
                    generatedTrap.Add(previewedObject);
                    previewedObject.instantiateTrap();
                    previewedObject = null;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            EnableTrap(generatedTrap);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            ResetPreview();
            indexCurrentSelection += 1;
            currentPlugIndex = 0;
            indexCurrentSelection = indexCurrentSelection % availablePrefabs.Length;
            currentSelection = availablePrefabs[indexCurrentSelection];
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPreview();
            currentPlugIndex += 1;
            currentPlugIndex = currentPlugIndex % GetNbPlugs(currentSelection);
        }
    }
}
