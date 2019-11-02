using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public int cubesDestroyed = 0;

    public int  numAllowed = 3;
    private GameObject endScreen;
    private GameObject filter;
    // Start is called before the first frame update
    void Start()
    {
        endScreen = GameObject.FindGameObjectWithTag("Finish");
        endScreen.SetActive(false);
        filter = GameObject.FindGameObjectWithTag("Filter");
        filter.SetActive(false);
    }       

    // Update is called once per frame
    void Update()
    {
        if (cubesDestroyed >= numAllowed)
        {
            endScreen.SetActive(true);
            filter.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("main");
    }
}

