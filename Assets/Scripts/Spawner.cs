
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject CubePrefab;
    [SerializeField]
    private GameObject BombPrefab;
    [SerializeField]
    private Material StickyMat;


    private GameObject nextGO;
    private UI UI;

    [SerializeField]
    private float respawnTime = 4.0f;

    
    void Start()
    {
        UI = GameObject.FindGameObjectWithTag("UI").GetComponent<UI>();
        CreateObject();
        StartCoroutine(Spawn());  
    }

    public static Color GetColor()
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


    private void CreateObject()
    {
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.1f, 0.9f), 1.1f, 10 - Camera.main.transform.position.z));
        int random = Random.Range(0, 20);
        nextGO = Instantiate(CubePrefab) as GameObject;
        if (random == 0)
        {
            nextGO = Instantiate(BombPrefab) as GameObject;
        }
        else if (random <= 2)
        {
            nextGO.GetComponent<Cube>().sticky = true;
            nextGO.GetComponent<MeshRenderer>().material = StickyMat;
        }
        else
        {
            nextGO.GetComponent<Renderer>().material.color = GetColor();
        }
        nextGO.transform.position = pos;
        UI.SetNext(nextGO);
        nextGO.SetActive(false);
    }

    private void SpawnObject()
    {
        nextGO.SetActive(true);
        CreateObject();
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
