using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddElement : MonoBehaviour
{
    public GameObject currentSelection;
    private int currentPlugIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckCursorCollision();
    }

    void CheckCursorCollision()
    {
        if(Input.GetButton("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.name == "plug")
                {
                    Transform attachedElement = hit.collider.gameObject.transform.parent;
                    Transform plug = currentSelection.transform.GetChild(currentPlugIndex);
                    Transform plugged = hit.collider.transform;
                    Vector3 newElementPosition = plugged.position - plug.position;
                    Vector3 normalPlug = plug.forward;
                    Vector3 normalPlugged = plugged.forward;

                    GameObject newElement = Instantiate(currentSelection, newElementPosition, attachedElement.rotation);
                    newElement.transform.RotateAround(plugged.position, Vector3.Cross(normalPlug, normalPlugged), -Vector3.Angle(normalPlug, normalPlugged));
                }
            }
        }
    }
}
