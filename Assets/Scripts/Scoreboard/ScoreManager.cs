using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
using UnityGoogleDrive;

public class ScoreManager : MonoBehaviour
{
    public enum Gamemode
    { 
        DEFAULT,
        SPS,
        FIVESTONES,
        CHAPTEH,
        FLAPPY,
        COUNTRY_ERASERS,
        HANGMAN,
        TOTAL,
    }

    public class Score
    {
        public int m_userId;
        public int m_scoreId;
        public string m_username;
        public string m_gamemode;
        public int m_score; 
        public int m_hatId;
        public int m_faceId;
        public int m_colourId;
        public Score(int userId, string username, string gamemode, int score, int hatId, int faceId, int colourId)
        {
            m_userId = userId;
            m_username = username;
            m_gamemode = gamemode;
            m_score = score;
            m_hatId = hatId;
            m_faceId = faceId;
            m_colourId = colourId;
        }
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

    public List<Score> m_allScoreList = new List<Score>();
    private int m_maxUser = 1000;

    public string m_currentUsername;
    public int m_currentUserID;
    private int m_currentScore;
    public Gamemode m_currentGamemode;

    private const string url = "https://ttshnursesday.com";
    private const string api_allScore = "/scoreboard.php?all=";
    private const string api_addScore = "/scoreboard.php";

    private GoogleDriveFiles.DownloadRequest driveRequest;
    private string driveResult;
    private const string fileId = "1joby-qdIKkc0Phwkw5RGbktujGaiz3Ix";
    /*private const string UploadFilePath = "My Drive"*/

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
        // Get scoreboard.csv from Gdrive
        GetGdriveFile();
    }

    // (Currently unused)
    #region mySQL Database
    IEnumerator RequestAllScore(int count=1000)
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

