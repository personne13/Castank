﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Soldier : Character {
    public Sword sword;
    private Rigidbody srb;
    //private Weapon s;
    public static int unitLife = 1000;
    private int ennemyLife = 1000;
    public Image healthBar;

    // Use this for initialization
    new void Awake () {
        life = unitLife;
        speed = 15f;
        sword = Instantiate(sword, transform.position + transform.forward*0.5f, Quaternion.identity);
        srb = sword.GetComponent<Rigidbody>();
        gameObject.AddComponent<FixedJoint>().connectedBody = srb;
        ressourceGain = 1;
        base.Awake();
    }

    private void SetHealthBar()
    {
        healthBar.fillAmount = (float)life / (float)startLife;
    }

    // Update is called once per frame
    new private void Update()
    {
        SetHealthBar();

        if (life <= 0)
        {
            if (isEnnemy)
            {
                Game.addRessources(ressourceGain);
            }
            Destroy(sword.gameObject);
        }
        base.Update();
    }


    private void FixedUpdate () {
        float step = speed * Time.fixedDeltaTime;
        Character target = null;
        if (isEnnemy)
        {
            target = Game.getClosestTroop(transform.position, 20);
        }
        else
        {
            target = NextWave.getClosestEnnemy(transform.position, 30);
        }
        if (target != null || isEnnemy)
        {
            Vector3 newDir;
            float dist;
            Vector3 targetPos;
            if (target == null && isEnnemy)
            {
                newDir = Vector3.RotateTowards(new Vector3(transform.forward.x, 0, transform.forward.z), -new Vector3(transform.position.x, 0, transform.position.z), 0.1f, 0.1f);
                dist = transform.position.magnitude;
                targetPos = new Vector3(0, 0, 0);
            }
            else
            {
                newDir = Vector3.RotateTowards(new Vector3(transform.forward.x, 0, transform.forward.z), new Vector3(target.transform.position.x, 0, target.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z), 0.1f, 0.1f);
                targetPos = target.transform.position;
                dist = (targetPos - transform.position).magnitude;
            }
            float dTheta = 0f;
            if (dist < 5f)
            {
                dTheta = Random.Range(-20f, 20f);
            }
            transform.rotation = Quaternion.LookRotation(newDir) * Quaternion.Euler(0f, dTheta, 0f);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, step);

        }
        else
        {
            Vector3 pos = transform.position - spawnPosition;
            if (pos.magnitude > 0.2)
            {
                transform.position = Vector3.MoveTowards(transform.position, spawnPosition, step);
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
        life = ennemyLife;
        startLife = life;
        if (sword != null)
        {
            sword.SetEnnemy();
        }
    }

    
}