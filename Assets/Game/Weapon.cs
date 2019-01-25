using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour{
    protected int damage = 0;
    protected bool isEnnemy = false;



    public void SetEnnemy() {
        isEnnemy = true;
    }

    protected void OnCollisionEnter(Collision collision)
    {
        Character c = collision.gameObject.GetComponent<Character>();
        if (c != null) {
            c.damage(damage, isEnnemy);
        }
        Game g = collision.gameObject.GetComponent<Game>();
        if (g != null) {
            g.damage(damage, isEnnemy);
        }


    }
}
