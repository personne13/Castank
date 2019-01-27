using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    public static int unitDamage = 100;
    private int ennemyDamage = 100;

    private void Awake()
    {
        damage = unitDamage;
    }

    public new void SetEnnemy()
    {
        base.SetEnnemy();
        damage = ennemyDamage;
    }

}
