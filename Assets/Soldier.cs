using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Character {
    public Weapon sword;
    private Rigidbody srb;
    // Use this for initialization
    void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        Weapon s = Instantiate(sword, transform.position + new Vector3(1,0,1), Quaternion.identity);
        srb = s.GetComponent<Rigidbody>();
        gameObject.AddComponent<FixedJoint>().connectedBody = srb;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKey("up"))
        {
            rb.AddForce(new Vector3(1, 0, 0) * speed);
            srb.AddForce(new Vector3(1, 0, 0) * speed);
        }
        if (Input.GetKey("down"))
        {
            rb.AddForce(new Vector3(-1, 0, 0) * speed);
            srb.AddForce(new Vector3(-1, 0, 0) * speed);
        }
        if (Input.GetKey("left"))
        {
            rb.AddForce(new Vector3(0, 0, 1) * speed);
            srb.AddForce(new Vector3(0, 0, 1) * speed);
        }
        if (Input.GetKey("right"))
        {
            rb.AddForce(new Vector3(0, 0, -1) * speed);
            srb.AddForce(new Vector3(0, 0, -1) * speed);
        }
    }
}
