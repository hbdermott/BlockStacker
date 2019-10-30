using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCam : MonoBehaviour
{
    private Point pointSystem;
    private GameObject player;
    private Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        initialPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        pointSystem = GameObject.FindGameObjectWithTag("Point").GetComponent<Point>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > (initialPos.z - pointSystem.height))
            transform.position -= new Vector3(0, 0, 0.01f);
        else if (transform.position.z < (initialPos.z - pointSystem.height))
            transform.position += new Vector3(0, 0, 0.01f);
        if(Camera.main.WorldToViewportPoint(player.transform.position).y > 0.1)
            transform.position += new Vector3(0, 0.001f, 0);
        else if(Camera.main.WorldToViewportPoint(player.transform.position).y < 0.1)
            transform.position -= new Vector3(0, 0.001f, 0);
    }
}
