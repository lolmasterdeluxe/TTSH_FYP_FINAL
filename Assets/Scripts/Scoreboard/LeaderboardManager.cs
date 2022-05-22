using System.Collections;
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

    [SerializeField] private GameObject rankOneSlot;
    [SerializeField] private GameObject rankTwoSlot;
    [SerializeField] private GameObject rankThreeSlot;
    [SerializeField] private GameObject currentRankSlot;

    [SerializeField] private TMP_Text highestScorebarText;
    [SerializeField] private TMP_Text currentScorebarText;
    [SerializeField] private Slider scoreBarSlider;

    [SerializeField] private TMP_Text scorebarText;

    [SerializeField] private Image background;
    [SerializeField] private Sprite chaptehBg;
    [SerializeField] private Sprite fiveStonesBg;
    [SerializeField] private Sprite spsBg;
    [SerializeField] private Sprite parachuteBg;
    [SerializeField] private Sprite totalBg;

    [SerializeField] private CanvasGroup leaderboardCanvasGroup;
    [SerializeField] private CanvasGroup endScreenCanvasGroup;

    [SerializeField] private GameObject scissorsPaperStoneGroup;
    [SerializeField] private TMP_Text powerUpsPicked;
    [SerializeField] private TMP_Text enemiesKilled;

    [SerializeField] private GameObject parachuteGroup;
    [SerializeField] private TMP_Text balloonsCollected;
    [SerializeField] private TMP_Text obstaclesDodged;

    [SerializeField] private GameObject countryErasersGroup;
    [SerializeField] private TMP_Text totalMatches;

    [SerializeField] private GameObject overallGroup;
    [SerializeField] private TMP_Text totalScissorsPaperStoneScore;
    [SerializeField] private TMP_Text totalParachuteScore;
    [SerializeField] private TMP_Text totalMatchesScore;

    [SerializeField] private TMP_Text overallScoreText;
    [SerializeField] private TMP_Text standardGameScoreText;

    [SerializeField] private GameObject leaderboardPage;

    [SerializeField] private GameObject exitButton;
    [SerializeField] private GameObject exitCreditsButton;
    [SerializeField] private CanvasGroup artistThanksScreen;
    [SerializeField] private CanvasGroup programmerThanksScreen;
    [SerializeField] private CanvasGroup musicCreditsScreen;
    [SerializeField] private CanvasGroup specialThanksScreen;
    [SerializeField] private CanvasGroup thankYouScreen;

    [SerializeField] private GameObject Animations, RankPanel, RankTemplate;

    [SerializeField] private bool m_isFinalGame;

    [SerializeField] private AudioSource LeaderboardBGM;

    public void ShowLeaderBoard()
    {
        leaderboardPage.SetActive(true);
        TweenManager.Instance.AnimateFade(endScreenCanvasGroup, 0, 1);
        TweenManager.Instance.AnimateFade(leaderboardCanvasGroup, 1, 1);
    }

    public void ShowCredits()
    {
        TweenManager.Instance.AnimateFade(artistThanksScreen, 0, 0);
        TweenManager.Instance.AnimateFade(programmerThanksScreen, 0, 0);
        TweenManager.Instance.AnimateFade(musicCreditsScreen, 0, 0);
        TweenManager.Instance.AnimateFade(specialThanksScreen, 0, 0);
        TweenManager.Instance.AnimateFade(thankYouScreen, 0, 0);
        exitCreditsButton.SetActive(true);
        m_isFinalGame = true;
        Time.timeScale = 1f;
    }

    private void OnEnable()
    {
        LeaderboardBGM.Play();
        if (!m_isFinalGame)
        {
            Debug.Log("Leaderboard enabled");
            UpdateAllLeaderboardUIData();
            TweenManager.Instance.AnimateFade(endScreenCanvasGroup, 1, 1);
            TweenManager.Instance.AnimateFade(leaderboardCanvasGroup, 0, 0);
            endScreenCanvasGroup.gameObject.SetActive(true);
        }
        else
            StartCoroutine(ShowFinalGameAnimation());
    }

    public void UpdateAllLeaderboardUIData()
    {
        TweenManager.Instance.AnimateFade(endScreenCanvasGroup, 0, 0);
        TweenManager.Instance.AnimateFade(leaderboardCanvasGroup, 0, 0);
        TweenManager.Instance.AnimateFade(artistThanksScreen, 0, 0);
        TweenManager.Instance.AnimateFade(programmerThanksScreen, 0, 0);
        TweenManager.Instance.AnimateFade(musicCreditsScreen, 0, 0);
        TweenManager.Instance.AnimateFade(specialThanksScreen, 0, 0);
        TweenManager.Instance.AnimateFade(thankYouScreen, 0, 0);

        sortedScoreList = ScoreManager.Instance.m_allScoreList.Where(x => x.m_gamemode == leaderboardType.ToString()).OrderByDescending(x => x.m_score).ToList();
        currentScore = sortedScoreList.Where(x => x.m_username == ScoreManager.Instance.m_currentUsername).FirstOrDefault();

        UpdateBackground();
        UpdateEndScreen();
        UpdateLeaderboard();
    }

    public void UpdateEndScreen()
    {
        switch (leaderboardType)
        {
            case ScoreManager.Gamemode.SPS:
                scissorsPaperStoneGroup.SetActive(true);

                powerUpsPicked.text = SPS_UIManager.Instance.sweetCount.ToString();
                enemiesKilled.text = SPS_UIManager.Instance.enemyCount.ToString();
                break;
            case ScoreManager.Gamemode.FLAPPY:
                parachuteGroup.SetActive(true);

                obstaclesDodged.text = FlappyGameManager.Instance.ObstaclesPassed.ToString();
                balloonsCollected.text = FlappyGameManager.Instance.BalloonsCollected.ToString();
                break;
            case ScoreManager.Gamemode.COUNTRY_ERASERS:
                countryErasersGroup.SetActive(true);

                totalMatches.text = EraserGameManager.Instance.ErasersMatched.ToString();
                break;
            case ScoreManager.Gamemode.TOTAL:
                Animations.SetActive(false);
                overallGroup.SetActive(true);
                standardGameScoreText.transform.parent.transform.parent.gameObject.SetActive(false);

                List<ScoreManager.Score> scoreList = ScoreManager.Instance.m_allScoreList.Where(x => x.m_username == ScoreManager.Instance.m_currentUsername).ToList();

                ScoreManager.Score score = scoreList.Where(x => x.m_gamemode == ScoreManager.Gamemode.SPS.ToString()).FirstOrDefault();

                if (score != null)
                    totalScissorsPaperStoneScore.text = score.m_score.ToString();

                score = scoreList.Where(x => x.m_gamemode == ScoreManager.Gamemode.FLAPPY.ToString()).FirstOrDefault();

                if (score != null)
                    totalParachuteScore.text = score.m_score.ToString();

                score = scoreList.Where(x => x.m_gamemode == ScoreManager.Gamemode.COUNTRY_ERASERS.ToString()).FirstOrDefault();

                if (score != null)
                    totalMatchesScore.text = score.m_score.ToString();

                break;
        }

        overallScoreText.text = currentScore.m_score.ToString();
        standardGameScoreText.text = currentScore.m_score.ToString();
    }

    public IEnumerator ShowFinalGameAnimation()
    {
        // Display goodjob
        endScreenCanvasGroup.gameObject.SetActive(false);
        TweenManager.Instance.AnimateFade(artistThanksScreen, 1, 1);

        // Display overall leaderboard
        yield return new WaitForSeconds(3);
        TweenManager.Instance.AnimateFade(artistThanksScreen, 0, 1);
        TweenManager.Instance.AnimateFade(programmerThanksScreen, 1, 1);

        // Display credits screen
        yield return new WaitForSeconds(3);
        TweenManager.Instance.AnimateFade(programmerThanksScreen, 0, 1);
        TweenManager.Instance.AnimateFade(musicCreditsScreen, 1, 1);

        // Display music credits screen
        yield return new WaitForSeconds(3);
        TweenManager.Instance.AnimateFade(musicCreditsScreen, 0, 1);
        TweenManager.Instance.AnimateFade(specialThanksScreen, 1, 1);

        // Display thanks for playing screen
        yield return new WaitForSeconds(3);
        TweenManager.Instance.AnimateFade(musicCreditsScreen, 0, 1);
        TweenManager.Instance.AnimateFade(thankYouScreen, 1, 1);

        yield return new WaitForSeconds(3);
        TweenManager.Instance.AnimateFade(thankYouScreen, 0, 0);
        exitCreditsButton.SetActive(false);
        ExitLeaderboard();
    }

    public void StopCredits()
    {
        TweenManager.Instance.KillCanvasGroupTween(artistThanksScreen);
        TweenManager.Instance.KillCanvasGroupTween(programmerThanksScreen);
        TweenManager.Instance.KillCanvasGroupTween(musicCreditsScreen);
        TweenManager.Instance.KillCanvasGroupTween(specialThanksScreen);
        TweenManager.Instance.KillCanvasGroupTween(thankYouScreen);
        exitCreditsButton.SetActive(false);
        ExitLeaderboard();
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
            case ScoreManager.Gamemode.SPS:
                background.sprite = spsBg;
                break;
            case ScoreManager.Gamemode.FLAPPY:
                background.sprite = parachuteBg;
                break;
            case ScoreManager.Gamemode.COUNTRY_ERASERS:
                background.sprite = totalBg;
                break;
            case ScoreManager.Gamemode.TOTAL:
                background.sprite = totalBg;
                break;
        }
    }

    void UpdateRank()
    {
        if (sortedScoreList.ElementAtOrDefault(0) != null)
        {
            UpdateSlider(currentScore.m_score, sortedScoreList[0].m_score);
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

        if (sortedScoreList.ElementAtOrDefault(3) != null)
        {
            RankTemplate.SetActive(true);
            RankTemplate.transform.GetChild(0).GetComponent<TMP_Text>().text = (sortedScoreList.IndexOf(sortedScoreList[3]) + 1).ToString();
            UpdateAvatar(RankTemplate.transform.GetChild(1).gameObject, sortedScoreList[3]);
            RankTemplate.transform.GetChild(2).GetComponent<TMP_Text>().text = sortedScoreList[3].m_username;
            RankTemplate.transform.GetChild(3).GetComponent<TMP_Text>().text = sortedScoreList[3].m_score.ToString();
        }

        for (int i = 4; i < sortedScoreList.Count; ++i)
        {
            GameObject rankSlot = Instantiate(RankTemplate, RankPanel.transform);
            rankSlot.transform.GetChild(0).GetComponent<TMP_Text>().text = (sortedScoreList.IndexOf(sortedScoreList[i]) + 1).ToString();
            UpdateAvatar(rankSlot.transform.GetChild(1).gameObject, sortedScoreList[i]);
            rankSlot.transform.GetChild(2).GetComponent<TMP_Text>().text = sortedScoreList[i].m_username;
            rankSlot.transform.GetChild(3).GetComponent<TMP_Text>().text = sortedScoreList[i].m_score.ToString();
        }

        UpdateAvatar(currentRankSlot.transform.GetChild(1).gameObject, currentScore);
        currentRankSlot.transform.GetChild(0).GetComponent<TMP_Text>().text = (sortedScoreList.IndexOf(currentScore) + 1).ToString();
        currentRankSlot.transform.GetChild(3).GetComponent<TMP_Text>().text = currentScore.m_score.ToString();
    }

    void UpdateAvatar(GameObject avatar, ScoreManager.Score score)
    {
        CustomizerManager.Customizable hatCustomizable = CustomizerManager.Instance.m_hatPool.ElementAtOrDefault(score.m_hatId);
        CustomizerManager.Customizable faceCustomizable = CustomizerManager.Instance.m_facePool.ElementAtOrDefault(score.m_faceId);
        CustomizerManager.Customizable colorCustomizable = CustomizerManager.Instance.m_colorPool.ElementAtOrDefault(score.m_colourId);

        for (int i = 1; i < avatar.transform.childCount; i++)
            avatar.transform.GetChild(i).GetComponent<Image>().enabled = false;


        if (colorCustomizable != null)
        {
            avatar.transform.GetChild(0).GetComponent<Image>().color = colorCustomizable.m_color;
        }

        if (hatCustomizable != null && score.m_hatId != 0)
        {
            avatar.transform.GetChild(1).GetComponent<Image>().enabled = true;
            avatar.transform.GetChild(1).GetComponent<Image>().sprite = hatCustomizable.m_sprite;
        }

        if (faceCustomizable != null && score.m_faceId != 0)
        {
            avatar.transform.GetChild(2).GetComponent<Image>().enabled = true;
            avatar.transform.GetChild(2).GetComponent<Image>().sprite = faceCustomizable.m_sprite;
        }
    }

    void UpdateSlider(int currentScore, int highestScore)
    {
        currentScorebarText.text = currentScore.ToString();
        highestScorebarText.text = highestScore.ToString();
        scoreBarSlider.value = (float)currentScore / (float)highestScore;
    }

    public void ReturnSplashScreen()
    {
        SceneManager.LoadScene("CustomizeScene");
    }

    public void ExitLeaderboard()
    {
        Debug.Log("exit leaderboard");
        if (SceneManager.GetActiveScene().name == "MainMenuGameScene")
        {
            if (m_isFinalGame)
                Time.timeScale = 0f;
            else
                for (int i = 4; i < sortedScoreList.Count; ++i)
                    Destroy(RankPanel.transform.GetChild(i).gameObject);

            m_isFinalGame = false;
            gameObject.SetActive(false);
            leaderboardPage.SetActive(false);
            Debug.Log("Leaderboard closed");
        }
        else
            SceneManager.LoadScene("MainMenuGameScene");
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
}
