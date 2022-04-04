using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class HighscoreTable : MonoBehaviour
{
    [SerializeField] private Transform entryContainer;
    [SerializeField] private Transform entryTemplate;
    private List<HighscoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;
    // Start is called before the first frame update
    void Awake()
    {
        entryTemplate.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        highscoreEntryList = highscores.highscoreEntryList;
        for (int i = 0; i < highscoreEntryList.Count; ++i)
        {
            for (int j = i + 1; j < highscoreEntryList.Count; ++j)
            {
                if (highscoreEntryList[j].score > highscoreEntryList[i].score)
                {
                    // Swap
                    HighscoreEntry tmp = highscoreEntryList[i];
                    highscoreEntryList[i] = highscoreEntryList[j];
                    highscoreEntryList[j] = tmp;
                }
            }
        }

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 206f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(-168, 257 - templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {   
            case 1:
                rankString = "1";
                break;
            case 2:
                rankString = "2";
                break;
            case 3:
                rankString = "3";
                break;
            default:
                rankString = rank + "";
                break;
        }

        entryTransform.Find("Rank").GetComponent<TextMeshProUGUI>().text = rankString;

        entryTransform.Find("Username").GetComponent<TextMeshProUGUI>().text = highscoreEntry.name;

        entryTransform.Find("Points").GetComponent<TextMeshProUGUI>().text = highscoreEntry.score.ToString();

        CustomizerManager.Customizable hatCustomizable = CustomizerManager.Instance.m_hatPool.ElementAtOrDefault(highscoreEntry.HatId);
        CustomizerManager.Customizable faceCustomizable = CustomizerManager.Instance.m_facePool.ElementAtOrDefault(highscoreEntry.FaceId);
        CustomizerManager.Customizable colorCustomizable = CustomizerManager.Instance.m_colorPool.ElementAtOrDefault(highscoreEntry.ColorId);

        entryTransform.Find("Avatar").transform.GetChild(0).GetComponent<SpriteRenderer>().color = colorCustomizable.m_color;

        entryTransform.Find("Avatar").transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = hatCustomizable.m_sprite;

        entryTransform.Find("Avatar").transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = faceCustomizable.m_sprite;

        transformList.Add(entryTransform);
    }

    private void AddHighscoreEntry(int score, string name, int ColorId, int HatId, int FaceId)
    {
        // Create HighscoreEntry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name, ColorId = ColorId, HatId = HatId, FaceId = FaceId};

        // Load saves Highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        // Add new entry to Highscores
        highscores.highscoreEntryList.Add(highscoreEntry);

        // Save updated Highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    /*
     * Represents a single High score entry
     *  */
    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
        public int ColorId;
        public int HatId;
        public int FaceId;
    }
}
