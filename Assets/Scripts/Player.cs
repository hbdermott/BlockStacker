using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private float speed = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0) {
            var touch = Input.GetTouch(0);
            if (touch.position.x > Screen.width / 2)
                rb.velocity += new Vector3(speed, 0, 0);
            // rb.AddForce(new Vector3(speed, 0, 0));
            else
                rb.velocity += new Vector3(-speed, 0, 0);
            // rb.AddForce(new Vector3(-speed, 0, 0));
        }
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        if (pos.x == 0)
        {
            rb.velocity = new Vector3(speed, 0, 0);
           // rb.AddForce(new Vector3(speed * 2, 0, 0));
        }

        if (pos.x == 1)
        {
            rb.velocity = new Vector3(-speed, 0, 0);
           // rb.AddForce(new Vector3(-speed * 2, 0, 0));
        }
        //transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
}
