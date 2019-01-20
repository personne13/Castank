using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapElement : MonoBehaviour
{
    private bool isCollided = false;
    private bool isInstantiated = false;
    private bool hasBeenUpdated = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void LateUpdate()
    {
        hasBeenUpdated = true;
    }

    public bool CanInstantiate()
    {
        return !isCollided && hasBeenUpdated;
    }

    public void instantiateTrap()
    {
        isInstantiated = true;
    }

    void OnTriggerStay(Collider collision)
    {
        if (!isInstantiated)
        {
            if(collision.gameObject.layer != LayerMask.NameToLayer("Plug"))
            {
                isCollided = true;
                Renderer rend = GetComponent<Renderer>();
                rend.material.SetColor("_Color", Color.red);
            }
        }
    }
}
