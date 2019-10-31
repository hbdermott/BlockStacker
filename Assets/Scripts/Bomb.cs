using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private GameObject[] children;
    private ParticleSystem[] explosion;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        children = new GameObject[gameObject.transform.childCount];
        for (int i = 0; i < gameObject.transform.childCount; i++)
            children[i] = gameObject.transform.GetChild(i).gameObject;
        explosion = new ParticleSystem[2];
        explosion[0] = children[children.Length - 1].GetComponent<ParticleSystem>();
        explosion[1] = children[children.Length - 1].transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider col)
    {
        GameObject obj = col.gameObject;
        if(obj != player)
            Destroy(obj);

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
