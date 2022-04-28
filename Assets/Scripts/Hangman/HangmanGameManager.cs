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
    private TMP_Text scoreText, timerText, WordToSolveText;

    public bool m_gameStarted, m_gameEnded = false;

    [SerializeField]
    private GameObject g_gameTimeUp, leaderboard;

    [SerializeField]
    private GameObject WordContainer, WordFormat, LetterFormat, ThemeDisplay, BoxGuy, Keyboard;

    [SerializeField]
    private CanvasGroup NextWordPanel;

    [HideInInspector]
    public float WordsSolved;

    private bool WordCompleted = false;

    private GameObject LetterContainer;

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
        audioSources[0].Play();

        int score = 0;
        scoreText.text = score.ToString();
        RandomizeWord();
        ResetGame();
    }

    void Update()
    {
        if (!m_gameStarted)
            return;

        // Update UI for timer and score
        timerText.text = TimerManager.Instance.GetFormattedRemainingTime();
        scoreText.text = ScoreManager.Instance.GetCurrentGameScore().ToString();

        if (TimerManager.Instance.GetRemainingTime() <= 0)
            GameOver();

        // If word is solved, move on to next word
        if (CheckWordSolved())
            LoadNextWord();

        LetterColor();
    }

    private void RandomizeWord()
    {
        // Check if existing letter is active
        if (LetterContainer != null)
            Destroy(LetterContainer);

        // Add points if a word is completed by multiplying ten to the amt of letters in the word
        if (WordCompleted)
        {
            ScoreManager.Instance.AddCurrentGameScore(10 * LetterContainer.transform.childCount);
            WordCompleted = false;
        }

        // Randomize Word index
        int randomIndex = Random.Range(0, m_allWordList.Count);

        // Derive word from list using randomized index
        randomWord = m_allWordList.Where(x => x.m_index == randomIndex).FirstOrDefault();

        // Display the word theme
        ThemeDisplay.GetComponent<TextMeshProUGUI>().text = randomWord.m_theme;

        // Instantiate the appropriate container for the word to be solved
        LetterContainer = Instantiate(WordFormat, WordContainer.transform);
        LetterContainer.name = randomWord.m_word;
        LetterContainer.SetActive(true);
        
        // Instantiate each letter in the word as a gameobject by substringing the letters from the word
        for (int i = 0; i < randomWord.m_word.Length; ++i)
        {
            GameObject Letter = Instantiate(LetterFormat, LetterContainer.transform);
            Letter.SetActive(true);
            Letter.transform.GetChild(0).gameObject.SetActive(false);
            Letter.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = randomWord.m_word.Substring(i, 1);
            // Check if letter is a space, if yes then solve it by default and create empty space
            if (randomWord.m_word.Substring(i, 1) == " ")
            {
                Letter.GetComponent<TextMeshProUGUI>().text = " ";
                Letter.transform.GetChild(0).gameObject.SetActive(true);
            }
            // Set letter gameobject to name of word for easier readability
            Letter.name = randomWord.m_word.Substring(i, 1) + "(" + i.ToString() + ")";
        }
    }

    public void ReturnLetter(string letter)
    {
        // Boolean to check if the letter picked is correct (To prevent boxman from building)
        bool correctLetter = false;

        // Compare the letter picked to letters in the word
        for (int i = 0; i < randomWord.m_word.Length; ++i)
        {
            GameObject LetterToSolveGO = GameObject.Find(randomWord.m_word.Substring(i, 1) + "(" + i.ToString() + ")").transform.GetChild(0).gameObject;
            GameObject LetterGO = GameObject.Find(letter);
            Button LetterButton = LetterGO.GetComponent<Button>();

            // If the letter picked matches a letter in the word
            if (letter == randomWord.m_word.Substring(i, 1).ToUpper() && !LetterToSolveGO.activeSelf)
            {
                // Set the GO to active and disable the picked letter in the keyboard
                LetterButton.interactable = false;
                LetterToSolveGO.SetActive(true);
                LetterGO.GetComponent<ButtonAnimation>().isEnabled = false;
                ScoreManager.Instance.AddCurrentGameScore(1);
                correctLetter = true;
            }
            // If not correct, build up the boxman
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

    private void LetterColor()
    {
        // Create boolean to check validate each letter in the word
        bool[] correctColor = new bool[Keyboard.transform.childCount];
        for (int j = 0; j < Keyboard.transform.childCount; ++j)
        {
            for (int i = 0; i < randomWord.m_word.Length; ++i)
            {
                GameObject LetterGO = Keyboard.transform.GetChild(j).gameObject;
                Button LetterButton = LetterGO.GetComponent<Button>();
                ColorBlock ButtonColor = LetterButton.colors;

                // If the letter picked matches a letter in the word, color when letter pressed is green
                if (Keyboard.transform.GetChild(j).name == randomWord.m_word.Substring(i, 1).ToUpper())
                {
                    ButtonColor.pressedColor = Color.green;
                    LetterButton.colors = ButtonColor;
                    correctColor[j] = true;
                }
                // If not correct, color when letter pressed is red
                else if (i == (randomWord.m_word.Length - 1) && !correctColor[j])
                {
                    ButtonColor.pressedColor = Color.red;
                    LetterButton.colors = ButtonColor;
                }
            }
        }
    }

    private bool CheckWordSolved()
    {
        for (int i = 1; i < LetterContainer.transform.childCount; i++)
        {
            if (!LetterContainer.transform.GetChild(i).transform.GetChild(0).gameObject.activeInHierarchy)
            {
                return false;
            }
        }
        return true;
    }

    private void LoadNextWord()
    {
        TweenManager.Instance.AnimateFade(NextWordPanel, 1, 1);
        Invoke("RandomizeWord", 1);
        Invoke("ResetGame", 3);
    }

    private void ResetGame()
    {
        // Reset keyboard and boxman
        TweenManager.Instance.AnimateFade(NextWordPanel, 0, 1);
        WordCompleted = true;
        for (int i = 0; i < Keyboard.transform.childCount; i++)
        {
            if (!Keyboard.transform.GetChild(i).GetComponent<Button>().interactable)
            {
                Keyboard.transform.GetChild(i).GetComponent<Button>().interactable = true;
                Keyboard.transform.GetChild(i).GetComponent<ButtonAnimation>().isEnabled = true;
            }
        }
        for (int k = 1; k < 7; ++k)
        {
            if (BoxGuy.transform.GetChild(k).gameObject.activeSelf)
            {
                BoxGuy.transform.GetChild(k).gameObject.SetActive(false);
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
        audioSources[0].Stop();

        WordToSolveText.text = "The word was " + randomWord.m_word;

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
