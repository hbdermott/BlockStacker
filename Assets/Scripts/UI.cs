using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
public class UI : MonoBehaviour
{
    private GameObject InGameUI;
    private GameObject GameOver;
    private GameObject Filter;
    private GameObject Transition;
    private GameObject NextObj;
    private GameObject Pause;
    private Image PauseIMG;
    private PostProcessVolume Post;
    private Player Player;
    [SerializeField]
    private Sprite Paused;
    [SerializeField]
    private Sprite Played;

    private bool toggled = false;


    void Start()
    {
        InGameUI = gameObject.transform.GetChild(0).gameObject;
        GameOver = gameObject.transform.GetChild(1).gameObject;
        Filter = gameObject.transform.GetChild(2).gameObject;
        Transition = gameObject.transform.GetChild(3).gameObject;
        Pause = InGameUI.transform.GetChild(0).gameObject;
        PauseIMG = Pause.GetComponent<Button>().GetComponent<Image>();
        NextObj = InGameUI.transform.GetChild(1).gameObject;
        Post = GameObject.FindGameObjectWithTag("Post").GetComponent<PostProcessVolume>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        UIMode();
    }       
    public void UIMode(bool end = false)
    {
        GameOver.SetActive(end);
        Filter.SetActive(end);
        Pause.SetActive(!end);
        NextObj.SetActive(!end);
        Player.paused = end;
        if (end) { 
            Time.timeScale = 0;
            Post.weight = 0.7f;
        }
        else {
            Time.timeScale = 1;
            Post.weight = 1.0f;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("main");
        Start();
    }

    public void TogglePause()
    {
        if (!toggled)
        {
            Time.timeScale = 0;
            PauseIMG.sprite = Played;
        }
        else
        {
            PauseIMG.sprite = Paused;
            Time.timeScale = 1;
        }
        toggled = !toggled;
        Player.paused = !Player.paused;
    }

    public void SetNext(GameObject obj)
    {
       GameObject NextCube = NextObj.transform.GetChild(0).gameObject;
       GameObject NextBomb = NextObj.transform.GetChild(1).gameObject;

        if (obj.tag == "Bomb")
        {
            NextCube.SetActive(false);
            NextBomb.SetActive(true);
        }

        else
        {
            NextBomb.SetActive(false);
            NextCube.SetActive(true);
            NextCube.GetComponent<MeshRenderer>().material = obj.GetComponent<MeshRenderer>().material;
            NextCube.GetComponent<Renderer>().material.color = obj.GetComponent<Renderer>().material.color;
        }
    }

}

