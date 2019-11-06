using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCam : MonoBehaviour
{
    private GameController pointSystem;
    private GameObject player;
    private Vector3 initialPos;
    private Vector3 playerSize;
    private float curClipPlane;

    [SerializeField]
    private float mult = 1.0f;
    
    [SerializeField]
    private float zspeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    { 
        player = GameObject.FindGameObjectWithTag("Player");
        pointSystem = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        playerSize = player.GetComponent<Collider>().bounds.size;
        initialPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        curClipPlane = Camera.main.farClipPlane;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (transform.position.z > (initialPos.z - pointSystem.height * mult))
            transform.position -= new Vector3(0, 0, 0.01f * zspeed);
        else if (transform.position.z < (initialPos.z - pointSystem.height * mult))
            transform.position += new Vector3(0, 0, 0.01f * zspeed);
        if(Camera.main.WorldToViewportPoint(player.transform.position - playerSize).y > Camera.main.ScreenToViewportPoint(Vector3.zero).y)
            transform.position += new Vector3(0, 0.001f * zspeed, 0);
        else if(Camera.main.WorldToViewportPoint(player.transform.position - playerSize).y < Camera.main.ScreenToViewportPoint(Vector3.zero).y)
            transform.position -= new Vector3(0, 0.001f * zspeed, 0);
        if(player.transform.position.z - transform.position.z > curClipPlane)
        {
            curClipPlane += curClipPlane;
            Camera.main.farClipPlane = curClipPlane;
        }

    }
}
