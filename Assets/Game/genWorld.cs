using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genWorld : MonoBehaviour {
    int R = 80;
    int nbTrees = 8000;
    float xSemiLength = 150;
    float ySemiLength = 120;
    public GameObject[] trees = new GameObject[4];
	// Use this for initialization
	void Start () {
        int k = 0;
        int it = 0;
        while (k < nbTrees && it < 1000000000)
        {
            float x = Random.Range(-xSemiLength, xSemiLength);
            float y = Random.Range(-ySemiLength, ySemiLength);
            Vector3 pos = new Vector3(x, 0, y);
            if (pos.magnitude > R - 5)
            {
                Instantiate(trees[Random.Range(0, 4)], pos, Quaternion.identity);
                k++;
            }
            it++;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
