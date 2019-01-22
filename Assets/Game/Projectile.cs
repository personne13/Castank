using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Weapon
{

    private void OnCollisionEnter(Collision collision)
    {
        Character c = collision.gameObject.GetComponent<Character>();
        if (c != null)
        {
            c.damage(damage, isEnnemy);
        }
        Game g = collision.gameObject.GetComponent<Game>();
        if (g != null)
        {
            g.damage(damage, isEnnemy);
        }
        
        Destroy(gameObject);

    }
}
