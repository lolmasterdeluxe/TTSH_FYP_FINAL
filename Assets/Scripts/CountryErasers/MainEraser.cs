using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEraser : MonoBehaviour
{
    [SerializeField] private GameObject cardBack;
    [SerializeField] private SceneController controller;
    private int _id;
    public int id
    {
        get { return _id; }
    }

    private void Awake()
    {
        cardBack = transform.GetChild(0).gameObject;
    }
    private void OnMouseDown()
    {
        if (cardBack.activeSelf && controller.canReveal)
        {
            cardBack.SetActive(!cardBack.activeSelf);
            controller.EraserRevealed(this);
            print("revealing");
        }
    }

    public void ChangeSprite(int id, Sprite Image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = Image;
    }

    public void Cover()
    {
        cardBack.SetActive(true);
    }
}
