using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    protected Rigidbody rb;
    protected int life = 100;
    protected float speed = 10.0f;
    public bool isEnnemy = false;

    // Use this for initialization
    void Start () {
        rb = gameObject.GetComponent<Rigidbody>();

    }

    public void SetEnnemy()
    {
        isEnnemy = true;
    }

    // Update is called once per frame
    void Update () {
        if (life == 0) {
            Debug.Log("dead");
            Destroy(gameObject);
        }
	}

    private void FixedUpdate() {
        rb.AddForce(-transform.position.normalized * speed);
    }

    public void damage(int d) {
        life -= d;
    }

}
