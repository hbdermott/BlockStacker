using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    public GameObject cubePrefab;

    private float respawnTime = 4.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());  
    }

    private void SpawnObject()
    {
        GameObject obj = Instantiate(cubePrefab) as GameObject;
        var objRenderer = obj.GetComponent<Renderer>();
        objRenderer.material.color = Random.ColorHSV();
        obj.transform.position = new Vector3(Random.Range(2, 8), 20, 10);
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
