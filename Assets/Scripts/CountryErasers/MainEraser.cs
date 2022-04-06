using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainEraser : MonoBehaviour
{
    [SerializeField] private GameObject cardBack;
    [SerializeField] private GameObject cardFront;
    
    private int _id;
    
    public int id
    {
        get { return _id; }
    }

    private void Start()
    {
        Invoke("OpenEraser", 3);
        Invoke("Cover", EraserGameManager.Instance.openTimer +3);
        Invoke("CanStartRevealing", EraserGameManager.Instance.openTimer + 5);
    }

    private void OnMouseDown()
    {
        if (!EraserGameManager.Instance.m_gameEnded)
        {
            if (GetComponent<Animator>().GetBool("onIdle") == true)
            {
                if (cardBack.activeSelf && EraserGameManager.Instance.canReveal && EraserGameManager.Instance.startRevealing)
                {
                    //cardBack.SetActive(!cardBack.activeSelf);
                    OpenEraser();

                    EraserGameManager.Instance.EraserRevealed(this);
                    print("revealing");
                }
            }
        }
    }

    public void ChangeMaterial(int id, Material material)
    {
        _id = id;
        transform.GetChild(1).GetComponent<MeshRenderer>().material = material  ;
    }

    public void Cover()
    {
        //cardBack.SetActive(true);
        GetComponent<Animator>().SetBool("onCover",true);
        Invoke("OnIdle",0.5f);
        Invoke("CanStartRevealing", 0.5f);
    }

    public void ChangeLayer()
    {
        int temp = cardBack.GetComponent<SpriteRenderer>().sortingOrder;
        cardBack.GetComponent<SpriteRenderer>().sortingOrder = cardFront.GetComponent<SpriteRenderer>().sortingOrder;
        cardFront.GetComponent<SpriteRenderer>().sortingOrder = temp;
    }

    private void OnIdle()
    {
        GetComponent<Animator>().SetBool("onIdle",true);
        GetComponent<Animator>().SetBool("onCover",false);

    }
    private void OpenEraser()
    {
        GetComponent<Animator>().SetBool("onIdle",false);
    }

    public void CanStartRevealing()
    {
        EraserGameManager.Instance.startRevealing = true;
    }
}
