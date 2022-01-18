﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    public ScoreManager.Gamemode leaderboardType;

    List<ScoreManager.Score> sortedScoreList;
    ScoreManager.Score currentScore;

    public GameObject rankOneSlot;
    public GameObject rankTwoSlot;
    public GameObject rankThreeSlot;
    public GameObject currentRankSlot;

    public TMP_Text highestScorebarText;
    public TMP_Text currentScorebarText;
    public Slider scoreBarSlider;

    public TMP_Text scorebarText;

    public Image background;
    public Sprite chaptehBg;
    public Sprite fiveStonesBg;
    public Sprite spsBg;
    public Sprite totalBg;

    public CanvasGroup leaderboardCanvasGroup;
    public CanvasGroup endScreenCanvasGroup;

    public GameObject fiveStonesGroup;
    public TMP_Text fiveStonesTotalCaughtText;
    public TMP_Text rainbowCaughtText;

    public GameObject chaptehGroup;
    public TMP_Text redHoopsHit;
    public TMP_Text greenHoopsHit;
    public TMP_Text yellowHoopsHit;

    public GameObject scissorsPaperStoneGroup;
    public TMP_Text powerUpsPicked;
    public TMP_Text enemiesKilled;

    public GameObject overallGroup;
    public TMP_Text totalChaptehScore;
    public TMP_Text totalScissorsPaperStoneScore;
    public TMP_Text totalFiveStonesScore;

    public TMP_Text overallScoreText;

    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnEnable()
    {
        sortedScoreList = ScoreManager.Instance.m_allScoreList.Where(x => x.m_gamemode == leaderboardType.ToString()).OrderByDescending(x => x.m_score).ToList();
        currentScore = sortedScoreList.Where(x => x.m_username == ScoreManager.Instance.m_currentUsername).FirstOrDefault();
        UpdateBackground();
        UpdateEndScreen();
        UpdateLeaderboard();
    }

    public void UpdateEndScreen()
    {
        TweenManager.Instance.AnimateFade(endScreenCanvasGroup, 1, 0);
        TweenManager.Instance.AnimateFade(leaderboardCanvasGroup, 0, 0);

        switch (leaderboardType)
        {
            case ScoreManager.Gamemode.CHAPTEH:
                chaptehGroup.SetActive(true);
                scissorsPaperStoneGroup.SetActive(false);
                fiveStonesGroup.SetActive(false);
                overallGroup.SetActive(false);

                redHoopsHit.text = ChaptehGameManager.Instance.redCount.ToString();
                greenHoopsHit.text = ChaptehGameManager.Instance.greenCount.ToString();
                yellowHoopsHit.text = ChaptehGameManager.Instance.yellowCount.ToString();
                break;
            case ScoreManager.Gamemode.FIVESTONES:
                chaptehGroup.SetActive(false);
                scissorsPaperStoneGroup.SetActive(false);
                fiveStonesGroup.SetActive(true);
                overallGroup.SetActive(false);

                fiveStonesTotalCaughtText.text = FiveStonesGameManager.Instance.m_totalCaught.ToString();
                rainbowCaughtText.text = FiveStonesGameManager.Instance.m_totalRainbowCaught.ToString();
                break;
            case ScoreManager.Gamemode.SPS:
                chaptehGroup.SetActive(false);
                scissorsPaperStoneGroup.SetActive(true);
                fiveStonesGroup.SetActive(false);
                overallGroup.SetActive(false);

                powerUpsPicked.text = SPS_PlayerManager.Instance.sweetCount.ToString();
                enemiesKilled.text = SPS_PlayerManager.Instance.enemyCount.ToString();
                break;
            case ScoreManager.Gamemode.TOTAL:
                chaptehGroup.SetActive(false);
                scissorsPaperStoneGroup.SetActive(false);
                fiveStonesGroup.SetActive(false);
                overallGroup.SetActive(true);

                List<ScoreManager.Score> scoreList = ScoreManager.Instance.m_allScoreList.Where(x => x.m_username == ScoreManager.Instance.m_currentUsername).ToList();

                totalChaptehScore.text = scoreList.Where(x => x.m_gamemode == ScoreManager.Gamemode.CHAPTEH.ToString()).FirstOrDefault().m_score.ToString();
                totalFiveStonesScore.text = scoreList.Where(x => x.m_gamemode == ScoreManager.Gamemode.FIVESTONES.ToString()).FirstOrDefault().m_score.ToString();
                totalScissorsPaperStoneScore.text = scoreList.Where(x => x.m_gamemode == ScoreManager.Gamemode.SPS.ToString()).FirstOrDefault().m_score.ToString();
                break;
        }

        overallScoreText.text = currentScore.m_score.ToString();

        StartCoroutine(ShowLeaderboard());
    }

    public IEnumerator ShowLeaderboard()
    {
        yield return new WaitForSeconds(3);
        TweenManager.Instance.AnimateFade(endScreenCanvasGroup, 0, 1);
        TweenManager.Instance.AnimateFade(leaderboardCanvasGroup, 1, 1);
    }

    public void UpdateLeaderboard()
    {
        UpdateBackground();
        UpdateRank();
    }
    void UpdateBackground()
    {
        switch (leaderboardType)
        {
            case ScoreManager.Gamemode.CHAPTEH:
                background.sprite = chaptehBg;
                break;
            case ScoreManager.Gamemode.FIVESTONES:
                background.sprite = fiveStonesBg;
                break;
            case ScoreManager.Gamemode.SPS:
                background.sprite = spsBg;
                break;
            case ScoreManager.Gamemode.TOTAL:
                background.sprite = totalBg;
                break;
        }
    }

    void UpdateRank()
    {
        UpdateSlider(currentScore.m_score, sortedScoreList[0].m_score);

        if (sortedScoreList.ElementAtOrDefault(0) != null)
        {
            UpdateAvatar(rankOneSlot.transform.GetChild(0).gameObject, sortedScoreList[0]);
            rankOneSlot.transform.GetChild(1).GetComponent<TMP_Text>().text = sortedScoreList[0].m_username;
            rankOneSlot.transform.GetChild(2).GetComponent<TMP_Text>().text = sortedScoreList[0].m_score.ToString();
        }

        if (sortedScoreList.ElementAtOrDefault(1) != null)
        {
            UpdateAvatar(rankTwoSlot.transform.GetChild(0).gameObject, sortedScoreList[1]);
            rankTwoSlot.transform.GetChild(1).GetComponent<TMP_Text>().text = sortedScoreList[1].m_username;
            rankTwoSlot.transform.GetChild(2).GetComponent<TMP_Text>().text = sortedScoreList[1].m_score.ToString();
        }

        if (sortedScoreList.ElementAtOrDefault(2) != null)
        {
            UpdateAvatar(rankThreeSlot.transform.GetChild(0).gameObject, sortedScoreList[2]);
            rankThreeSlot.transform.GetChild(1).GetComponent<TMP_Text>().text = sortedScoreList[2].m_username;
            rankThreeSlot.transform.GetChild(2).GetComponent<TMP_Text>().text = sortedScoreList[2].m_score.ToString();
        }

        UpdateAvatar(currentRankSlot.transform.GetChild(1).gameObject, currentScore);
        currentRankSlot.transform.GetChild(0).GetComponent<TMP_Text>().text = (sortedScoreList.IndexOf(currentScore) + 1).ToString();
        currentRankSlot.transform.GetChild(3).GetComponent<TMP_Text>().text = currentScore.m_score.ToString();
    }

    void UpdateAvatar(GameObject avatar, ScoreManager.Score score)
    {
        CustomizerManager.Customizable hatCustomizable = CustomizerManager.Instance.m_hatPool[score.m_hatId];
        CustomizerManager.Customizable faceCustomizable = CustomizerManager.Instance.m_facePool[score.m_faceId];
        CustomizerManager.Customizable colorCustomizable = CustomizerManager.Instance.m_colorPool[score.m_colourId];

        for (int i = 1; i < avatar.transform.childCount; i++)
            avatar.transform.GetChild(i).GetComponent<Image>().enabled = false;

        if (hatCustomizable.m_bone == CustomizerManager.Bone.HAT && hatCustomizable.m_sprite != null)
        {
            avatar.transform.GetChild(2).GetComponent<Image>().enabled = true;
            avatar.transform.GetChild(2).GetComponent<Image>().sprite = hatCustomizable.m_sprite;
        }
        else if (hatCustomizable.m_bone == CustomizerManager.Bone.HEAD_BAND && hatCustomizable.m_sprite != null)
        {
            avatar.transform.GetChild(3).GetComponent<Image>().enabled = true;
            avatar.transform.GetChild(3).GetComponent<Image>().sprite = hatCustomizable.m_sprite;
        }

        if (faceCustomizable.m_bone == CustomizerManager.Bone.GLASSES && faceCustomizable.m_sprite != null)
        {
            avatar.transform.GetChild(4).GetComponent<Image>().enabled = true;
            avatar.transform.GetChild(4).GetComponent<Image>().sprite = faceCustomizable.m_sprite;
        }
        else if (faceCustomizable.m_bone == CustomizerManager.Bone.MOUTH && faceCustomizable.m_sprite != null)
        {
            avatar.transform.GetChild(1).GetComponent<Image>().enabled = true;
            avatar.transform.GetChild(1).GetComponent<Image>().sprite = faceCustomizable.m_sprite;
        }
    }

    void UpdateSlider(int currentScore, int highestScore)
    {
        currentScorebarText.text = currentScore.ToString();
        highestScorebarText.text = highestScore.ToString();
        scoreBarSlider.value = (float)currentScore / (float)highestScore;
    }

    public void ExitLeaderboard()
    {
        if (SceneManager.GetActiveScene().name == "MainMenuGameScene")
        {
            GameObject.Find("MainCharacter").GetComponent<PlayerMovementScript>().Start();
            gameObject.SetActive(false);
        }
        else
        {
            SceneManager.LoadScene("MainMenuGameScene");
        }
    }
}
