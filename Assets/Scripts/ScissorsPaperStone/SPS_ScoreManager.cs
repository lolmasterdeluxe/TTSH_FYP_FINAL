using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SPS_ScoreManager : MonoBehaviour
{
    //this script handles the score the player gets
    //we take the final score and sent it into a local general pooled score

    #region Variables

    public int playerScore;
    public Text scoreText;
    ComboManager combomanagerInstance;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        //player starts with a score of 0
        playerScore = 0;
        //add reference HERE
        combomanagerInstance = FindObjectOfType<ComboManager>();
    }

    private void Update()
    {
        scoreText.text = "Score: " + playerScore.ToString();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerScores();
        }
    }

    #endregion

    #region Functions   

    public void PlayerScores()
    {
        playerScore += 1 * combomanagerInstance.GetCurrentCombo();
        Debug.Log("combo current:" + combomanagerInstance.GetCurrentCombo());
        combomanagerInstance.AddCombo();
    }

    #endregion
}
