using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MainEraser : MonoBehaviour
{
    [SerializeField] private GameObject cardBack;
    [SerializeField] private GameObject cardFront;
    [SerializeField] private SceneController controller;
    private int _id;
    public int id
    {
        get { return _id; }
    }

   //private void Awake()
   //{
   //    cardBack = transform.GetChild(0).gameObject;
   //}
    private void OnMouseDown()
    {
        if (cardBack.activeSelf && controller.canReveal)
        {
            //cardBack.SetActive(!cardBack.activeSelf);
            GetComponent<Animator>().SetTrigger("onClick");
            controller.EraserRevealed(this);
            print("revealing");
        }
    }

    public void ChangeSprite(int id, Material material)
    {
        _id = id;
        transform.GetChild(1).GetComponent<MeshRenderer>().material = material;
    }

    public void Cover()
    {
        //cardBack.SetActive(true);
        GetComponent<Animator>().SetTrigger("onClick");
        Invoke("OnIdle",1.3f);
    }

    public void ChangeLayer()
    {
        int temp = cardBack.GetComponent<SpriteRenderer>().sortingOrder;
        cardBack.GetComponent<SpriteRenderer>().sortingOrder = cardFront.GetComponent<SpriteRenderer>().sortingOrder;
        cardFront.GetComponent<SpriteRenderer>().sortingOrder = temp;
    }

    private void OnIdle()
    {
        GetComponent<Animator>().SetTrigger("onIdle");
    }
}
