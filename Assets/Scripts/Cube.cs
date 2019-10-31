using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cube : MonoBehaviour
{
    private Rigidbody rb;
    public bool collided = false;
    public bool sticky = false;
    public bool heavy = false;

    private GameObject gameController;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameController = GameObject.FindGameObjectWithTag("GameController");
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
        {
            Destroy(gameObject);
            gameController.GetComponent<Scene>().droppedCubes++;
        }
    }
}
