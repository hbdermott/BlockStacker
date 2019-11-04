using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject cubePrefab;
    private UI UI;
    private GameObject[] cube;



    private int cubesDestroyed = 0;
    private int numAllowed = 3;
    private int points = 0;
    public int height = 0;
    private float maxHeight = 0;
    private float cubeHeight = 0;
    UserData user;
    private bool userDataSet = false;

    void Start()
    {
        cubeHeight = cubePrefab.GetComponent<Renderer>().bounds.size.y;
        UI = GameObject.FindGameObjectWithTag("UI").GetComponent<UI>();
        user = SaveSystem.LoadUser();
        if (user == null)
        {
            user = new UserData();
        }
    }

    public void Restart()
    {
        SaveSystem.SaveUser(user);
        SceneManager.LoadScene("main");
        UI.UIMode(false);
        userDataSet = false;
    }

    public void Continue()
    {
        UI.UIMode(false);
        cubesDestroyed = 0;
        userDataSet = false;
    }

    void OnApplicationQuit()
    {
        SaveSystem.SaveUser(user);
    }

    public void DroppedCube() { cubesDestroyed++; }

    void Update()
    {
        if (cubesDestroyed >= numAllowed)
        {
            if (!userDataSet)
                user.Update(points);
            UI.SetScores(points, user.highScore);
            UI.UIMode(true);
            userDataSet = true;
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
        UI.SetCurScore(points);
    }
}
