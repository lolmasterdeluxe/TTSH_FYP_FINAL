using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    private int m_hatID = 0;
    private int m_faceID = 0;
    private int m_colorID = 0;

    public SpriteRenderer m_headSprite;
    public SpriteRenderer m_hatSprite;
    public SpriteRenderer m_eyeSprite;
    public SpriteRenderer m_mouthSprite;

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
        UpdateCosmetics();
    }

    public void ScrollHat(bool forward)
    {
        if (forward)
        {
            m_hatID++;
            if (m_hatID >= m_hatPool.Count)
                m_hatID = 0;
        }
        else
        {
            m_hatID--;
            if (m_hatID < 0)
                m_hatID = m_hatPool.Count - 1;
        }
    }

    public void ScrollFace(bool forward)
    {
        if (forward)
        {
            m_faceID++;
            if (m_faceID >= m_facePool.Count)
                m_faceID = 0;
        }
        else
        {
            m_faceID--;
            if (m_faceID < 0)
                m_faceID = m_facePool.Count - 1;
        }
    }

    public void ScrollColor(bool forward)
    {
        if (forward)
        {
            m_colorID++;
            if (m_colorID >= m_colorPool.Count)
                m_colorID = 0;
        }
        else
        {
            m_colorID--;
            if (m_colorID < 0)
                m_hatID = m_colorPool.Count - 1;
        }
    }

    void UpdateCosmetics()
    {
        Customizable hatCustomizable = m_hatPool[m_hatID];
        Customizable faceCustomizable = m_facePool[m_faceID];
        Customizable colorCustomizable = m_colorPool[m_colorID];

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
}
