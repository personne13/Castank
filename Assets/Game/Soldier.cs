using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Character {
    public Weapon sword;
    private Rigidbody srb;
    private Weapon s;
    private int ressourceGain = 1;

    // Use this for initialization
    new void Awake () {
        base.Awake();
        s = Instantiate(sword, transform.position + transform.forward, Quaternion.identity);
        srb = s.GetComponent<Rigidbody>();
        gameObject.AddComponent<FixedJoint>().connectedBody = srb;
    }

    // Update is called once per frame
    new private void Update()
    {
        if (life <= 0)
        {
            Game.addRessources(ressourceGain);
            Destroy(s.gameObject);
        }
        base.Update();
    }


    private void FixedUpdate () {
        float step = speed * Time.fixedDeltaTime;
        if (isEnnemy)
        {
            Character target = Game.getClosestTroop(transform.position, 20);
            if (target != null)
            {
                Vector3 newDir = Vector3.RotateTowards(new Vector3(transform.forward.x, 0, transform.forward.z), new Vector3(target.transform.position.x, 0, target.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z), 0.1f, 0.1f);
                transform.rotation = Quaternion.LookRotation(newDir);
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
            }
            else
            {
                Vector3 newDir = Vector3.RotateTowards(new Vector3(transform.forward.x, 0, transform.forward.z) , -new Vector3(transform.position.x, 0, transform.position.z), 0.1f, 0.1f);
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
                if (pos.magnitude > 0.2)
                {
                    transform.position = Vector3.MoveTowards(transform.position, spoonPosition, step);
                }
            }
            
        }
        if (rb.velocity.magnitude > speed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, speed);
        }
        
    }

 

    public new void SetEnnemy()
    {
        base.SetEnnemy();
        if (s != null)
        {
            s.SetEnnemy();
        }
    }

    
}
