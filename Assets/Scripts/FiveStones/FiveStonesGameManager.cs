using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FiveStonesGameManager : MonoBehaviour
{
    private static FiveStonesGameManager _instance;
    public static FiveStonesGameManager Instance { get { return _instance; } }

    public enum Objective
    { 
        DEFAULT,
        CATCH_RED_STONES,
        CATCH_YELLOW_STONES,
        CATCH_BLUE_STONES,
        CATCH_ANY_STONES,
        TOTAL,
    }

    public GameObject g_scoreText;
    public GameObject g_comboText;
    public GameObject g_comboExpiryBar;
    public GameObject g_comboExpiryBarTimerText;
    public GameObject g_timerText;
    public GameObject g_objectiveText;
    public Objective m_currentObjective;

    // using this temporary, will make a score manager later on
    public int m_score;
    public int baseScore = 1;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartGame(60, 1.1f);
    }

    void StartGame(float time, float difficultyMultiplier)
    {
        TimerManager.Instance.StartCountdown(time);
        ComboManager.Instance.SetComboExpiry(3f);
        StartCoroutine(GetComponent<StoneSpawner>().SpawnStoneLoop());
        RandomizeObjective();
        StartCoroutine(ObjectiveCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    public IEnumerator ObjectiveCoroutine()
    {
        while (true)
        {
            RandomizeObjective();
            yield return new WaitForSeconds(3);
        }
    }

    public void RandomizeObjective()
    {
        m_currentObjective = (Objective)Random.Range(1, (int)Objective.TOTAL);

        switch (m_currentObjective)
        {
            case Objective.CATCH_RED_STONES:
                g_objectiveText.GetComponent<TMP_Text>().text = "Catch <color=red>red</color> stones!";
                break;
            case Objective.CATCH_YELLOW_STONES:
                g_objectiveText.GetComponent<TMP_Text>().text = "Catch <color=yellow>yellow</color> stones!";
                break;
            case Objective.CATCH_BLUE_STONES:
                g_objectiveText.GetComponent<TMP_Text>().text = "Catch <color=blue>blue</color> stones!";
                break;
            case Objective.CATCH_ANY_STONES:
                g_objectiveText.GetComponent<TMP_Text>().text = "Catch <color=grey>any</color> stones!";
                break;
        }
    }

    void UpdateUI()
    {
        g_scoreText.GetComponent<TMP_Text>().text = "Score: " + m_score.ToString();
        g_timerText.GetComponent<TMP_Text>().text = TimerManager.Instance.GetFormattedRemainingTime();
        g_comboText.GetComponent<TMP_Text>().text = "Combo: " + ComboManager.Instance.GetCurrentCombo() + "x";
        g_comboExpiryBarTimerText.GetComponent<TMP_Text>().text = ComboManager.Instance.GetComboExpiryTimerFormatted();
        g_comboExpiryBar.GetComponent<Slider>().value = ComboManager.Instance.GetComboExpiryTimer() / ComboManager.Instance.GetComboExpiryTimerDefault();

        // Replace this with animation for pop up combo box here or something
        if (ComboManager.Instance.GetCurrentCombo() > 0)
        {
            g_comboText.SetActive(true);
        }
        else
        {
            g_comboText.SetActive(false);
        }
    }
    public void OnStoneCaught(GameObject gameObject)
    {
        if (gameObject.GetComponent<Stone>().type == m_currentObjective || m_currentObjective == Objective.CATCH_ANY_STONES)
        {
            ComboManager.Instance.AddCombo();
            m_score += (baseScore * ComboManager.Instance.GetCurrentCombo());
        }
        else
        {
            m_score += baseScore;
            ComboManager.Instance.BreakCombo();
        }
    }

    public static Objective GetRandomColouredObjective()
    {
        return (Objective)Random.Range(1, (int)Objective.TOTAL - 1);
    }

    private void OnDestroy()
    {
        if (this == _instance)
            _instance = null;
    }
}
