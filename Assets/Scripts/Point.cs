using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    private GameObject[] cube;

    [SerializeField]
    private GameObject cubePrefab;

    private TMPro.TextMeshPro playerTM;

    private float cubeHeight = 0;
    public int points = 0;
 

    void Start()
    {
        cubeHeight = cubePrefab.GetComponent<Renderer>().bounds.size.y;
        playerTM = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<TMPro.TextMeshPro>();
    }
    void Update()
    {
        float maxHeight = 0;
        cube = GameObject.FindGameObjectsWithTag("Cube");
        for(int i = 0; i < cube.Length; i++)
        {
            maxHeight = Mathf.Max(cube[i].GetComponent<Cube>().height, maxHeight);
        }
        points = (int)(maxHeight / cubeHeight);
        playerTM.text = points.ToString();
    }
}
