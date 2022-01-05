﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CustomizerManager : MonoBehaviour
{
    public enum Bone
    {
        HAT,
        GLASSES,
        MOUTH,
        HEAD_BAND,
    }

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
        public Bone m_bone;
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

    private string m_name = string.Empty;
    private int m_hatId = 0;
    private int m_faceId = 0;
    private int m_colorId = 0;

    public SpriteRenderer m_headSprite;
    public SpriteRenderer m_hatSprite;
    public SpriteRenderer m_eyeSprite;
    public SpriteRenderer m_mouthSprite;

    public Button hatPreviousButton;
    public Button hatNextButton;
    public Button facePreviousButton;
    public Button faceNextButton;
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
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
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
                m_hatId = m_colorPool.Count - 1;
        }
    }

    void UpdateCharacterName()
    {
        // Check if username exists

        m_name = inputNameField.text;
    }

    void UpdateCosmetics()
    {
        Customizable hatCustomizable = m_hatPool[m_hatId];
        Customizable faceCustomizable = m_facePool[m_faceId];
        Customizable colorCustomizable = m_colorPool[m_colorId];

        if (hatCustomizable.m_bone == Bone.HAT)
        {
            m_hatSprite.sprite = hatCustomizable.m_sprite;
            m_headSprite.sprite = null;
        }
        else if (hatCustomizable.m_bone == Bone.HEAD_BAND)
        {
            m_hatSprite.sprite = null;
            m_headSprite.sprite = hatCustomizable.m_sprite;
        }

        if (faceCustomizable.m_bone == Bone.GLASSES)
        {
            m_eyeSprite.sprite = faceCustomizable.m_sprite;
            m_mouthSprite.sprite = null;
        }
        else if (faceCustomizable.m_bone == Bone.MOUTH)
        {
            m_eyeSprite.sprite = null;
            m_mouthSprite.sprite = faceCustomizable.m_sprite;
        }
    }

    void UpdateButtonReferences()
    {
        if (hatPreviousButton == null)
            hatPreviousButton = GameObject.Find("HatPrevious").GetComponent<Button>();

        if (hatNextButton == null)
            hatNextButton = GameObject.Find("HatNext").GetComponent<Button>();

        if (facePreviousButton == null)
            facePreviousButton = GameObject.Find("FacePrevious").GetComponent<Button>();

        if (faceNextButton == null)
            faceNextButton = GameObject.Find("FaceNext").GetComponent<Button>();

        if (nextSceneButton == null)
            nextSceneButton = GameObject.Find("MainGameScene").GetComponent<Button>();

        if (inputNameField == null)
            inputNameField = GameObject.Find("NameInput").GetComponent<TMP_InputField>();

        hatPreviousButton.onClick.RemoveAllListeners();
        hatPreviousButton.onClick.AddListener(delegate { ScrollHat(false); });

        hatNextButton.onClick.RemoveAllListeners();
        hatNextButton.onClick.AddListener(delegate { ScrollHat(true); });

        facePreviousButton.onClick.RemoveAllListeners();
        facePreviousButton.onClick.AddListener(delegate { ScrollFace(false); });

        faceNextButton.onClick.RemoveAllListeners();
        faceNextButton.onClick.AddListener(delegate { ScrollFace(true); });

        nextSceneButton.onClick.RemoveAllListeners();
        nextSceneButton.onClick.AddListener(delegate { EnterMainScene(); });

        inputNameField.onValueChanged.RemoveAllListeners();
        inputNameField.onValueChanged.AddListener(delegate { UpdateCharacterName(); });
    }

    // Find another way to re-reference when changing scenes, but this works for now
    void UpdateSpriteReferences()
    {
        if (m_headSprite == null)
            m_headSprite = GameObject.Find("HeadSprite").GetComponent<SpriteRenderer>();

        if (m_hatSprite == null)
            m_hatSprite = GameObject.Find("HatSprite").GetComponent<SpriteRenderer>();

        if (m_eyeSprite == null)
            m_eyeSprite = GameObject.Find("EyeSprite").GetComponent<SpriteRenderer>();

        if (m_mouthSprite == null)
            m_mouthSprite = GameObject.Find("MouthSprite").GetComponent<SpriteRenderer>();
    }

    public void EnterMainScene()
    {
        SceneManager.LoadScene("MainMenuGameScene");
    }
}
