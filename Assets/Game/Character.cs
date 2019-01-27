using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour {
    protected Rigidbody rb;
    protected int life;
    protected float speed = 15.0f;
    protected bool isEnnemy = false;
    protected Vector3 spawnPosition;
    protected static int cost;
    protected int ressourceGain;

    protected int startLife;

    // Use this for initialization
    protected void Awake () {
        rb = gameObject.GetComponent<Rigidbody>();
        spawnPosition = transform.position;
        startLife = life;
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
        /*
        if (transform.position.y > 0.55f)
        {
            transform.position = new Vector3(transform.position.x, 0.55f, transform.position.z);
        }
        */
	}






    public void damage(int d, bool ennemyDamage) {
        if (ennemyDamage!=isEnnemy)
        {
            life -= d; ;
        }

    }

    static public int Cost()
    {
        return cost;
    }

}
