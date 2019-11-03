using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
public class Scene : MonoBehaviour
{
    public int cubesDestroyed = 0;
    public int  numAllowed = 3;
    private GameObject endScreen;
    private GameObject filter;
    private GameObject button;
    private GameObject post;
    private Player player;
    private bool toggled = false;
    [SerializeField]
    private Sprite paused;
    [SerializeField]
    private Sprite played;
    // Start is called before the first frame update
    void Start()
    {
        cubesDestroyed = 0;
        endScreen = GameObject.FindGameObjectWithTag("Finish");
        endScreen.SetActive(false);
        filter = GameObject.FindGameObjectWithTag("Filter");
        filter.SetActive(false);
        button = GameObject.FindGameObjectWithTag("Pause");
        button.SetActive(true);
        post = GameObject.FindGameObjectWithTag("Post");
        post.GetComponent<PostProcessVolume>().weight = 1f;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.paused = false;
        Time.timeScale = 1.0f;
    }       

    // Update is called once per frame
    void Update()
    {
        if (cubesDestroyed >= numAllowed)
        {
            endScreen.SetActive(true);
            filter.SetActive(true);
            button.SetActive(false);
            post.GetComponent<PostProcessVolume>().weight = 0.7f;
            Time.timeScale = 0;
        }
    }

    public void TogglePause()
    {
        Image pause = button.GetComponent<Button>().GetComponent<Image>();
        if (!toggled)
            pause.sprite = played;
        else
            pause.sprite = paused;
        ToggleTime();
        toggled = !toggled;
        player.paused = !player.paused;
    }

    public void ToggleTime()
    {
        if(Time.timeScale != 0)
            Time.timeScale = 0;
        else if (Time.timeScale == 0)
            Time.timeScale = 1;
    }

    public void StartGame()
    {
        LoadScene();
        Start();
    }
    public void LoadScene()
    {
        SceneManager.LoadScene("main");
    }
}

