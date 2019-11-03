using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cube : MonoBehaviour
{
    public bool collided = false;
    public bool sticky = false;
    public bool heavy = false;

    private Renderer ren;
    private Scene gameController;

    private AudioSource audio;
    [SerializeField]
    private AudioClip stickAudio;
    [SerializeField]
    private AudioClip collideAudio;
    private List<int> cubeCol = new List<int>();


   

    void Start()
    {
        ren = GetComponent<Renderer>();
        audio = GetComponent<AudioSource>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<Scene>();
    }

    void OnCollisionEnter(Collision col)
    {
        int id = col.gameObject.GetInstanceID();
        if (cubeCol.Contains(id))
            return;
        else if (col.gameObject.tag != "Player" && col.gameObject.tag != "Cube")
        {
            Physics.IgnoreCollision(col.collider, GetComponent<Collider>());
            return;
        }
        cubeCol.Add(id);
        if ((sticky || col.gameObject.GetComponent<Renderer>().material.color == ren.material.color || (col.gameObject.tag != "Player" && col.gameObject.GetComponent<Cube>().sticky)))
        {
            FixedJoint joint = gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = col.rigidbody;
            audio.clip = stickAudio;
        }
        else
            audio.clip = collideAudio;
        if(id < GetInstanceID() || col.gameObject.tag == "Player")
            audio.Play();
        collided = true;
    }
    
    public void DestroyCube(float time = 0)
    {
        Destroy(gameObject, time);
        gameController.cubesDestroyed++;
    }


    void Update()
    {
        if (Camera.main.ViewportToWorldPoint(transform.position).y < -5)
            DestroyCube();
    }



}
