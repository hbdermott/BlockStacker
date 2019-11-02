
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    public GameObject cubePrefab;
    [SerializeField]
    private GameObject bombPrefab;
    [SerializeField]
    private Material stickyMat;
    [SerializeField]
    private Material heavyMat;


    [SerializeField]
    private float respawnTime = 4.0f;
    
    void Start()
    {
        StartCoroutine(Spawn());  
    }

    private void SpawnObject()
    {
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.1f, 0.9f), 1.1f, 10 - Camera.main.transform.position.z));
        int random = Random.Range(0, 20);
        GameObject obj = Instantiate(cubePrefab) as GameObject;
        if(random == 0)
            obj = Instantiate(bombPrefab) as GameObject;
        else if(random <= 2)
        {
            obj.GetComponent<Cube>().sticky = true;
            obj.GetComponent<MeshRenderer>().material = stickyMat;
        }
        else
            obj.GetComponent<Renderer>().material.color = GetColor();
        obj.transform.position = pos;
    }

    private Color GetColor()
    {
        Color color;
        int val = Random.Range(0, 7);
        switch (val)
        {
            case 0:
                color = new Color(1, 0, 0);
                break;
            case 1:
                color = new Color(0, 1, 0);
                break;
            case 2:
                color = new Color(0, 0, 1);
                break;
            case 3:
                color = new Color(0, 1, 1);
                break;
            case 4:
                color = new Color(1, 1, 0);
                break;
            case 5:
                color = new Color(1, 0, 1);
                break;
            default:
                color = new Color(1, 1, 1);
                break;
        }
        return color;
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnObject();
        }
    }
}
