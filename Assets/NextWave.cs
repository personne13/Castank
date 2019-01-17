﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextWave : MonoBehaviour {
    public Character soldier;
    private int waveNum = 0;
    private int nbUnits = 10;
    private int nbUnitsSent = 0;
    private bool active = false;
    private bool isParam = false;
    private float timer = 0;
    private float[] waitTime = {1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f};
    private int radius = 30;
    private float dtmin = 1.0f;
    private float dtmax = 2.0f;
    static private List<Character> Ennemies = new List<Character>();

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("d"))//FOR DEBUG
        {
            Soldier cc = Instantiate((Soldier)soldier, new Vector3(10, 0.55f, 10), Quaternion.identity);
            cc.SetEnnemy();
            Ennemies.Add(cc);

        }

        if (active && nbUnitsSent == nbUnits)
        {
            active = false;
            isParam = false;
            Debug.Log("Wave " + waveNum.ToString() + " is finished");
        }
        else if (active && nbUnitsSent != nbUnits)
        {
            timer += Time.deltaTime;
            if (timer > waitTime[nbUnitsSent])
            {
                Spoon();
            }
        }
        else if (!active && !isParam)
        {
            ParamWave();
        }
        if (Input.GetKeyDown("r") && !active)
        {
            active = true;
        }

    }

    void ParamWave()
    {
        waveNum++;
        Debug.Log("Press r to run wave " + waveNum.ToString());
        //TODO improve the wave generation
        nbUnits = nbUnits*2;
        waitTime = new float[nbUnits];
        for (int k = 0; k < nbUnits; k++)
        {
            waitTime[k] = Random.Range(dtmin, dtmax);
        }
        isParam = true;
        timer = 0;
    }


    void Spoon()
    {
        float angle = Random.Range(0.0f, 2*Mathf.PI);
        //TODO: Instantiate different kind of troops here
        Soldier c = Instantiate((Soldier)soldier, new Vector3(radius*Mathf.Cos(angle), 0.1f, radius * Mathf.Sin(angle)), Quaternion.identity);
        c.SetEnnemy();
        Ennemies.Add(c);
        nbUnitsSent++;
        timer = 0;
    }


    static public void RemoveEnnemy(Character c)
    {
        Ennemies.Remove(c);
    }

    static public Character getClosestEnnemy(Vector3 position, float maxDist)
    {
        float dist = maxDist+1;
        Character t = null;
        foreach (Character c in Ennemies)
        {
            float distCurrent = (c.transform.position - position).magnitude;
            if (distCurrent < maxDist && distCurrent < dist)
            {
                t = c;
                dist = distCurrent;
            }
        }
        return t;
    }


}
