using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Weapon
{
    public static int unitDamage = 100;
    private int ennemyDamage = 100;

    private void Awake()
    {
        damage = unitDamage;
    }

    new private void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        Destroy(gameObject);
    }

    public new void SetEnnemy()
    {
        base.SetEnnemy();
        damage = ennemyDamage;
    }
}
