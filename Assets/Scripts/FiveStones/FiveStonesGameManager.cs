using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FiveStonesGameManager : MonoBehaviour
{
    private static FiveStonesGameManager _instance;
    public static FiveStonesGameManager Instance { get { return _instance; } }

    public GameObject g_scoreText;
    public GameObject g_comboText;
    public GameObject g_timerText;
    public GameObject g_objectiveText;

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
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        g_scoreText.GetComponent<TMP_Text>().text = m_score.ToString();
        g_timerText.GetComponent<TMP_Text>().text = TimerManager.Instance.GetFormattedRemainingTimeMS();
        g_comboText.GetComponent<TMP_Text>().text = "Combo: " + ComboManager.Instance.GetCurrentCombo();
    }

    void StartGame()
    {
        TimerManager.Instance.StartCountdown(10);
        StartCoroutine(GetComponent<StoneSpawner>().SpawnStoneLoop());
    }
    public void StoneCaught(GameObject gameObject)
    {
        m_score += (baseScore * ComboManager.Instance.GetCurrentCombo());
        ComboManager.Instance.AddCombo();
    }

    private void OnDestroy()
    {
        if (this == _instance)
            _instance = null;
    }
}
