using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextWave : MonoBehaviour {
    private int waveNum = 0;
    private int targetDifficulty = 0;
    private int reachedDifficulty = 0;
    private bool active = false;
    private bool isParam = false;
    static private List<Character> Ennemies = new List<Character>();

    public Text waveNumberText;

    public Character soldier;
    public Character archer;
    public Character bomber;
    int nbUnitKind = 3;
    enum Units { Soldier, Archer, Bomber};
    int[] Values = { 1, 2, 3};
    float timer = 0;
    int spawnRadius = 80;
    int kindApparitionStep = 3;
    //int levelAugmentationStep = 8;


// Use this for initialization
void Start () {
    }

    private void printWaveNumber()
    {
        waveNumberText.text = "WAVE : " + Mathf.Round(waveNum).ToString();
    }
    // Update is called once per frame
    void Update () {
        printWaveNumber();
        if (Input.GetKeyDown("d"))//FOR DEBUG
        {
            Soldier cc = Instantiate((Soldier)soldier, new Vector3(10, 0.55f, 10), Quaternion.identity);
            cc.SetEnnemy();
            Ennemies.Add(cc);

        }

        if (active && reachedDifficulty >= targetDifficulty)
        {
            active = false;
            isParam = false;
            Debug.Log("Wave " + waveNum.ToString() + " is finished");
        }
        else if (active && reachedDifficulty < targetDifficulty)
        {
            timer += Time.deltaTime;
            Spawn();
        }
        else if (!active && !isParam)
        {
            ParamWave();
        }
        if (Input.GetKeyDown("r") && !active && isParam)
        {
            active = true;
            timer = 0;
        }

    }

    void ParamWave()
    {
        waveNum++;
        Debug.Log("Press r to run wave " + waveNum.ToString());
        targetDifficulty += 10;
        reachedDifficulty = 0;
        isParam = true;
    }


    void Spawn()
    {
        if (timer > 3)
        {
            int packetDifficulty = Mathf.Min(targetDifficulty / 10, targetDifficulty - reachedDifficulty);
            List<int> Packet = new List<int>();
            int diff = 0;
            while (diff != packetDifficulty)
            {
                int u = Random.Range(0, Mathf.Min(waveNum/kindApparitionStep, nbUnitKind - 1) + 1);
                int maxValue = packetDifficulty - diff;
                if (u < nbUnitKind && Values[u] <= maxValue)
                {
                    diff += Values[u];
                    Packet.Add(u);
                } 
            }
            float angle = Random.Range(0.0f, 2 * Mathf.PI);
            Vector3 spawnCenter = new Vector3(spawnRadius * Mathf.Cos(angle), 0f, spawnRadius * Mathf.Sin(angle));
            foreach (int index in Packet)
            {
                angle = Random.Range(0.0f, 2 * Mathf.PI);
                int radius = Random.Range(2, 10);
                if (index == 0)
                {
                    Soldier c;
                    c = Instantiate((Soldier)soldier, spawnCenter + new Vector3(radius * Mathf.Cos(angle), 1f, radius * Mathf.Sin(angle)), Quaternion.identity);
                    c.SetEnnemy();
                    Ennemies.Add(c);
                }
                else if (index == 1)
                {
                    Archer c;
                    c = Instantiate((Archer)archer, spawnCenter + new Vector3(radius * Mathf.Cos(angle), 1f, radius * Mathf.Sin(angle)), Quaternion.identity);
                    c.SetEnnemy();
                    Ennemies.Add(c);
                }
                else if (index == 2)
                {
                    BomberMan c;
                    c = Instantiate((BomberMan)bomber, spawnCenter + new Vector3(radius * Mathf.Cos(angle), 1f, radius * Mathf.Sin(angle)), Quaternion.identity);
                    c.SetEnnemy();
                    Ennemies.Add(c);
                }
            }
            timer = 0;
            reachedDifficulty += packetDifficulty;
        }
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
