using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomizerManager : MonoBehaviour
{
    public enum ItemType
    {
        HAT,
        FACE,
        COLOR
    }

    [Serializable]
    public class Customizable
    {
        public string m_name;
        public ItemType m_itemType;
        public Sprite m_sprite;
        public Color m_color;
    }

    [SerializeField]
    public List<Customizable> m_hatPool = new List<Customizable>();
    [SerializeField]
    public List<Customizable> m_facePool = new List<Customizable>();
    [SerializeField]
    public List<Customizable> m_colorPool = new List<Customizable>();

    public int m_hatId = 0;
    public int m_faceId = 0;
    public int m_colorId = 0;

    public SpriteRenderer m_hatSprite;
    public SpriteRenderer m_eyeSprite;
    public SpriteRenderer m_colorSprite;

    public Button nextSceneButton;
    public TMP_InputField inputNameField;

    private static CustomizerManager _instance;
    public static CustomizerManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject instance = new GameObject("CustomizerManager");
                instance.AddComponent<CustomizerManager>();
            }

            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenuGameScene" || SceneManager.GetActiveScene().name == "CustomizeScene")
        {
            if (SceneManager.GetActiveScene().name == "CustomizeScene")
                UpdateButtonReferences();
            UpdateSpriteReferences();
            UpdateCosmetics();
        }
    }

    public void ScrollHat(bool forward)
    {
        if (forward)
        {
            m_hatId++;
            if (m_hatId >= m_hatPool.Count)
                m_hatId = 0;
        }
        else
        {
            m_hatId--;
            if (m_hatId < 0)
                m_hatId = m_hatPool.Count - 1;
        }
    }

    public void ScrollFace(bool forward)
    {
        if (forward)
        {
            m_faceId++;
            if (m_faceId >= m_facePool.Count)
                m_faceId = 0;
        }
        else
        {
            m_faceId--;
            if (m_faceId < 0)
                m_faceId = m_facePool.Count - 1;
        }
    }

    public void ScrollColor(bool forward)
    {
        if (forward)
        {
            m_colorId++;
            if (m_colorId >= m_colorPool.Count)
                m_colorId = 0;
        }
        else
        {
            m_colorId--;
            if (m_colorId < 0)
                m_colorId = m_colorPool.Count - 1;
        }
    }

    public void UpdateCharacterName()
    {
        if (inputNameField.text == "")
        {
            nextSceneButton.interactable = false;
            ColorBlock colorBlock = inputNameField.colors;
            colorBlock.normalColor = new Color32(255, 255, 255, 255);
            colorBlock.selectedColor = new Color32(255, 255, 255, 255);
            inputNameField.colors = colorBlock;
            return;
        }

        ScoreManager.Score score = ScoreManager.Instance.m_allScoreList.Where(x => x.m_username.ToLower() == inputNameField.text).FirstOrDefault();

        if (score == null)
        {
            nextSceneButton.interactable = true;
            ColorBlock colorBlock = inputNameField.colors;
            colorBlock.normalColor = new Color32(222, 255, 201, 255);
            colorBlock.selectedColor = new Color32(222, 255, 201, 255);
            inputNameField.colors = colorBlock;
        }
        else
        {
            nextSceneButton.interactable = false;
            ColorBlock colorBlock = inputNameField.colors;
            colorBlock.normalColor = new Color32(255, 198, 198, 255);
            colorBlock.selectedColor = new Color32(255, 198, 198, 255);
            inputNameField.colors = colorBlock;
            return;
        }


        ScoreManager.Instance.m_currentUsername = inputNameField.text;
    }

    private void UpdateCosmetics()
    {
        Customizable hatCustomizable = m_hatPool.ElementAtOrDefault(m_hatId);
        Customizable faceCustomizable = m_facePool.ElementAtOrDefault(m_faceId);
        Customizable colorCustomizable = m_colorPool.ElementAtOrDefault(m_colorId);

        m_hatSprite.sprite = null;
        m_eyeSprite.sprite = null;
        m_colorSprite.sprite = null;

        if (hatCustomizable != null && hatCustomizable.m_itemType == ItemType.HAT)
        {
            m_hatSprite.sprite = hatCustomizable.m_sprite;
            m_hatSprite.sortingOrder = 4;
        }

        if (faceCustomizable != null && faceCustomizable.m_itemType == ItemType.FACE)
        {
            m_eyeSprite.sprite = faceCustomizable.m_sprite;
            m_eyeSprite.sortingOrder = 4;
        }

        if (colorCustomizable != null && colorCustomizable.m_itemType == ItemType.COLOR)
        {
            m_colorSprite.color = colorCustomizable.m_color;
            m_colorSprite.sortingOrder = 3;
        }

    }

    private void UpdateButtonReferences()
    {
        nextSceneButton.onClick.RemoveAllListeners();
        nextSceneButton.onClick.AddListener(delegate { EnterMainScene(); });

        inputNameField.onValueChanged.RemoveAllListeners();
        inputNameField.onValueChanged.AddListener(delegate { UpdateCharacterName(); });
    }

    // Find another way to re-reference when changing scenes, but this works for now
    private void UpdateSpriteReferences()
    {
        if (m_hatSprite == null)
            m_hatSprite = GameObject.Find("HatSprite").GetComponent<SpriteRenderer>();

        if (m_eyeSprite == null)
            m_eyeSprite = GameObject.Find("EyeSprite").GetComponent<SpriteRenderer>();

        if (m_colorSprite == null)
            m_colorSprite = GameObject.Find("MainCharacter").GetComponent<SpriteRenderer>();
    }

    public void EnterMainScene()
    {
        ScoreManager.Instance.UpdateCurrentUserTotalScore();
        SceneManager.LoadScene("MainMenuGameScene");
    }
}
