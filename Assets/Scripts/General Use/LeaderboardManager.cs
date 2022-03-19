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
    [SerializeField] private Sprite totalBg;

    [SerializeField] private CanvasGroup leaderboardCanvasGroup;
    [SerializeField] private CanvasGroup endScreenCanvasGroup;

    [SerializeField] private GameObject fiveStonesGroup;
    [SerializeField] private TMP_Text fiveStonesTotalCaughtText;
    [SerializeField] private TMP_Text rainbowCaughtText;

    [SerializeField] private GameObject chaptehGroup;
    [SerializeField] private TMP_Text redHoopsHit;
    [SerializeField] private TMP_Text greenHoopsHit;
    [SerializeField] private TMP_Text yellowHoopsHit;

    [SerializeField] private GameObject scissorsPaperStoneGroup;
    [SerializeField] private TMP_Text powerUpsPicked;
    [SerializeField] private TMP_Text enemiesKilled;

    [SerializeField] private GameObject overallGroup;
    [SerializeField] private TMP_Text totalChaptehScore;
    [SerializeField] private TMP_Text totalScissorsPaperStoneScore;
    [SerializeField] private TMP_Text totalFiveStonesScore;

    [SerializeField] private TMP_Text overallScoreText;
    [SerializeField] private TMP_Text standardGameScoreText;

    [SerializeField] private GameObject exitButton;
    [SerializeField] private GameObject leaderboardPage;

    [SerializeField] private GameObject splashScreenButton;
    [SerializeField] private CanvasGroup thanksScreenCanvasGroup;
    [SerializeField] private CanvasGroup goodJobScreenCanvasGroup;
    [SerializeField] private CanvasGroup creditsScreenCanvasGroup;
    [SerializeField] private CanvasGroup musicCreditsCanvasGroup;

    [SerializeField] private GameObject Animations;

    [SerializeField] private bool m_isFinalGame;

    public void ShowLeaderBoard()
    {
        leaderboardPage.SetActive(true);
        TweenManager.Instance.AnimateFade(endScreenCanvasGroup, 0, 1);
        TweenManager.Instance.AnimateFade(leaderboardCanvasGroup, 1, 1);

        if (m_isFinalGame)
            StartCoroutine(ShowFinalGameAnimation());
    }

    private IEnumerator ITransitionToLeaderBoard()
    {
        yield return new WaitForSeconds(0);
        TweenManager.Instance.AnimateFade(endScreenCanvasGroup, 0, 1);
        TweenManager.Instance.AnimateFade(leaderboardCanvasGroup, 1, 1);

        //StartCoroutine(ShowFinalGameAnimation());
    }

    private void OnEnable()
    {
        m_isFinalGame = false;
        splashScreenButton.SetActive(false);

        /*if (ScoreManager.Instance.GetCurrentSavedScoreCount() >= 4 || m_isFinalGame)
        {
            ScoreManager.Instance.ConcludeGameScore();
            exitButton.SetActive(false);
            splashScreenButton.SetActive(true);
            m_isFinalGame = true;
        }*/

        UpdateAllLeaderboardUIData();
        TweenManager.Instance.AnimateFade(endScreenCanvasGroup, 1, 1);
        //StartCoroutine(ShowLeaderboard());
    }

    public void UpdateAllLeaderboardUIData()
    {
        TweenManager.Instance.AnimateFade(endScreenCanvasGroup, 0, 0);
        TweenManager.Instance.AnimateFade(leaderboardCanvasGroup, 0, 0);
        TweenManager.Instance.AnimateFade(goodJobScreenCanvasGroup, 0, 0);
        TweenManager.Instance.AnimateFade(thanksScreenCanvasGroup, 0, 0);
        TweenManager.Instance.AnimateFade(musicCreditsCanvasGroup, 0, 0);
        TweenManager.Instance.AnimateFade(creditsScreenCanvasGroup, 0, 0);
        thanksScreenCanvasGroup.gameObject.SetActive(false);

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
            case ScoreManager.Gamemode.CHAPTEH:
                Animations.SetActive(true);
                chaptehGroup.SetActive(true);
                scissorsPaperStoneGroup.SetActive(false);
                fiveStonesGroup.SetActive(false);
                overallGroup.SetActive(false);
                standardGameScoreText.transform.parent.transform.parent.gameObject.SetActive(true);

                redHoopsHit.text = ChaptehGameManager.Instance.redCount.ToString();
                greenHoopsHit.text = ChaptehGameManager.Instance.greenCount.ToString();
                yellowHoopsHit.text = ChaptehGameManager.Instance.yellowCount.ToString();
                break;
            case ScoreManager.Gamemode.FIVESTONES:
                Animations.SetActive(true);
                chaptehGroup.SetActive(false);
                scissorsPaperStoneGroup.SetActive(false);
                fiveStonesGroup.SetActive(true);
                overallGroup.SetActive(false);
                standardGameScoreText.transform.parent.transform.parent.gameObject.SetActive(true);

                fiveStonesTotalCaughtText.text = FiveStonesGameManager.Instance.m_totalCaught.ToString();
                rainbowCaughtText.text = FiveStonesGameManager.Instance.m_totalRainbowCaught.ToString();
                break;
            case ScoreManager.Gamemode.SPS:
                Animations.SetActive(true);
                chaptehGroup.SetActive(false);
                scissorsPaperStoneGroup.SetActive(true);
                fiveStonesGroup.SetActive(false);
                overallGroup.SetActive(false);
                standardGameScoreText.transform.parent.transform.parent.gameObject.SetActive(true);

                powerUpsPicked.text = SPS_UIManager.Instance.sweetCount.ToString();
                enemiesKilled.text = SPS_UIManager.Instance.enemyCount.ToString();
                break;
            case ScoreManager.Gamemode.TOTAL:
                Animations.SetActive(false);
                chaptehGroup.SetActive(false);
                scissorsPaperStoneGroup.SetActive(false);
                fiveStonesGroup.SetActive(false);
                overallGroup.SetActive(true);
                standardGameScoreText.transform.parent.transform.parent.gameObject.SetActive(false);

                List<ScoreManager.Score> scoreList = ScoreManager.Instance.m_allScoreList.Where(x => x.m_username == ScoreManager.Instance.m_currentUsername).ToList();

                ScoreManager.Score score = scoreList.Where(x => x.m_gamemode == ScoreManager.Gamemode.CHAPTEH.ToString()).FirstOrDefault();

                if (score != null)
                    totalChaptehScore.text = score.m_score.ToString();

                score = scoreList.Where(x => x.m_gamemode == ScoreManager.Gamemode.FIVESTONES.ToString()).FirstOrDefault();

                if (score != null)
                    totalFiveStonesScore.text = score.m_score.ToString();

                score = scoreList.Where(x => x.m_gamemode == ScoreManager.Gamemode.SPS.ToString()).FirstOrDefault();

                if (score != null)
                    totalScissorsPaperStoneScore.text = score.m_score.ToString();

                break;
        }

        overallScoreText.text = currentScore.m_score.ToString();
        standardGameScoreText.text = currentScore.m_score.ToString();
        
    }

    public IEnumerator ShowLeaderboard()
    {
        yield return new WaitForSeconds(3);
        TweenManager.Instance.AnimateFade(endScreenCanvasGroup, 0, 1);
        TweenManager.Instance.AnimateFade(leaderboardCanvasGroup, 1, 1);
        
        if (m_isFinalGame)
            StartCoroutine(ShowFinalGameAnimation());
    }

    public IEnumerator ShowFinalGameAnimation()
    {
        // Display goodjob
        yield return new WaitForSeconds(3);
        TweenManager.Instance.AnimateFade(leaderboardCanvasGroup, 0, 1);
        TweenManager.Instance.AnimateFade(goodJobScreenCanvasGroup, 1, 1);

        // Display overall leaderboard
        ScoreManager.Instance.m_currentGamemode = ScoreManager.Gamemode.TOTAL;
        UpdateAllLeaderboardUIData();
        yield return new WaitForSeconds(3);
        TweenManager.Instance.AnimateFade(goodJobScreenCanvasGroup, 0, 1);
        TweenManager.Instance.AnimateFade(leaderboardCanvasGroup, 1, 1);

        // Display credits screen
        yield return new WaitForSeconds(3);
        TweenManager.Instance.AnimateFade(leaderboardCanvasGroup, 0, 1);
        TweenManager.Instance.AnimateFade(creditsScreenCanvasGroup, 1, 1);

        // Display music credits screen
        yield return new WaitForSeconds(3);
        TweenManager.Instance.AnimateFade(creditsScreenCanvasGroup, 0, 1);
        TweenManager.Instance.AnimateFade(musicCreditsCanvasGroup, 1, 1);

        // Display thanks for playing screen
        yield return new WaitForSeconds(3);
        thanksScreenCanvasGroup.gameObject.SetActive(true);
        leaderboardCanvasGroup.gameObject.SetActive(false);
        TweenManager.Instance.AnimateFade(musicCreditsCanvasGroup, 0, 1);
        TweenManager.Instance.AnimateFade(thanksScreenCanvasGroup, 1, 1);
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


        if (colorCustomizable != null && score.m_colourId != 0)
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
            gameObject.SetActive(false);
        else
            SceneManager.LoadScene("MainMenuGameScene");
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
}
