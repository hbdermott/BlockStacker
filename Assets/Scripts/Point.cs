using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField]
    private GameObject cubePrefab;
    private float cubeHeight = 0;
    private GameObject[] cube;
    
    private int points = 0;
    public int height = 0;

    private float maxHeight = 0;

    private TMPro.TextMeshPro playerTM;

    void Start()
    {
        playerTM = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<TMPro.TextMeshPro>();
        cubeHeight = cubePrefab.GetComponent<Renderer>().bounds.size.y;
    }

    void Update()
    {
        cube = GameObject.FindGameObjectsWithTag("Cube");
        points = 0;
        height = 0;
        maxHeight = 0;
        for(int i = 0; i < cube.Length; i++)
        {
            if (cube[i].GetComponent<Cube>().collided)
            {
                points++;
                maxHeight = Mathf.Max(cube[i].GetComponent<Cube>().transform.position.y, maxHeight);
            } 
        }
        height = (int)(maxHeight / cubeHeight);
        playerTM.text = points.ToString();
    }
}
