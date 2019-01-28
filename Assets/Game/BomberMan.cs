using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberMan : Character
{

    public Weapon bomb;
    //new private int life = 200;
    private int scope = 15;
    private float deltaTimeShoot = 2f;
    private float timer = 0.0f;
    private bool isShooting = false;

    public static int unitLife = 500;
    private int ennemyLife = 500;



    // Use this for initialization
    new void Awake()
    {
        base.Awake();
        life = 500;
        speed = 6f;
        ressourceGain = 2;
    }

    // Update is called once per frame
    new private void Update()
    {
        if (life <= 0)
        {
            if (isEnnemy)
            {
                Game.addRessources(ressourceGain);
            }
        }
        base.Update();
    }


    private void FixedUpdate()
    {
        float step = speed * Time.fixedDeltaTime;
        if (isEnnemy)
        {
            Character target = Game.getClosestTroop(transform.position, 30);
            archerBehaviour(target, step);
            if (target == null)
            {
                if (transform.position.magnitude > scope / 2.0f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, 0), step);
                }
            }
        }
        else
        {
            Character target = NextWave.getClosestEnnemy(transform.position, 30);
            archerBehaviour(target, step);
            if (target == null)
            {
                Vector3 pos = transform.position - spawnPosition;
                if (pos.magnitude > 0.2)
                {
                    transform.position = Vector3.MoveTowards(transform.position, spawnPosition, step);
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
        GetComponent<Renderer>().material.color = Color.red;
        isEnnemy = true;
        life = ennemyLife;
        startLife = life;
    }


    private void archerBehaviour(Character target, float step)
    {
        Vector3 targetPos = new Vector3(0, 0, 0);
        if (target != null)
        {
            targetPos = target.transform.position;
        }

        if (target != null || isEnnemy)
        {
            Vector3 newDir = Vector3.RotateTowards(new Vector3(transform.forward.x, 0, transform.forward.z), new Vector3(targetPos.x, 0, targetPos.z) - new Vector3(transform.position.x, 0, transform.position.z), 0.1f, 0.1f);
            transform.rotation = Quaternion.LookRotation(newDir);
            Vector3 v = targetPos - transform.position;
            if (v.magnitude > scope * 0.9f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
            }
            else if (v.magnitude < 5)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position - v, step);
            }
            if (v.magnitude < scope)
            {
                if (!isShooting)
                {
                    timer = 0.0f;
                }
                else
                {
                    timer += Time.fixedDeltaTime;
                }
                if (timer > deltaTimeShoot)
                {
                    Bomb p = Instantiate((Bomb)bomb, transform.position + transform.forward + new Vector3(0, 0.5f, 0), Quaternion.identity);
                    if (isEnnemy)
                    {
                        p.SetEnnemy();
                    }
                    p.GetComponent<Rigidbody>().velocity = transform.forward.normalized * 30;
                    timer = 0.0f;
                }
                isShooting = true;

            }
            else
            {
                isShooting = false;
            }
        }
        else
        {
            Vector3 newDir = Vector3.RotateTowards(new Vector3(transform.forward.x, 0, transform.forward.z), -new Vector3(transform.position.x, 0, transform.position.z), 0.1f, 0.1f);
            transform.rotation = Quaternion.LookRotation(newDir);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, 0), step);
        }
    }

}
