using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Weapon
{

    private void Awake()
    {
        damage = 50;
    }

    new private void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        Destroy(gameObject);

    }
}
