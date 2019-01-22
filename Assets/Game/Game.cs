using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
    public Character soldier;
    public Character archer;
    private int CastleLife = 10000;
    static private int Ressources = 100000;
    static private List<Character> Troops = new List<Character>();

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update()
    {
        if (CastleLife <= 0)
        {
            Debug.Log("Game over");
        }
        if (Input.GetKeyDown("s"))
        {
            int ressourcesNeeded = 50;
            if (Ressources >= ressourcesNeeded)
            {
                float R = 10;
                for (int k =0; k < R; k++)
                {
                    Soldier c = Instantiate((Soldier)soldier, new Vector3(10*Mathf.Cos(2*Mathf.PI*k/R), 0.55f, 10*Mathf.Sin(2* Mathf.PI*k / R)), Quaternion.identity);
                    Troops.Add(c);

                }
                Ressources -= ressourcesNeeded;
            }
            else
            {
                Debug.Log(string.Concat("Not enought ressources for soldier : ", Ressources.ToString(),"/", ressourcesNeeded.ToString()) );
            }
        }
        if (Input.GetKeyDown("q"))
        {
            int ressourcesNeeded = 80;
            if (Ressources >= ressourcesNeeded)
            {
                float R = 5;
                for (int k = 0; k < R; k++)
                {
                    Archer c = Instantiate((Archer)archer, new Vector3(10 * Mathf.Cos(2 * Mathf.PI * k / R), 0.55f, 10 * Mathf.Sin(2 * Mathf.PI * k / R)), Quaternion.identity);
                    Troops.Add(c);

                }
                Ressources -= ressourcesNeeded;
            }
            else
            {
                Debug.Log(string.Concat("Not enought ressources for archer : ", Ressources.ToString(), "/", ressourcesNeeded.ToString()));
            }
        }
    }

    public void damage(int d, bool ennemyDamage)
    {
        if (ennemyDamage) {
            CastleLife -= d;
            Debug.Log(string.Concat("Castle life : ", CastleLife.ToString()));
        }
    }

    public static void addRessources(int ressourceToAdd)
    {
        Ressources += ressourceToAdd;
    }

    static public void RemoveTroop(Character c)
    {
        Troops.Remove(c);
    }

    static public Character getClosestTroop(Vector3 position, float maxDist)
    {
        float dist = maxDist + 1;
        Character t = null;
        foreach (Character c in Troops)
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
