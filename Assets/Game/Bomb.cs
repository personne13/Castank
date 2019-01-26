using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Weapon
{
    int R = 5;
    bool hasCollide = false;
    Rigidbody rb;
    List<Collider> colliders = new List<Collider>();

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        damage = 100;
    }

    new private void OnCollisionEnter(Collision collision)
    {
        if (hasCollide)
        {
            Character c = collision.gameObject.GetComponent<Character>();
            if (c != null && !colliders.Contains(collision.collider))
            {
                float dist = (c.transform.position - transform.position).magnitude;
                c.damage((int)(damage * Mathf.Pow(dist/(float)R - 1, 2)), isEnnemy);
                colliders.Add(collision.collider);
            }
            Game g = collision.gameObject.GetComponent<Game>();
            if (g != null)
            {
                g.damage(damage, isEnnemy);
            }
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.black;
            hasCollide = true;
            Destroy(rb);
            //rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
            transform.localScale = Vector3.zero;
        }

    }

    private void Update()
    {

        if (transform.localScale.x > R)
            Destroy(gameObject);

        if (hasCollide)
        {
            float explosionSpeed = 0.2f;
            transform.localScale += new Vector3(explosionSpeed, explosionSpeed, explosionSpeed);
        }
    }
}
