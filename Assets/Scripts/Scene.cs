using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public int cubesDestroyed = 0;

    public int  numAllowed = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }       

    // Update is called once per frame
    void Update()
    {
        if (cubesDestroyed >= numAllowed)
            StartGame();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("main");
    }
}

