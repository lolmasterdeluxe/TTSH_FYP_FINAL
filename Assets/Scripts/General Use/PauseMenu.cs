using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject quitPromptMenu;
    [SerializeField] private GameObject settingsMenu;

    public bool isPaused = false;

    [SerializeField] private AudioSource[] pauseMenuSound;

    private Pregame preGame;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        quitPromptMenu.SetActive(false);

        if (SceneManager.GetActiveScene().name != "MainMenuGameScene")
            preGame = GameObject.Find("Pregame").GetComponent<Pregame>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "MainMenuGameScene")
        {
            if (preGame.m_countdownOver)
                return;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        // Pause sound
        pauseMenuSound[1].Play();

        if (SceneManager.GetActiveScene().name != "MainMenuGameScene")
        {
            if (SceneManager.GetActiveScene().name == "FiveStonesFruitNinja")
                FiveStonesGameManager.Instance.audioSources[0].Pause();
            else if (SceneManager.GetActiveScene().name == "Chapteh")
                ChaptehGameManager.Instance.audioSources[0].Pause();
            else if (SceneManager.GetActiveScene().name == "Scissors Paper Stone")
                SPS_UIManager.Instance.bgmSource.Pause();
        }   
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        quitPromptMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        // Button Press sound
        pauseMenuSound[0].Play();

        if (SceneManager.GetActiveScene().name != "MainMenuGameScene")
        {
            if (SceneManager.GetActiveScene().name == "FiveStonesFruitNinja")
                FiveStonesGameManager.Instance.audioSources[0].UnPause();
            else if (SceneManager.GetActiveScene().name == "Chapteh")
                ChaptehGameManager.Instance.audioSources[0].UnPause();
            else if (SceneManager.GetActiveScene().name == "Scissors Paper Stone")
                SPS_UIManager.Instance.bgmSource.UnPause();
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenuGameScene");
        Time.timeScale = 1f;

        // Button Press sound
        pauseMenuSound[0].Play();
    }

    public void LoadSettings()
    {
        settingsMenu.SetActive(true);

        // Button Press sound
        pauseMenuSound[0].Play();
    }

    public void ExitSettings()
    {
        settingsMenu.SetActive(false);

        // Back button sound
        pauseMenuSound[2].Play();
    }

    public void LoadQuitPrompt()
    {
        quitPromptMenu.SetActive(true);

        // Button Press sound
        pauseMenuSound[0].Play();
    }

    public void ExitQuitPrompt()
    {
        quitPromptMenu.SetActive(false);

        // Back button sound
        pauseMenuSound[2].Play();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
        ScoreManager.Instance.EndSessionConcludeScore();
    }
}
