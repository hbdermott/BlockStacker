using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cube : MonoBehaviour
{
    private Rigidbody rb;
    // public float height = 0;
    public bool collided = false;
    public bool sticky = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision col)
    {
        if (sticky)
        {
            FixedJoint joint = gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = col.rigidbody;
        }

        collided = true;
    }

    void Update()
    {
        if (Camera.main.ViewportToWorldPoint(transform.position).y < -5)
            Destroy(gameObject);
    }
}
