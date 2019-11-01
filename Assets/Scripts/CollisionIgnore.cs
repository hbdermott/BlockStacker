using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionIgnore : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
        if(collision.gameObject.tag == "Cube")
        {
            Debug.Log(collision);
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }
}
