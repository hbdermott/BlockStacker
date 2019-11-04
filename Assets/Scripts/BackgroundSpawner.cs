using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject CubePrefab;
    [SerializeField]
    private Material StickyMat;

    void Start()
    {
        StartCoroutine(SpawnCubes());
    }

    private void CreateCubes()
    {
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.1f, 0.9f), 1.1f, 10 - Camera.main.transform.position.z));
        int random = Random.Range(0, 20);
        GameObject nextGO = Instantiate(CubePrefab) as GameObject;
        if (random <= 2)
            nextGO.GetComponent<MeshRenderer>().material = StickyMat;
        else
            nextGO.GetComponent<Renderer>().material.color = UsefulFunc.GetColor();
        nextGO.transform.position = pos;
    }


    IEnumerator SpawnCubes()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);
            CreateCubes();
        }
    }
}
