using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
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
        masterVolText.text = "0";
        musicVolText.text = "0";
        sfxVolText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void MasterVolume()
    {
        masterVolText.text = Mathf.RoundToInt(masterVolSlider.value * 1).ToString();
    }

    public void MusicVolume()
    {
        musicVolText.text = Mathf.RoundToInt(musicVolSlider.value * 1).ToString();
    }

    public void SFXVolume()
    {
        sfxVolText.text = Mathf.RoundToInt(sfxVolSlider.value * 1).ToString();
    }
}
