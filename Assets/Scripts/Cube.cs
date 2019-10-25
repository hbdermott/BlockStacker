using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cube : MonoBehaviour
{
    private Rigidbody rb;
    private ParticleSystem ps;
    public float height = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision col)
    {
        height = transform.position.y;
    }

    void Update()
    {
        if (transform.position.y < -3)
            Destroy(gameObject);
    }
}
