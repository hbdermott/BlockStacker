using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private GameObject[] children;
    private AudioSource bomb;
    private AudioSource explosionAudio;
    private ParticleSystem[] explosion = new ParticleSystem[2];
    private GameObject explosionHandler;
    private bool played = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        explosionHandler = GameObject.FindGameObjectWithTag("Explosion");
        bomb = GetComponent<AudioSource>();
        children = new GameObject[gameObject.transform.childCount];
        for (int i = 0; i < gameObject.transform.childCount; i++)
            children[i] = gameObject.transform.GetChild(i).gameObject;
        explosion[0] = children[children.Length - 1].GetComponent<ParticleSystem>();
        explosionAudio = explosion[0].GetComponent<AudioSource>();
        explosion[1] = children[children.Length - 1].transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider col)
    {
        GameObject obj = col.gameObject;
        if (obj != player)
            explosionHandler.GetComponent<Explosion>().explode(obj);
        if (!played)
        {
            bomb.Stop();
            explosionAudio.Play();
            played = true;
        }
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        for (int i = 0; i < children.Length - 1; i++)
            children[i].SetActive(false);
        for (int i = 0; i < explosion.Length; i++)
            explosion[i].Play();
        Destroy(gameObject, explosion[0].main.duration);
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.ViewportToWorldPoint(transform.position).y < -5)
            Destroy(gameObject);
    }
}
