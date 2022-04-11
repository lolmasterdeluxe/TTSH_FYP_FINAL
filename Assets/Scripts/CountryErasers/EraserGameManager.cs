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
    private int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 };

    [SerializeField] private MainEraser originalEraser;
    [SerializeField] private Material[] material;

    [SerializeField] private GameObject g_timerText;
    [SerializeField] private GameObject g_comboGroup;
    [SerializeField] private GameObject g_comboText;
    [SerializeField] private GameObject g_scoreText;
    [SerializeField] private GameObject g_gameTimeUp;
    [SerializeField] private Transform EraserContainer, Background, BackgroundBlur;

    [SerializeField] private List<MainEraser> erasersCount;
    [SerializeField] private List<MainEraser> erasersList;

    private MainEraser _firstRevealed;
    private MainEraser _secondRevealed;
    public bool m_gameStarted = false;
    public bool m_gameEnded = false;
    public bool startRevealing = false;
    private bool looping = false;
    [HideInInspector]
    public int ErasersMatched = 0;
    public bool canReveal
    { 
        get { /*if (_secondRevealed == null || )*/
            return _secondRevealed == null; 
        }
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
        ComboManager.Instance.SetComboExpiry(10f);
        ScoreManager.Instance.LoadNewGamemode(ScoreManager.Gamemode.COUNTRY_ERASERS);

        // Setup game logic
        TweenManager.Instance.AnimateFade(g_comboGroup.GetComponent<CanvasGroup>(), 0f, 0f);
        TweenManager.Instance.AnimateFade(g_gameTimeUp.GetComponent<CanvasGroup>(), 0f, 0f);

        TweenManager.Instance.AnimateSpriteFade(Background.GetComponent<SpriteRenderer>(), 0f, 0.5f);
        TweenManager.Instance.AnimateScale(Background, 1.2f, 1f);
        TweenManager.Instance.AnimateScale(BackgroundBlur, 1.2f, 1f);

        // Plays background music after countdown
        audioSources[0].Play();

        // Attach events
        TimerManager.Instance.e_TimerFinished.AddListener(OnGameEnd);
        ComboManager.Instance.e_comboAdded.AddListener(OnComboAdd);
        ComboManager.Instance.e_comboBreak.AddListener(OnComboBreak);
        ComboManager.Instance.e_comboExpired.AddListener(OnComboBreak);
    }

    private void Start()
    {
        MakeErasers();
    }
  
    private void Update()
    {
        
        if (!m_gameStarted)
            return;
        openTimer -= Time.deltaTime;
        StartCoroutine(OnLeaderboardLoad());
        UpdateUI();
        if (erasersCount.Count <= 2)
        {
            for (int i = 0; i < erasersCount.Count; i++)
            {
                erasersCount[i].OpenEraser();
                erasersCount.Remove(erasersCount[i]);
            }
        }
        if (erasersCount.Count == 0 && looping == false)
        {
            looping = true;
            Invoke("SwapBoard", 1f);
            Invoke("LoopGame", 2.5f);
            for (int i = 0; i < erasersList.Count;i++)
            {
                erasersList[i].Invoke("Cover", 5.5f);
            }
        }
    }

    void UpdateUI()
    {
        g_scoreText.GetComponent<TMP_Text>().text = ScoreManager.Instance.GetCurrentGameScore().ToString();
        g_timerText.GetComponent<TMP_Text>().text = TimerManager.Instance.GetFormattedRemainingTime();
        g_comboText.GetComponent<TMP_Text>().text = "Combo " + ComboManager.Instance.GetCurrentCombo() + "x";
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


    
    public void EraserRevealed(MainEraser eraser)
    {
        audioSources[1].Play();
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
        startRevealing = false;
        Debug.Log("Checking");
        if (_firstRevealed.id == _secondRevealed.id)
        {
            ErasersMatched++;
            yield return new WaitForSeconds(0.5f);
            erasersCount.Remove(_firstRevealed);
            erasersCount.Remove(_secondRevealed);
            audioSources[2].Play();
            startRevealing = true;
            ScoreManager.Instance.AddCurrentGameScore(1 * ComboManager.Instance.GetCurrentCombo());
            ComboManager.Instance.AddCombo(1);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            print("wrong");
            _firstRevealed.Cover();
            _secondRevealed.Cover();
            audioSources[3].Play();
            ComboManager.Instance.BreakCombo();
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
        audioSources[0].Stop();

        // Plays time's up audio
        //audioSources[1].Play();

        ScoreManager.Instance.EndCurrentGameScore();
    }
    private void LoopGame()
    {
        //yield return new WaitForSeconds(0.5f);
        numbers = ShuffleArray(numbers);
        Material[] materials = ShuffleMaterials(material);
        int iterator = 0;
        //looping = false;
        for (int j = 0; j < gridCol; j++)
        {
            for (int k = 0; k < gridRows; k++)
            {
                int index = k * gridCol + j;
                int id = numbers[index];
                
                //MainEraser eraser;
                //if (i == 0 && j == 0)
                //{
                //    eraser = originalEraser;
                //}
                //else
                //{
                //    eraser = Instantiate(originalEraser) as MainEraser;
                //}
                print(id);
                erasersCount.Add(erasersList[iterator]);
                erasersList[iterator].ChangeMaterial(id, materials[id]);
                iterator++;
                print(iterator);
            }
        }
        looping = false;
    }
    private void MakeErasers()
    {
        Vector3 startPos = originalEraser.transform.position;
        numbers = ShuffleArray(numbers);
        Material[] materials = ShuffleMaterials(material);
        for (int i = 0; i < gridCol; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MainEraser eraser;
                if (i == 0 && j == 0)
                {
                    eraser = originalEraser;
                }
                else
                {
                    eraser = Instantiate(originalEraser, EraserContainer) as MainEraser;
                }
                int index = j * gridCol + i;
                int id = numbers[index];
                eraser.ChangeMaterial(id, materials[id]);

                float posX = (offSetX * i) + startPos.x;
                float posY = (offSetY * j) + startPos.y;

                eraser.transform.position = new Vector3(posX, posY, startPos.z);
                erasersCount.Add(eraser);
                erasersList.Add(eraser);
            }
        }
    }

    private void SwapBoard()
    {
        EraserContainer.GetComponent<Animator>().SetTrigger("Swap");
        ScoreManager.Instance.AddCurrentGameScore(10);
    }

    private void OnComboAdd()
    {
        TweenManager.Instance.AnimateFade(g_comboGroup.GetComponent<CanvasGroup>(), 1f, 0.25f);
        TweenManager.Instance.AnimateEnlargeText(g_comboText.transform, 1, 0.25f);
    }

    public void OnComboBreak()
    {
        TweenManager.Instance.AnimateFade(g_comboGroup.GetComponent<CanvasGroup>(), 0f, 1f);
    }

    private void OnKillTween()
    {
        TweenManager.Instance.KillCanvasGroupTween(g_comboGroup.GetComponent<CanvasGroup>());
        TweenManager.Instance.KillTween(g_comboText);
    }

    private void OnDestroy()
    {
        if (this == _instance)
            _instance = null;
    }

}
