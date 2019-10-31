using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public int droppedCubes = 0;

    private int dropsAllowed = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(droppedCubes >= dropsAllowed)
        {
            SceneManager.LoadScene("main");
        }
    }
}
