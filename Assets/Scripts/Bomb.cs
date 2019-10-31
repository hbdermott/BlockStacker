using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        if(obj != player)
            Destroy(collision.gameObject);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.ViewportToWorldPoint(transform.position).y < -5)
            Destroy(gameObject);
    }
}
