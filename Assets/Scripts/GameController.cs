using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject cubePrefab;
    private GameObject UI;
    private GameObject[] cube;

    private TMPro.TextMeshPro playerTM;

    private int cubesDestroyed = 0;
    private int numAllowed = 3;
    private int points = 0;
    public int height = 0;
    private float maxHeight = 0;
    private float cubeHeight = 0;




    void Start()
    {
        playerTM = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<TMPro.TextMeshPro>();
        cubeHeight = cubePrefab.GetComponent<Renderer>().bounds.size.y;
        UI = GameObject.FindGameObjectWithTag("UI");
    }

    public void DroppedCube() { cubesDestroyed++; }

    void Update()
    {
        if (cubesDestroyed >= numAllowed)
        {
            UI.GetComponent<UI>().UIMode(true);
        }
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
