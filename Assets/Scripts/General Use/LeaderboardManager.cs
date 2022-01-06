using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    public GameObject rankOneSlot;
    public GameObject rankTwoSlot;
    public GameObject rankThreeSlot;
    public GameObject currentRankSlot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRank();
    }

    void UpdateLeaderboard()
    {
        UpdateRank();
        UpdateSlider();
    }
    void UpdateRank()
    {
        if (ScoreManager.Instance.m_allScoreList.Count <= 0)
            return;

        rankOneSlot.transform.GetChild(1).GetComponent<TMP_Text>().text = ScoreManager.Instance.m_allScoreList[0].m_username;
        rankOneSlot.transform.GetChild(2).GetComponent<TMP_Text>().text = ScoreManager.Instance.m_allScoreList[0].m_score.ToString();

        rankTwoSlot.transform.GetChild(1).GetComponent<TMP_Text>().text = ScoreManager.Instance.m_allScoreList[1].m_username;
        rankTwoSlot.transform.GetChild(2).GetComponent<TMP_Text>().text = ScoreManager.Instance.m_allScoreList[1].m_score.ToString();

        rankThreeSlot.transform.GetChild(1).GetComponent<TMP_Text>().text = ScoreManager.Instance.m_allScoreList[2].m_username;
        rankThreeSlot.transform.GetChild(2).GetComponent<TMP_Text>().text = ScoreManager.Instance.m_allScoreList[2].m_score.ToString();
    }
    void UpdateSlider()
    {

    }
}
