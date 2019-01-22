using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    protected Rigidbody rb;
    protected int life = 100;
    protected float speed = 15.0f;
    protected bool isEnnemy = false;
    protected Vector3 spoonPosition;

    // Use this for initialization
    protected void Awake () {
        rb = gameObject.GetComponent<Rigidbody>();
        spoonPosition = transform.position;

    }

    public virtual void SetEnnemy()
    {
        isEnnemy = true;
        GetComponent<Renderer>().material.color = Color.red;
    }

    public bool isEnnemyBool()
    {
        return isEnnemy;
    }

    // Update is called once per frame
    protected void Update () {
        
        if (life <= 0 || transform.position.y <-2) {
            //Debug.Log("dead");
            if (isEnnemy)
            {
                NextWave.RemoveEnnemy(this);
            }
            else
            {
                Game.RemoveTroop(this);

            }
            Destroy(gameObject);
        }
	}



    


    public void damage(int d, bool ennemyDamage) {
        if (ennemyDamage!=isEnnemy)
        {
            life -= d; ;
        }
        
    }

}
