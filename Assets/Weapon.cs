using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour{
    protected int damage = 100;
    public bool isEnnemy = false;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetEnnemy() {
        isEnnemy = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Character c = collision.gameObject.GetComponent<Character>();
        if (c != null) {
            c.damage(damage);
        }
        Game g = collision.gameObject.GetComponent<Game>();
        if (g != null) {
            g.damage(damage, isEnnemy);
        }
    }
}
