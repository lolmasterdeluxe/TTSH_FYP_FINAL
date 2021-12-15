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
    public List<Customizable> m_customizableList = new List<Customizable>();

    private List<Customizable> m_hatPool = new List<Customizable>();
    private List<Customizable> m_facePool = new List<Customizable>();
    private List<Customizable> m_colorPool = new List<Customizable>();

    private Customizable m_hat = null;
    private Customizable m_face = null;
    private Customizable m_color = null;

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
        UpdatePool();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdatePool()
    {
        m_hatPool = m_customizableList.Where(x => x.m_itemType == ItemType.HAT).ToList();
        m_facePool = m_customizableList.Where(x => x.m_itemType == ItemType.FACE).ToList();
        m_colorPool = m_customizableList.Where(x => x.m_itemType == ItemType.COLOR).ToList();
    }
}
