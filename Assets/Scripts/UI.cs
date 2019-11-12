using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
public class UI : MonoBehaviour
{
    private GameObject InGameUI;
    private GameObject GameOver;
    private TMPro.TextMeshProUGUI Score;
    private TMPro.TextMeshProUGUI HighScore;
    private GameObject Filter;
    private GameObject Transition;
    private GameObject NextObj;
    private GameObject Pause;
    private GameObject Life;
    private GameObject[] lives;
    private RewardedAdsButton Con;
    private Image PauseIMG;
    private PostProcessVolume Post;
    private Player Player;
    private TMPro.TextMeshPro PlayerTM;
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
        Score = GameOver.gameObject.transform.GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();
        HighScore = GameOver.gameObject.transform.GetChild(2).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();
        Pause = InGameUI.transform.GetChild(0).gameObject;
        PauseIMG = Pause.GetComponent<Button>().GetComponent<Image>();
        NextObj = InGameUI.transform.GetChild(1).gameObject;
        Life = InGameUI.transform.GetChild(2).gameObject;
        lives = new GameObject[3];
        for(int i = 0; i < 3; i++)
        {
            lives[i] = Life.transform.GetChild(i).gameObject;
        }
        Con = GameOver.gameObject.transform.GetChild(3).GetComponent<RewardedAdsButton>();
        Post = GameObject.FindGameObjectWithTag("Post").GetComponent<PostProcessVolume>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        PlayerTM = Player.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>();
        UIMode();
    }
    public void UIMode(bool end = false)
    {
        GameOver.SetActive(end);
        Filter.SetActive(end);
        InGameUI.SetActive(!end);
        Player.paused = end;
        if (end) {
            Con.Init();
            Time.timeScale = 0;
            Post.weight = 0.5f;
        }
        else {
            Time.timeScale = 1;
            Post.weight = 1.0f;
        }
    }

    public void LoseLife(int counter)
    {
        lives[counter - 1].SetActive(false);
    }

    public void ResetLives(bool reset)
    {
        for(int i = 0; i < 3; i++)
        {
            lives[i].SetActive(true);
        }
    }

    public void SetScores(int cur, int high)
    {
        Score.text = cur.ToString();
        HighScore.text = high.ToString();
    }

    public void SetCurScore(int score)
    {
        PlayerTM.text = score.ToString();
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

