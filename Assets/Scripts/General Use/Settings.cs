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

    private Chapteh chapteh;
    private KickChapteh kickChapteh;

    // Start is called before the first frame update
    void Start()
    {
        masterVolText.text = "100";
        musicVolText.text = "100";
        sfxVolText.text = "100";

        //chapteh = GameObject.Find("Chapteh").GetComponent<Chapteh>();
        //kickChapteh = GameObject.Find("Chapteh Manager").GetComponent<KickChapteh>();
    }

    // Adjusts all volumes in the scene
    public void MasterVolume()
    {
        switch (currentGamemode)
        {
            case GameMode.MAINMENU:
                GameObject.Find("BGM").GetComponent<AudioSource>().volume = masterVolSlider.value;
                GameObject.Find("FootstepsSFX").GetComponent<AudioSource>().volume = masterVolSlider.value;
                GameObject.Find("InteractSFX").GetComponent<AudioSource>().volume = masterVolSlider.value;
                GameObject.Find("HoverSFX").GetComponent<AudioSource>().volume = masterVolSlider.value;
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
                // On Ring Hit audio volume adjuster
                chapteh.GetComponent<Chapteh>().onRingHitSource.volume = masterVolSlider.value;
                // Kick Chapteh audio volume adjuster
                kickChapteh.GetComponent<KickChapteh>().audioSources[0].volume = masterVolSlider.value;
                // Charge Bar audio volume adjuster 
                kickChapteh.audioSources[1].volume = masterVolSlider.value;
                masterVolText.text = Mathf.RoundToInt(masterVolSlider.value * 100).ToString();
                break;
            case GameMode.FIVE_STONES:
                for(int i = 0; i < 6; i++)
                {
                    FiveStonesGameManager.Instance.audioSources[i].volume = masterVolSlider.value;
                    masterVolText.text = Mathf.RoundToInt(masterVolSlider.value * 100).ToString();
                }
                break;
        }
    }

    public void MusicVolume()
    {
        switch(currentGamemode)
        {
            case GameMode.MAINMENU:
                // BGM volume adjuster
                GameObject.Find("BGM").GetComponent<AudioSource>().volume = musicVolSlider.value;
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
        }
    }

    public void SFXVolume()
    {
        switch (currentGamemode)
        {
            case GameMode.MAINMENU:
                // BGM volume adjuster
                GameObject.Find("FootstepsSFX").GetComponent<AudioSource>().volume = sfxVolSlider.value;
                GameObject.Find("InteractSFX").GetComponent<AudioSource>().volume = sfxVolSlider.value;
                GameObject.Find("HoverSFX").GetComponent<AudioSource>().volume = sfxVolSlider.value;
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
                // On Ring Hit audio volume adjuster
                chapteh.onRingHitSource.volume = sfxVolSlider.value;
                // Kick Chapteh audio volume adjuster
                kickChapteh.audioSources[0].volume = sfxVolSlider.value;
                // Charge Bar audio volume adjuster 
                kickChapteh.audioSources[1].volume = sfxVolSlider.value;
                sfxVolText.text = Mathf.RoundToInt(sfxVolSlider.value * 100).ToString();
                break;
            case GameMode.FIVE_STONES:
                for (int i = 1; i < 6; i++)
                {
                    FiveStonesGameManager.Instance.audioSources[i].volume = sfxVolSlider.value;
                    sfxVolText.text = Mathf.RoundToInt(sfxVolSlider.value * 100).ToString();
                }
                break;
        }
        
    }
}
