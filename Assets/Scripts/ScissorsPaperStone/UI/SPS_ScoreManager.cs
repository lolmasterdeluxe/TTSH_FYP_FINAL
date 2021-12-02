using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SPS_ScoreManager : MonoBehaviour
{
    //this script handles the score the player gets
    //we take the final score and sent it into a local general pooled score

    #region Variables

    public int playerScore;
    int baseScore;
    public GameObject scoretextInstance;
    ComboManager combomanagerInstance;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        baseScore = 1;
        //player starts with a score of 0
        playerScore = 0;
        //add reference HERE
        combomanagerInstance = FindObjectOfType<ComboManager>();
    }

    private void Update()
    {
        scoretextInstance.GetComponent<TMP_Text>().text = "Score: " + playerScore;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerScores();
        }
    }

    #endregion

    #region Functions   

    public void PlayerScores()
    {
        combomanagerInstance.AddCombo();
        playerScore += baseScore * combomanagerInstance.GetCurrentCombo();
        Debug.Log("combo current:" + combomanagerInstance.GetCurrentCombo());
    }

    #endregion
}
