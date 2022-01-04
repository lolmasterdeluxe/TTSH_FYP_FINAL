using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject quitPromptMenu;
    public GameObject settingsMenu;

    public bool isPaused = false;

    private Pregame preGame;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        quitPromptMenu.SetActive(false);

        preGame = GameObject.Find("Pregame").GetComponent<Pregame>();
    }

    // Update is called once per frame
    void Update()
    {
        if (preGame.m_countdownOver)
            return;

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
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenuGameScene");
        Time.timeScale = 1f;
    }

    public void LoadSettings()
    {
        settingsMenu.SetActive(true);
    }

    public void ExitSettings()
    {
        settingsMenu.SetActive(false);
    }

    public void LoadQuitPrompt()
    {
        quitPromptMenu.SetActive(true);
    }

    public void ExitQuitPrompt()
    {
        quitPromptMenu.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
