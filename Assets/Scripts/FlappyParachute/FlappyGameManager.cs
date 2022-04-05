using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlappyGameManager : MonoBehaviour
{
    private static FlappyGameManager _instance;

    public static FlappyGameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject instance = new GameObject("GameManager");
                instance.AddComponent<FlappyGameManager>();
            }

            return _instance;
        }

    }

    [SerializeField]
    private FlappyPlayer player;

    public TMP_Text scoretext;

    public bool m_gameStarted, m_gameEnded = false;

    public float SpeedMultiplier, SpawnMultiplier;

    [SerializeField] 
    private GameObject g_gameTimeUp, leaderboard;

    public AudioSource[] audioSources;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
            
        Application.targetFrameRate = 60;
    }

    public void StartGame()
    {
        m_gameStarted = true;
        // Setup managers
        ScoreManager.Instance.LoadNewGamemode(ScoreManager.Gamemode.FLAPPY);
        TweenManager.Instance.AnimateFade(g_gameTimeUp.GetComponent<CanvasGroup>(), 0f, 0f);
        player.gameObject.GetComponent<Animator>().SetBool("IsFlying", true);

        // Plays background music after countdown
        audioSources[0].Play();

        int score = 0;
        scoretext.text = score.ToString();
        player.enabled = true;

        MoveObstacle[] obstacles = FindObjectsOfType<MoveObstacle>();

        for (int i = 0; i < obstacles.Length; i++)
        {
            Destroy(obstacles[i].gameObject);
        }

    }

    void Update()
    {
        if (!m_gameStarted)
            return;

        scoretext.text = ScoreManager.Instance.GetCurrentGameScore().ToString();
        ScaleDifficulty();

/*        if (player.transform.position.y > 4.5f)
            scoretext.transform.parent.GetComponent<CanvasGroup>().alpha = 0.5f;
        else
            scoretext.transform.parent.GetComponent<CanvasGroup>().alpha = 1f;*/
    }

    public void GameOver()
    {
        Debug.Log("Player is dead, game over ");
        m_gameEnded = true;
        TweenManager.Instance.AnimateFade(g_gameTimeUp.GetComponent<CanvasGroup>(), 1f, 1f);
        player.gameObject.GetComponent<Animator>().SetBool("IsFlying", false);


        // Stops playing bgm audio
        audioSources[0].Stop();

        // Plays time's up audio
        audioSources[1].Play();

        ScoreManager.Instance.EndCurrentGameScore();
        StartCoroutine(OnLeaderboardLoad());
    }
    public void increaseScore()
    {
        ScoreManager.Instance.AddCurrentGameScore(1);
    }

    public void balloonScore()
    {
        ScoreManager.Instance.AddCurrentGameScore(2);
    }

    public IEnumerator OnLeaderboardLoad()
    {
        if (m_gameEnded)
        {
            Debug.Log("Currently in a hiatus");
            yield return new WaitForSeconds(3);
            Debug.Log("Currently in a hiatus 2");
            //AudioObject.SetActive(false);
            leaderboard.SetActive(true);
        }
    }

    private void ScaleDifficulty()
    {
        if (ScoreManager.Instance.GetCurrentGameScore() > 0 && ScoreManager.Instance.GetCurrentGameScore() % 10 == 0)
            SpeedMultiplier = (float)ScoreManager.Instance.GetCurrentGameScore() / 20;
        if (SpeedMultiplier > 4)
            SpeedMultiplier = 4;
        if (SpeedMultiplier < 1)
            SpawnMultiplier = 1;
        else
            SpawnMultiplier = SpeedMultiplier;
    }

    private void OnDestroy()
    {
        if (this == _instance)
            _instance = null;
    }
}
