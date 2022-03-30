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
    private GameObject CosmeticNPC;
    [SerializeField] 
    private TMP_InputField inputNameField;
    [SerializeField]
    private Button exitButton;

    private void Start()
    {
        inputNameField.text = ScoreManager.Instance.m_currentUsername;
        customizer = FindObjectOfType<CustomizerManager>();
        if (customizer == null)
        {
            CosmeticNPC.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        SetCloneCosmetics();
        ChangeCharacterName();
        customizer.nextSceneButton = exitButton;
        customizer.inputNameField = inputNameField;
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
        ScoreManager.Score score = ScoreManager.Instance.m_allScoreList.Where(x => x.m_username.ToLower() == inputNameField.text).FirstOrDefault();
        if (inputNameField.text == "" || (score != null && ScoreManager.Instance.m_currentUsername != score.m_username))
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

        ScoreManager.Instance.m_currentUsername = inputNameField.text;
    }

    public void ScrollHat(bool forward)
    {
        customizer.ScrollHat(forward);
    }

    public void ScrollFace(bool forward)
    {
        customizer.ScrollFace(forward);
    }

    public void ScrollColor(bool forward)
    {
        customizer.ScrollColor(forward);
    }
}
