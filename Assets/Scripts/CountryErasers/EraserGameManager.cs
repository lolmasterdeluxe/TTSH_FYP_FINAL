using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EraserGameManager : MonoBehaviour
{
    public int gridRows = 2;
    public int gridCol = 2;
    public const float offSetX = 3.5f;
    public const float offSetY = 3f;
    public float openTimer = 3f;

    [SerializeField] private MainEraser originalEraser;
    [SerializeField] private Material[] material;

    [SerializeField] private GameObject g_timerText;
    [SerializeField] private GameObject g_comboGroup;
    [SerializeField] private GameObject g_scoreText;
    [SerializeField] private GameObject g_gameTimeUp;

    public bool m_gameStarted = false;
    public bool m_gameEnded = false;
    public bool startRevealing = false;
    public bool canReveal
    { 
        get { return _secondRevealed == null; }
    }

    public AudioSource[] audioSources;

    private static EraserGameManager _instance;

    public static EraserGameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject instance = new GameObject("GameManager");
                instance.AddComponent<EraserGameManager>();
            }

            return _instance;
        }
    }

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
    }
    public void StartGame(float time)
    {
        m_gameStarted = true;
        // Setup managers
        TimerManager.Instance.StartCountdown(time);
        ComboManager.Instance.SetComboExpiry(8f);
        ScoreManager.Instance.LoadNewGamemode(ScoreManager.Gamemode.COUNTRY_ERASERS);

        // Setup game logic
        TweenManager.Instance.AnimateFade(g_comboGroup.GetComponent<CanvasGroup>(), 0f, 0f);
        TweenManager.Instance.AnimateFade(g_gameTimeUp.GetComponent<CanvasGroup>(), 0f, 0f);

        // Plays background music after countdown
        //audioSources[0].Play();

        // Attach events
        TimerManager.Instance.e_TimerFinished.AddListener(OnGameEnd);
    }

    private void Start()
    {
        Vector3 startPos = originalEraser.transform.position;
        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 ,4,4,5,5,6,6};
        numbers = ShuffleArray(numbers);
        Material[] materials = ShuffleMaterials(material);
        for (int i  = 0; i < gridCol; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MainEraser eraser;
                if (i == 0 && j == 0)
                {
                    eraser = originalEraser;

                }
                else
                    eraser = Instantiate(originalEraser) as MainEraser;
                int index = j * gridCol + i;
                int id = numbers[index];
                eraser.ChangeMaterial(id, materials[id]);

                float posX = (offSetX * i) + startPos.x;    
                float posY = (offSetY * j) + startPos.y;

                eraser.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }
    private void Update()
    {
        if (!m_gameStarted)
            return;
        openTimer -= Time.deltaTime;
        StartCoroutine(OnLeaderboardLoad());
        UpdateUI();
    }

    void UpdateUI()
    {
        g_scoreText.GetComponent<TMP_Text>().text = ScoreManager.Instance.GetCurrentGameScore().ToString();
        g_timerText.GetComponent<TMP_Text>().text = TimerManager.Instance.GetFormattedRemainingTime();
    }

    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for(int i = 0;i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }
    private Material[] ShuffleMaterials(Material[] materials)
    {

        Material[] newMatArray = materials.Clone() as Material[];
        for (int i = 0; i < newMatArray.Length; i++)
        {
            Material tmp = newMatArray[i];
            int r = Random.Range(i, newMatArray.Length);
            newMatArray[i] = newMatArray[r];
            newMatArray[r] = tmp;
        }
        return newMatArray;
    }
    private MainEraser _firstRevealed;
    private MainEraser _secondRevealed;

    
    public void EraserRevealed(MainEraser eraser)
    {
        if (_firstRevealed == null)
        {
            _firstRevealed = eraser;
            print("Reavealed 1");
        }
        else if (_firstRevealed != null && _secondRevealed == null)
        {
            _secondRevealed = eraser;
            if (_secondRevealed == _firstRevealed)
            {
                _secondRevealed = null;
            }
            else
            {
                print("revealed 2");
                StartCoroutine("CheckMatch");
            }
        }
        
    }

    private IEnumerator CheckMatch()
    {
        Debug.Log("Checking");
        if (_firstRevealed.id == _secondRevealed.id)
        {
            ScoreManager.Instance.AddCurrentGameScore(1);
        }
        else
        {
            yield return new WaitForSeconds(2f);
            print("wrong");
            _firstRevealed.Cover();
            _secondRevealed.Cover();
        }
        _firstRevealed = null;
        _secondRevealed = null;
        print("Done");
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

    public void OnGameEnd()
    {
        m_gameEnded = true;
        TweenManager.Instance.AnimateFade(g_gameTimeUp.GetComponent<CanvasGroup>(), 1f, 0.5f);

        // Stops playing bgm audio
        //audioSources[0].Stop();

        // Plays time's up audio
        //audioSources[1].Play();

        ScoreManager.Instance.EndCurrentGameScore();
    }


    private void OnDestroy()
    {
        if (this == _instance)
            _instance = null;
    }
}
