using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HangmanGameManager : MonoBehaviour
{
    private static HangmanGameManager _instance;

    public static HangmanGameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject instance = new GameObject("GameManager");
                instance.AddComponent<HangmanGameManager>();
            }

            return _instance;
        }

    }

    public class Word
    {
        public int m_index;
        public string m_theme;
        public string m_word;
        public Word(int index, string theme, string word)
        {
            m_index = index;
            m_theme = theme;
            m_word = word;
        }
    }

    [SerializeField]
    private TMP_Text scoreText, timerText;

    public bool m_gameStarted, m_gameEnded = false;

    [SerializeField]
    private GameObject g_gameTimeUp, leaderboard;

    [SerializeField]
    private GameObject WordContainer, WordFormat, LetterFormat, ThemeDisplay, BoxGuy;

    [HideInInspector]
    public float WordsSolved;

    public AudioSource[] audioSources;

    public List<Word> m_allWordList = new List<Word>();

    private Word randomWord;

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
        LoadWordList();
    }

    public void StartGame(float time)
    {
        m_gameStarted = true;
        // Setup managers
        TimerManager.Instance.StartCountdown(time);
        ScoreManager.Instance.LoadNewGamemode(ScoreManager.Gamemode.HANGMAN);
        TweenManager.Instance.AnimateFade(g_gameTimeUp.GetComponent<CanvasGroup>(), 0f, 0f);

        // Plays background music after countdown
        //audioSources[0].Play();

        int score = 0;
        scoreText.text = score.ToString();
        RandomizeWord();
    }

    void Update()
    {
        if (!m_gameStarted)
            return;

        timerText.text = TimerManager.Instance.GetFormattedRemainingTime();
        scoreText.text = ScoreManager.Instance.GetCurrentGameScore().ToString();
    }

    private void RandomizeWord()
    {
        int randomIndex = Random.Range(0, m_allWordList.Count);
        randomWord = m_allWordList.Where(x => x.m_index == randomIndex).FirstOrDefault();
        ThemeDisplay.GetComponent<TextMeshProUGUI>().text = randomWord.m_theme;
        GameObject LetterContainer = Instantiate(WordFormat, WordContainer.transform);
        LetterContainer.name = randomWord.m_word;
        LetterContainer.SetActive(true);
        for (int i = 0; i < randomWord.m_word.Length; ++i)
        {
            GameObject Letter = Instantiate(LetterFormat, LetterContainer.transform);
            Letter.SetActive(true);
            Letter.transform.GetChild(0).gameObject.SetActive(false);
            Letter.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = randomWord.m_word.Substring(i, 1);
            if (randomWord.m_word.Substring(i, 1) == " ")
                Letter.GetComponent<TextMeshProUGUI>().text = " ";
            Letter.name = randomWord.m_word.Substring(i, 1) + "(" + i.ToString() + ")";
        }
    }

    public void ReturnLetter(string letter)
    {
        bool correctLetter = false;
        for (int i = 0; i < randomWord.m_word.Length; ++i)
        {
            GameObject LetterToSolveGO = GameObject.Find(randomWord.m_word.Substring(i, 1) + "(" + i.ToString() + ")").transform.GetChild(0).gameObject;
            if (letter == randomWord.m_word.Substring(i, 1).ToUpper() && !LetterToSolveGO.activeSelf)
            {
                LetterToSolveGO.SetActive(true);
                GameObject LetterGO = GameObject.Find(letter);
                LetterGO.GetComponent<Button>().interactable = false;
                LetterGO.GetComponent<ButtonAnimation>().isEnabled = false;
                ScoreManager.Instance.AddCurrentGameScore(1);
                correctLetter = true;
            }
            else if (i == (randomWord.m_word.Length - 1) && !correctLetter)
            {
                for (int k = 0; k < 7; ++k)
                {
                    if (!BoxGuy.transform.GetChild(k).gameObject.activeSelf)
                    {
                        BoxGuy.transform.GetChild(k).gameObject.SetActive(true);
                        break;
                    }
                }
                if (BoxGuy.transform.GetChild(6).gameObject.activeSelf)
                    GameOver();
            }
        }
    }

    private string GetFilePath()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/CSV/" + "listofwords.csv";
#elif UNITY_ANDROID || UNITY_IPHONE
        return Application.persistentDataPath + "/listofwords.csv";
#else
        return Application.dataPath +"/CSV/"+"listofwords.csv";
#endif
    }

    private void LoadWordList()
    {
        int index = 0;
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

            var wordData = data.Split(',');
            if (wordData[0] == "Category" && wordData[1] == "Word")
                continue;

            m_allWordList.Add(new Word(index, wordData[0], wordData[1]));
            index++;
        }

        streamReader.Close();
    }

    public void GameOver()
    {
        m_gameEnded = true;
        TweenManager.Instance.AnimateFade(g_gameTimeUp.GetComponent<CanvasGroup>(), 1f, 1f);

        // Stops playing bgm audio
        // audioSources[0].Stop();

        // Plays time's up audio
        // audioSources[1].Play();

        ScoreManager.Instance.EndCurrentGameScore();
        StartCoroutine(OnLeaderboardLoad());
    }
    public void increaseScore()
    {
        ScoreManager.Instance.AddCurrentGameScore(1);
        WordsSolved++;
    }

    public IEnumerator OnLeaderboardLoad()
    {
        if (m_gameEnded)
        {
            Debug.Log("Currently in hiatus");
            yield return new WaitForSeconds(3);
            Debug.Log("Currently in hiatus 2");
            //AudioObject.SetActive(false);
            leaderboard.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        if (this == _instance)
            _instance = null;
    }
}
