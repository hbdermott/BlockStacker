﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    public float speed = 0.0f;
    private Vector3 move;
    public bool paused;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            move = new Vector3(speed, 0, 0);
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                if (touch.position.x > Screen.width / 2)
                    rb.velocity += move;
                else
                    rb.velocity -= move;
            }
            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
            pos.x = Mathf.Clamp01(pos.x);
            if (pos.x == 0)
                rb.velocity = move;
            else if (pos.x == 1)
                rb.velocity = -move;
        }

    }
}
