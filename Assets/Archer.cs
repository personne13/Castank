using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Character {

    public Weapon projectile;
    private Weapon p;
    private int ressourceGain = 2;
    //new private int life = 200;
    private int scope = 30;

    // Use this for initialization
    new void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    new private void Update()
    {
        if (life <= 0)
        {
            Game.addRessources(ressourceGain);
        }
        base.Update();
    }


    private void FixedUpdate()
    {
        float step = speed * Time.fixedDeltaTime;
        if (isEnnemy)
        {
            Character target = Game.getClosestTroop(transform.position, 40);
            if (target != null)
            {
                Vector3 newDir = Vector3.RotateTowards(new Vector3(transform.forward.x, 0, transform.forward.z), new Vector3(target.transform.position.x, 0, target.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z), 0.1f, 0.1f);
                transform.rotation = Quaternion.LookRotation(newDir);
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
            }
            else
            {
                Vector3 newDir = Vector3.RotateTowards(new Vector3(transform.forward.x, 0, transform.forward.z), -new Vector3(transform.position.x, 0, transform.position.z), 0.1f, 0.1f);
                transform.rotation = Quaternion.LookRotation(newDir);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, 0), step);
            }
        }
        else
        {
            Character target = NextWave.getClosestEnnemy(transform.position, 30);
            if (target != null)
            {
                Vector3 newDir = Vector3.RotateTowards(new Vector3(transform.forward.x, 0, transform.forward.z), new Vector3(target.transform.position.x, 0, target.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z), 0.1f, 0.1f);
                transform.rotation = Quaternion.LookRotation(newDir);
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
            }
            else
            {
                Vector3 pos = transform.position - spoonPosition;
                transform.position = Vector3.MoveTowards(transform.position, spoonPosition, step);
            }

        }
        if (rb.velocity.magnitude > speed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, speed);
        }

    }



    public new void SetEnnemy()
    {
        GetComponent<Renderer>().material.color = Color.red;
        isEnnemy = true;
    }


}
