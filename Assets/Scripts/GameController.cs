using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Advertisements;
public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject cubePrefab;
    private UI UI;
    private GameObject[] cube;
    private Spawner Spawner;
    private string gameId = "3353258";

    private int cubesDestroyed = 0;
    private int numAllowed = 3;
    public int points = 0;
    public int height = 0;
    private float maxHeight = 0;
    private float cubeHeight = 0;
    UserData user;
    private bool end = false;

    void Start()
    {
        cubeHeight = cubePrefab.GetComponent<Renderer>().bounds.size.y;
        UI = GameObject.FindGameObjectWithTag("UI").GetComponent<UI>();
        Spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
        user = SaveSystem.LoadUser();
        if (user == null)
        {
            user = new UserData();
        }
       Advertisement.Initialize(gameId, false);
    }

    public void Restart()
    {
        SaveSystem.SaveUser(user);
        SceneManager.LoadScene("main");
        UI.UIMode(false);
        UI.ResetLives(true);
        end = false;
    }

    public void Continue()
    {
        UI.UIMode(false);
        cubesDestroyed = 0;
        UI.ResetLives(true);
        end = false;
    }


    void OnApplicationQuit()
    {
        SaveSystem.SaveUser(user);
    }

    public void DroppedCube() {
        cubesDestroyed++;
        if(cubesDestroyed <= 3)
            UI.LoseLife(cubesDestroyed);
    }

    void Update()
    {
        if (cubesDestroyed >= numAllowed && !end)
        {
                user.Update(points, 1);
                UI.SetScores(points, user.highScore);
                UI.UIMode(true);
                end = true;
                if(user.numGames >= 5 && user.numGames % 5 == 0)
                {
                    if (Advertisement.IsReady("video"))
                       Advertisement.Show("video");
                }
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
