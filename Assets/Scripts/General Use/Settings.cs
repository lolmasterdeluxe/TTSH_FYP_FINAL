using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class Settings : MonoBehaviour
{
    public enum GameMode
    {
        MAINMENU,
        SPS,
        FIVE_STONES,
        CHAPTEH,
        FLAPPY,
        COUNTRY_ERASERS,
        HANGMAN
    }

    public GameMode currentGamemode;

    // Master Volume
    public Slider masterVolSlider;
    public TMP_Text masterVolText;

    // Music Volume
    public Slider musicVolSlider;
    public TMP_Text musicVolText;

    // SFX Volume
    public Slider sfxVolSlider;
    public TMP_Text sfxVolText;

    // Start is called before the first frame update
    void Start()
    {
        masterVolText.text = "100";
        musicVolText.text = "100";
        sfxVolText.text = "100";
    }

    // Adjusts all volumes in the scene
    public void MasterVolume()
    {
        switch (currentGamemode)
        {
            case GameMode.MAINMENU:
                GameObject MainMenu = GameObject.Find("Main Menu");
                // BGM volume adjuster
                MainMenu.transform.GetChild(0).GetComponent<AudioSource>().volume = masterVolSlider.value;
                // Footsteps volume adjuster
                MainMenu.transform.GetChild(1).GetComponent<AudioSource>().volume = masterVolSlider.value;
                // Interact volume adjuster
                MainMenu.transform.GetChild(2).GetComponent<AudioSource>().volume = masterVolSlider.value;
                // Hover volume adjuster
                MainMenu.transform.GetChild(3).GetComponent<AudioSource>().volume = masterVolSlider.value;
                masterVolText.text = Mathf.RoundToInt(masterVolSlider.value * 100).ToString();
                break;
            case GameMode.SPS:
                // BGM volume adjuster
                SPS_UIManager.Instance.bgmSource.volume = masterVolSlider.value;
                // Jump audio volume adjuster
                SPS_PlayerManager.Instance.jumpSFX.volume = masterVolSlider.value;
                // Stunned audio volmme adjuster
                SPS_PlayerManager.Instance.stunnedSFX.volume = masterVolSlider.value;
                // Scissors Atk audio volmme adjuster
                SPS_PlayerManager.Instance.scissorsAtkSFX.volume = masterVolSlider.value;
                // Paper Atk audio volume adjuster
                SPS_PlayerManager.Instance.paperAtkSFX.volume = masterVolSlider.value;
                // Stone Atk audio volume adjuster
                SPS_PlayerManager.Instance.stoneAtkSFX.volume = masterVolSlider.value;
                // Power Up audio volume adjuster
                SPS_PlayerManager.Instance.powerupSFX.volume = masterVolSlider.value;
                masterVolText.text = Mathf.RoundToInt(masterVolSlider.value * 100).ToString();
                break;
            case GameMode.CHAPTEH:
                // BGM volume adjuster
                ChaptehGameManager.Instance.audioSources[0].volume = masterVolSlider.value;
                for (int i = 0; i < 6; i++)
                {
                    ChaptehGameManager.Instance.audioSources[i].volume = masterVolSlider.value;
                    masterVolText.text = Mathf.RoundToInt(masterVolSlider.value * 100).ToString();
                }
                break;
            case GameMode.FIVE_STONES:
                for(int i = 0; i < 6; i++)
                {
                    FiveStonesGameManager.Instance.audioSources[i].volume = masterVolSlider.value;
                    masterVolText.text = Mathf.RoundToInt(masterVolSlider.value * 100).ToString();
                }
                break;
            case GameMode.FLAPPY:
                for (int i = 0; i < 4; i++)
                {
                    FlappyGameManager.Instance.audioSources[i].volume = masterVolSlider.value;
                    masterVolText.text = Mathf.RoundToInt(masterVolSlider.value * 100).ToString();
                }
                break;
            case GameMode.COUNTRY_ERASERS:
                for (int i = 0; i < 4; i++)
                {
                    EraserGameManager.Instance.audioSources[i].volume = masterVolSlider.value;
                    masterVolText.text = Mathf.RoundToInt(masterVolSlider.value * 100).ToString();
                }
                break;
            case GameMode.HANGMAN:
                for (int i = 0; i < 5; i++)
                {
                    HangmanGameManager.Instance.audioSources[i].volume = masterVolSlider.value;
                    masterVolText.text = Mathf.RoundToInt(masterVolSlider.value * 100).ToString();
                }
                break;
        }
    }

    public void MusicVolume()
    {
        switch (currentGamemode)
        {
            case GameMode.MAINMENU:
                // BGM volume adjuster
                GameObject MainMenu = GameObject.Find("Main Menu");
                MainMenu.transform.GetChild(0).GetComponent<AudioSource>().volume = musicVolSlider.value;
                musicVolText.text = Mathf.RoundToInt(musicVolSlider.value * 100).ToString();
                break;
            case GameMode.SPS:
                // BGM volume adjuster
                SPS_UIManager.Instance.bgmSource.volume = musicVolSlider.value;
                musicVolText.text = Mathf.RoundToInt(musicVolSlider.value * 100).ToString();
                break;
            case GameMode.CHAPTEH:
                // BGM volume adjuster
                ChaptehGameManager.Instance.audioSources[0].volume = musicVolSlider.value;
                musicVolText.text = Mathf.RoundToInt(musicVolSlider.value * 100).ToString();
                break;
            case GameMode.FIVE_STONES:
                // BGM volume adjuster
                FiveStonesGameManager.Instance.audioSources[0].volume = musicVolSlider.value;
                musicVolText.text = Mathf.RoundToInt(musicVolSlider.value * 100).ToString();
                break;
            case GameMode.FLAPPY:
                FlappyGameManager.Instance.audioSources[0].volume = musicVolSlider.value;
                musicVolText.text = Mathf.RoundToInt(musicVolSlider.value * 100).ToString();
                break;
            case GameMode.COUNTRY_ERASERS:
                EraserGameManager.Instance.audioSources[0].volume = musicVolSlider.value;
                musicVolText.text = Mathf.RoundToInt(musicVolSlider.value * 100).ToString();
                break;
            case GameMode.HANGMAN:
                HangmanGameManager.Instance.audioSources[0].volume = musicVolSlider.value;
                musicVolText.text = Mathf.RoundToInt(musicVolSlider.value * 100).ToString();
                break;
        }
    }

    public void SFXVolume()
    {
        switch (currentGamemode)
        {
            case GameMode.MAINMENU:
                GameObject MainMenu = GameObject.Find("Main Menu");
                // Footsteps audio volume adjuster
                MainMenu.transform.GetChild(1).GetComponent<AudioSource>().volume = sfxVolSlider.value;
                // Interact audio volume adjuster
                MainMenu.transform.GetChild(2).GetComponent<AudioSource>().volume = sfxVolSlider.value;
                // Hover audio volume adjuster
                MainMenu.transform.GetChild(3).GetComponent<AudioSource>().volume = sfxVolSlider.value;
                sfxVolText.text = Mathf.RoundToInt(sfxVolSlider.value * 100).ToString();
                break;
            case GameMode.SPS:
                // Jump audio volume adjuster
                SPS_PlayerManager.Instance.jumpSFX.volume = sfxVolSlider.value;
                // Stunned audio volume adjuster
                SPS_PlayerManager.Instance.stunnedSFX.volume = sfxVolSlider.value;
                // Scissors Atk audio volume adjuster
                SPS_PlayerManager.Instance.scissorsAtkSFX.volume = sfxVolSlider.value;
                // Paper Atk audio volume adjuster
                SPS_PlayerManager.Instance.paperAtkSFX.volume = sfxVolSlider.value;
                // Stone Atk audio volume adjuster
                SPS_PlayerManager.Instance.stoneAtkSFX.volume = sfxVolSlider.value;
                // Power Up audio volume adjuster
                SPS_PlayerManager.Instance.powerupSFX.volume = sfxVolSlider.value;
                sfxVolText.text = Mathf.RoundToInt(sfxVolSlider.value * 100).ToString();
                break;
            case GameMode.CHAPTEH:
                for (int i = 1; i < 6; i++)
                {
                    ChaptehGameManager.Instance.audioSources[i].volume = sfxVolSlider.value;
                    sfxVolText.text = Mathf.RoundToInt(sfxVolSlider.value * 100).ToString();
                }
                break;
            case GameMode.FIVE_STONES:
                for (int i = 1; i < 6; i++)
                {
                    FiveStonesGameManager.Instance.audioSources[i].volume = sfxVolSlider.value;
                    sfxVolText.text = Mathf.RoundToInt(sfxVolSlider.value * 100).ToString();
                }
                break;
            case GameMode.FLAPPY:
                for (int i = 2; i < 4; i++)
                {
                    FlappyGameManager.Instance.audioSources[i].volume = sfxVolSlider.value;
                    sfxVolText.text = Mathf.RoundToInt(sfxVolSlider.value * 100).ToString();
                }
                break;
            case GameMode.COUNTRY_ERASERS:
                for (int i = 1; i < 4; i++)
                {
                    EraserGameManager.Instance.audioSources[i].volume = sfxVolSlider.value;
                    sfxVolText.text = Mathf.RoundToInt(sfxVolSlider.value * 100).ToString();
                }
                break;
            case GameMode.HANGMAN:
                for (int i = 1; i < 5; i++)
                {
                    HangmanGameManager.Instance.audioSources[i].volume = sfxVolSlider.value;
                    sfxVolText.text = Mathf.RoundToInt(sfxVolSlider.value * 100).ToString();
                }
                break;
        }
        
    }
}
