using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plug : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int layerPlug = LayerMask.NameToLayer("Plug");
        Physics.IgnoreLayerCollision(layerPlug, layerPlug);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
