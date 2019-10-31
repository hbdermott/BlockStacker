using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject wick;
    private GameObject fire;
    private GameObject player;
    private ParticleSystem[] ps;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        /*
        wick = gameObject.transform.GetChild(2).gameObject;
        Debug.Log(wick);
        fire = wick.transform.GetChild(1).gameObject;
        Debug.Log(fire);
        int fireChild = fire.gameObject.transform.childCount;
        ps = new ParticleSystem[1 + fireChild];
        ps[0] = fire.GetComponent<ParticleSystem>();
        ps[0].Play();
        for(int i = 0; i < fireChild; i++)
        {
           
            ps[i] = fire.gameObject.transform.GetChild(i).GetComponent<ParticleSystem>();
            ps[i].Play();
            Debug.Log(ps[i]);
        }
        */
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
