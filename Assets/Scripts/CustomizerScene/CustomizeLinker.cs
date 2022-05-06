using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CustomizeLinker : MonoBehaviour
{
    private CustomizerManager customizer;
    [SerializeField]
    private GameObject CloneChar;
    [SerializeField] 
    private GameObject CustomizeScreenContainer;
    [SerializeField]
    private GameObject CosmeticNPC, Player, JoystickPanel, DpadPanel;
    [SerializeField] 
    private TMP_InputField inputNameField;
    [SerializeField]
    private Button exitButton;
    private ScoreManager.Score[] score;

    private void Start()
    {
        inputNameField.text = ScoreManager.Instance.m_currentUsername;
        customizer = FindObjectOfType<CustomizerManager>();
        DeclareScoreArray();

        if (customizer == null)
        {
            CosmeticNPC.SetActive(false);
            gameObject.SetActive(false);
        }
        Debug.Log(score.Length);
    }

    private void Update()
    {
        SetCloneCosmetics();
        ChangeCharacterName();
        customizer.nextSceneButton = exitButton;
        customizer.inputNameField = inputNameField;
    }

    private void DeclareScoreArray()
    {
        score = new ScoreManager.Score[7];
        score[0] = ScoreManager.Instance.m_allScoreList.Where(x => x.m_userId == ScoreManager.Instance.m_currentUserID && x.m_gamemode == ScoreManager.Gamemode.TOTAL.ToString()).FirstOrDefault();
        score[1] = ScoreManager.Instance.m_allScoreList.Where(x => x.m_userId == ScoreManager.Instance.m_currentUserID && x.m_gamemode == ScoreManager.Gamemode.SPS.ToString()).FirstOrDefault();
        score[2] = ScoreManager.Instance.m_allScoreList.Where(x => x.m_userId == ScoreManager.Instance.m_currentUserID && x.m_gamemode == ScoreManager.Gamemode.FIVESTONES.ToString()).FirstOrDefault();
        score[3] = ScoreManager.Instance.m_allScoreList.Where(x => x.m_userId == ScoreManager.Instance.m_currentUserID && x.m_gamemode == ScoreManager.Gamemode.CHAPTEH.ToString()).FirstOrDefault();
        score[4] = ScoreManager.Instance.m_allScoreList.Where(x => x.m_userId == ScoreManager.Instance.m_currentUserID && x.m_gamemode == ScoreManager.Gamemode.FLAPPY.ToString()).FirstOrDefault();
        score[5] = ScoreManager.Instance.m_allScoreList.Where(x => x.m_userId == ScoreManager.Instance.m_currentUserID && x.m_gamemode == ScoreManager.Gamemode.COUNTRY_ERASERS.ToString()).FirstOrDefault();
        score[6] = ScoreManager.Instance.m_allScoreList.Where(x => x.m_userId == ScoreManager.Instance.m_currentUserID && x.m_gamemode == ScoreManager.Gamemode.HANGMAN.ToString()).FirstOrDefault();
    }

    private void SetCloneCosmetics()
    {
        if (CloneChar != null)
        {
            CloneChar.GetComponent<SpriteRenderer>().color = customizer.m_colorSprite.color;
            CloneChar.transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = customizer.m_hatSprite.sprite;
            CloneChar.transform.GetChild(1).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = customizer.m_eyeSprite.sprite;
            CloneChar.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
            CustomizeScreenContainer.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
        }
        else return;
    }

    public void ChangeCharacterName()
    {
        DeclareScoreArray();
        if (inputNameField.text == "Reset Leaderboard")
        {
            ScoreManager.Instance.DeleteCSV();
            inputNameField.text = "";
            return;
        }

        if (inputNameField.text == "")
        {
            exitButton.interactable = false;
            ColorBlock colorBlock = inputNameField.colors;
            colorBlock.normalColor = new Color32(255, 198, 198, 255);
            colorBlock.selectedColor = new Color32(255, 198, 198, 255);
            inputNameField.colors = colorBlock;
            return;
        }
        else
        {
            exitButton.interactable = true;
            ColorBlock colorBlock = inputNameField.colors;
            colorBlock.normalColor = new Color32(222, 255, 201, 255);
            colorBlock.selectedColor = new Color32(222, 255, 201, 255);
            inputNameField.colors = colorBlock;
        }
        UpdatePlayer();
    }

    public void ScrollHat(bool forward)
    {
        customizer.ScrollHat(forward);
        UpdatePlayer();
    }

    public void ScrollFace(bool forward)
    {
        customizer.ScrollFace(forward);
        UpdatePlayer();
    }

    public void ScrollColor(bool forward)
    {
        customizer.ScrollColor(forward);
        UpdatePlayer();
    }

    private void UpdatePlayer()
    {
        for (int i = 0; i < score.Length; ++i)
        {
            if (score[i] != null)
            {
                score[i].m_username = inputNameField.text;
                score[i].m_hatId = CustomizerManager.Instance.m_hatId;
                score[i].m_faceId = CustomizerManager.Instance.m_faceId;
                score[i].m_colourId = CustomizerManager.Instance.m_colorId;
            }
        }
        ScoreManager.Instance.m_currentUsername = inputNameField.text;
    }

    public void checkDpadJoystick()
    {
        if (Player.GetComponent<PlayerJoystickMovement>().enabled)
        {
            JoystickPanel.SetActive(true);
        }

        if (Player.GetComponent<PlayerDpadMovement>().enabled)
        {
            DpadPanel.SetActive(true);
        }
    }
}
