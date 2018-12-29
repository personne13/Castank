using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextWave : MonoBehaviour {
    public Character character;
    private int waveNum = 0;
    private int nbUnits = 10;
    private int nbUnitsSent = 0;
    private bool active = false;
    private bool isParam = false;
    private float timer = 0;
    private float[] waitTime = {1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f};
    private int radius = 20;
    private float dtmin = 1.0f;
    private float dtmax = 2.0f;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        
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
        nbUnits = nbUnits*2;
        waitTime = new float[nbUnits];
        for (int k = 0; k < nbUnits; k++)
        {
            waitTime[k] = Random.Range(dtmin, dtmax);
        }
        timer = 0;
        isParam = true;
    }


    void Spoon()
    {
        float angle = Random.Range(0.0f, 2*Mathf.PI);
        Character c = Instantiate(character, new Vector3(radius*Mathf.Cos(angle), 0.1f, radius * Mathf.Sin(angle)), Quaternion.identity);
        c.SetEnnemy();
        nbUnitsSent++;
        timer = 0;
    }
}
