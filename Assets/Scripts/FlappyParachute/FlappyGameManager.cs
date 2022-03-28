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

    public FlappyPlayer player;
    
    public TMP_Text scoretext;

    private int score;

    private bool m_gameStarted;
    private bool m_gameEnded = false;

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
        //Pause();
    }

    void Update()
    {
        if (!m_gameStarted)
            return;

       
        //UpdateUI();
    }

    public void Play(float time)
    {
        m_gameStarted = true;
        ScoreManager.Instance.LoadNewGamemode(ScoreManager.Gamemode.FLAPPY);
        score = 0;
        scoretext.text = score.ToString();
        Time.timeScale = 1f;
        player.enabled = true;
        TimerManager.Instance.StartCountdown(time);


        Pipes[] pipes = FindObjectsOfType<Pipes>();

        for (int i=0; i <pipes.Length;i++)
        {
            Destroy(pipes[i].gameObject);
        }

        TimerManager.Instance.e_TimerFinished.AddListener(GameOver);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void GameOver()
    {
        Debug.Log("Player is dead, game over ");
        m_gameEnded = true;
        StartCoroutine(OnLeaderboardLoad());

        Pause();
    }
    public void inscreaseScore()
    {
        score++;
        scoretext.text = score.ToString();
    }

    public IEnumerator OnLeaderboardLoad()
    {
        if (m_gameEnded)
        {
            yield return new WaitForSeconds(3);
            //AudioObject.SetActive(false);
            Resources.FindObjectsOfTypeAll<LeaderboardManager>()[0].gameObject.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        if (this == _instance)
            _instance = null;
    }
}
