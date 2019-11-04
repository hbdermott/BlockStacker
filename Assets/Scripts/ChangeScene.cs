using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{
    Animator animator;
    public int sceneIndex;

    void Start()
    {
        animator = transform.GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(sceneIndex == 0)
                Change();
        }
    }
    public void Change()
    {
        StartCoroutine(LoadSceneAFterTransition());
    }
    private IEnumerator LoadSceneAFterTransition()
    {
        animator.SetBool("animateOut", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneIndex);
    }
}