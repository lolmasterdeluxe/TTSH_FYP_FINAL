using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;
using UnityEditor;
using UnityEngine;
using System;
using System.Reflection;
using Newtonsoft.Json;
using UnityEngine.Networking;

public class ScoreManager : MonoBehaviour
{
    public enum Gamemode
    { 
        DEFAULT,
        SPS,
        FIVESTONES,
        CHAPTEH,
        TOTAL,
    }

    public class Score
    {
        public int m_userId;
        public string m_username;
        public string m_gamemode;
        public int m_score;
        public int m_hatId;
        public int m_faceId;
        public int m_colourId;

        public Score(int userId, string username, string gamemode, int score)
        {
            m_userId = userId;
            m_username = username;
            m_gamemode = gamemode;
            m_score = score;
        }
    }

    private string GetFilePath()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/CSV/" + "score.csv"; 
        #elif UNITY_ANDROID
        return Application.persistentDataPath+"Saved_data.csv";
        #elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+"Saved_data.csv";
        #else
        return Application.dataPath +"/"+"Saved_data.csv";
        #endif
    }

    private static ScoreManager _instance;
    public static ScoreManager Instance 
    {
        get
        {
            if (_instance == null)
            {
                GameObject instance = new GameObject("ScoreManager");
                ScoreManager scoreManager = instance.AddComponent<ScoreManager>();
            }

            return _instance;
        }
    }

    private List<Score> m_allScoreList = new List<Score>();
    private List<Score> m_currentScoreList = new List<Score>();
    private int m_maxUser = 1000;

    private int m_currentUserId;
    private string m_currentUsername;
    private int m_totalScore;

    private Gamemode m_currentGamemode;
    private int m_currentScore;

    private const string url = "https://ttsh-project.000webhostapp.com";
    private const string api_allScore = "/scoreboard.php?all=";
    private const string api_addScore = "/scoreboard.php";

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreateNewUser(string username)
    {
        // Throw prompt for username already exists
        if (DoesUserExist(username))
            return;

        int userId = GenerateNewUserId();

        // Throw prompt for max users
        if (userId == -1)
            return;

        m_currentUserId = userId;
        m_currentUsername = username;
    }

    IEnumerator RequestAllScore(int count=100)
    {
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(url + api_allScore + count);
        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.isNetworkError)
        {
            Debug.Log("Error while sending web request");
        }
        else
        {
            Debug.Log("Received: " + unityWebRequest.downloadHandler.text);
            m_allScoreList = JsonConvert.DeserializeObject<List<Score>>(unityWebRequest.downloadHandler.text);
        }
    }

    public void LoadWebScoreList()
    {
        StartCoroutine(RequestAllScore());
    }

    public void LoadAllScoreList()
    {
        m_allScoreList.Clear();

        StreamReader streamReader = new StreamReader(GetFilePath());

        if (streamReader == null)
        {
            Debug.Log("Score list file not found");
            streamReader.Close();
            return;
        }

        while (true)
        {
            string data = streamReader.ReadLine();

            if (data == null)
                break;

            var scoreData = data.Split(',');

            // ass hardcode change it!!
            if (!int.TryParse(scoreData[0], out int i))
                continue;

            m_allScoreList.Add(new Score(int.Parse(scoreData[0]), scoreData[1], scoreData[2], int.Parse(scoreData[3])));

            Debug.Log(scoreData[0] + scoreData[1] + scoreData[2] + scoreData[3]);
        }

        streamReader.Close();
    }

    public void LoadNewGamemode(Gamemode gamemode)
    {
        m_currentGamemode = gamemode;
        m_currentScore = 0;
    }

    public int GetCurrentGameScore()
    {
        return m_currentScore;
    }

    public void AddCurrentGameScore(int score)
    {
        m_currentScore += score;
    }

    public void SetCurrentGameScore(int score)
    {
        m_currentScore = score;
    }

    public void ReduceCurrentGameScore(int score)
    {
        m_currentScore -= score;
    }

    public bool DoesUserExist(string username)
    {
        Score score = m_allScoreList.Where(x => x.m_username == username).FirstOrDefault();

        if (score != null)
            return true;
        else
            return false;
    }

    public int GenerateNewUserId()
    {
        for (int i = 0; i < m_maxUser; i++)
        {
            Score score = m_allScoreList.Where(x => x.m_userId == i).FirstOrDefault();

            if (score == null)
                return i;
        }

        return -1;
    }

    public void EndCurrentGameScore()
    {
        Score score = new Score(m_currentUserId, m_currentUsername, m_currentGamemode.ToString(), m_currentScore);
        m_currentScoreList.Add(score);
    }

    public void EndSessionConcludeScore()
    {
        m_allScoreList.AddRange(m_currentScoreList);

        var stringBuilder = new StringBuilder("Id,Name,Mode,Score");
        foreach (Score score in m_allScoreList)
        {
            stringBuilder.Append('\n')
                .Append(score.m_userId.ToString()).Append(',')
                .Append(score.m_username).Append(',')
                .Append(score.m_gamemode).Append(',')
                .Append(score.m_score.ToString());
        }

        SaveCSV(GetFilePath(), stringBuilder.ToString());
    }

    public void SaveCSV(string filePath, string data)
    {
        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(data);
        outStream.Close();
    }
}