            GameObject gameObject = GameObject.Find("Leaderboard");
            if (gameObject != null)
                gameObject.GetComponent<LeaderboardManager>().UpdateLeaderboard();
        }
    }

    IEnumerator SaveScoreData(Score score)
    {
        WWWForm form = new WWWForm();
        form.AddField("m_username", score.m_username);
        form.AddField("m_gamemode", score.m_gamemode);
        form.AddField("m_score", score.m_score);
        form.AddField("m_hatId", score.m_hatId);
        form.AddField("m_faceId", score.m_faceId);
        form.AddField("m_colourId", score.m_colourId);

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("m_username=" + score.m_username +
                                                  "&m_gamemode=" + score.m_gamemode +
                                                  "&m_score=" + score.m_score +
                                                  "&m_hatId=" + score.m_hatId +
                                                  "&m_faceId=" + score.m_faceId +
                                                  "&m_colourId=" + score.m_colourId));


        UnityWebRequest unityWebRequest = UnityWebRequest.Post(url + api_addScore, formData);
        unityWebRequest.chunkedTransfer = false;
        //unityWebRequest.SetRequestHeader("Content-Type", "multipart/form-data");
        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.isNetworkError)
        {
            Debug.Log("Error while sending web request");
        }
        else
        {
            Debug.Log("Response: " + unityWebRequest.downloadHandler.text);
        }
    }

    public void ConcludeGameScore()
    {
        List<Score> m_currentScoreList = m_allScoreList.Where(x => x.m_username == m_currentUsername).ToList();
        // Super awful way of doing, might lose data too need to test
        foreach (Score score in m_currentScoreList)
            StartCoroutine(SaveScoreData(score));
    }

    #endregion

    #region Google Drive Upload/Download
    private void GetGdriveFile()
    {
        driveRequest = GoogleDriveFiles.Download(fileId);
        driveRequest.Fields = new List<string> { };
        driveRequest.Send().OnDone += BuildScoreboardString;
    }

    private void BuildScoreboardString(UnityGoogleDrive.Data.File file)
    {
        driveResult = Encoding.UTF8.GetString(file.Content);
        Debug.Log(driveResult);
    }

    private void UploadNewScoreboard(string rawFileData)
    {
        // Uploading a file.
        var file = new UnityGoogleDrive.Data.File { Name = "score.csv", Content = Encoding.UTF8.GetBytes(rawFileData) };
        GoogleDriveFiles.Update(fileId, file).Send();
    }

    #endregion

    public void ResetUser()
    {
        m_currentUsername = "";
        m_currentScore = 0;
        m_currentUserID = m_maxUser;
        CustomizerManager.Instance.m_hatId = 0;
        CustomizerManager.Instance.m_colorId = 0;
        CustomizerManager.Instance.m_faceId = 0;
    }

    public void LoadWebScoreList()
    {
        StartCoroutine(RequestAllScore());
    }

    public int GetCurrentSavedScoreCount()
    {
        return m_allScoreList.Where(x => x.m_username == m_currentUsername).ToList().Count();
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

    public void UpdateCurrentUserTotalScore()
    {
        Score totalScore = m_allScoreList.Where(x => x.m_userId == m_currentUserID && x.m_gamemode == Gamemode.TOTAL.ToString()).FirstOrDefault();

        if (totalScore == null)
        {
            totalScore = new Score(GenerateNewUserId(), m_currentUsername, Gamemode.TOTAL.ToString(), 0, CustomizerManager.Instance.m_hatId, CustomizerManager.Instance.m_faceId, CustomizerManager.Instance.m_colorId);
            m_currentUserID = totalScore.m_userId;
            m_allScoreList.Add(totalScore);
        }

        List<int> scoreList = m_allScoreList.Where(x => x.m_userId == m_currentUserID && x.m_gamemode != Gamemode.TOTAL.ToString()).Select(x => x.m_score).ToList();
        totalScore.m_score = scoreList.Sum();
    }

    public void EndCurrentGameScore()
    {
        Score score = m_allScoreList.Where(x => x.m_userId == m_currentUserID && x.m_gamemode == m_currentGamemode.ToString()).FirstOrDefault();

        if (score == null)
        {
            score = new Score(m_currentUserID, m_currentUsername, m_currentGamemode.ToString(), m_currentScore, CustomizerManager.Instance.m_hatId, CustomizerManager.Instance.m_faceId, CustomizerManager.Instance.m_colorId);
            m_allScoreList.Add(score);
        }
        else
        {
            score.m_score = m_currentScore;
        }

        UpdateCurrentUserTotalScore();
    }

    #region Local CSV Saving

    private string GetFilePath()
    {
        // Always update current scoreboard.csv with latest drive data
#if UNITY_EDITOR
        SaveCSV(Application.dataPath + "/CSV/" + "score.csv", driveResult);
        return Application.dataPath + "/CSV/" + "score.csv";
#elif UNITY_ANDROID || UNITY_IPHONE
        SaveCSV(Application.persistentDataPath + "/score.csv", driveResult);
        return Application.persistentDataPath + "/score.csv";
#else
        SaveCSV(Application.dataPath + "/CSV/" + "score.csv", driveResult);
        return Application.dataPath +"/CSV/"+"score.csv";
#endif
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

            m_allScoreList.Add(new Score(int.Parse(scoreData[0]), scoreData[1], scoreData[2], int.Parse(scoreData[3]), int.Parse(scoreData[4]), int.Parse(scoreData[5]), int.Parse(scoreData[6])));

            Debug.Log(scoreData[0] + " " + scoreData[1] + " " + scoreData[2]);
        }

        streamReader.Close();
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

    public void EndSessionConcludeScore()
    {
        var stringBuilder = new StringBuilder("Id,Name,Mode,Score,HatId,FaceId,ColourId");
        foreach (Score score in m_allScoreList)
        {
            stringBuilder.Append('\n')
                .Append(score.m_userId.ToString()).Append(',')
                .Append(score.m_username).Append(',')
                .Append(score.m_gamemode).Append(',')
                .Append(score.m_score.ToString()).Append(',')
                .Append(score.m_hatId.ToString()).Append(',')
                .Append(score.m_faceId.ToString()).Append(',')
                .Append(score.m_colourId.ToString());
        }

        SaveCSV(GetFilePath(), stringBuilder.ToString());
        UploadNewScoreboard(stringBuilder.ToString());
    }

    private void CreateCSV(string filePath)
    {
        StreamWriter outStream = File.CreateText(filePath);
        outStream.Close();
    }

    private void SaveCSV(string filePath, string data)
    {
        StreamWriter outStream = File.CreateText(filePath);
        outStream.Write(data);
        outStream.Close();
    }

    public void DeleteCSV()
    {
#if UNITY_EDITOR
        File.Delete(Application.dataPath + "/CSV/" + "score.csv");
#elif UNITY_ANDROID || UNITY_IPHONE
        File.Delete(Application.persistentDataPath + "/score.csv");
#else
        File.Delete(Application.dataPath + "/CSV/" + "score.csv");
#endif
    }


    #endregion
}
