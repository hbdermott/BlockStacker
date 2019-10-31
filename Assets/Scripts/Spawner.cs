
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
        if(Random.Range(0,20) == 0)
        {
            GameObject bomb = Instantiate(bombPrefab) as GameObject;
            bomb.transform.position = pos;
            return;
        }

        GameObject obj = Instantiate(cubePrefab) as GameObject;
        var objRenderer = obj.GetComponent<Renderer>();
        objRenderer.material.color = Random.ColorHSV(0, 1, 1, 1, 1, 1);
        obj.transform.position = pos;
        if (Random.Range(0, 10) == 0)
        {
            obj.GetComponent<Cube>().sticky = true;
            obj.GetComponent<MeshRenderer>().material = stickyMat;
            objRenderer.material.color = Color.white;
        }

   
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
