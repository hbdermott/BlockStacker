using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCam : MonoBehaviour
{
    private Point pointSystem;
    private Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        pointSystem = GameObject.FindGameObjectWithTag("Point").GetComponent<Point>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > (initialPos.z - pointSystem.points))
            transform.position -= new Vector3(0, 0, 0.01f);
        else if (transform.position.z < (initialPos.z - pointSystem.points))
            transform.position += new Vector3(0, 0, 0.01f);
    }
}
