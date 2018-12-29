using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
    public Character soldier;
    private int CastleLife = 100;
    static private int Ressources = 100;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (CastleLife == 0) {
            Debug.Log("Perdu");
        }
        if (Input.GetKeyDown("i") && Ressources>=50)
        {
            Character c = Instantiate(soldier, new Vector3(5,0.55f,5), Quaternion.identity);
            c.isEnnemy = false;
        }
    }

    public void damage(int d, bool projectileEnnemy)
    {
        if (projectileEnnemy) {
            CastleLife -= d;
        }
    }

    public static void addRessources(int ressourceToAdd)
    {
        Ressources += ressourceToAdd;
    }
}
