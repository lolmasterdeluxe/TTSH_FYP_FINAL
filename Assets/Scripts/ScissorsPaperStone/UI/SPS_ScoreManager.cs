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

    //add script references HERE
    SPS_AttackCollision attackCollisionInstance;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        baseScore = 1;
        //player starts with a score of 0
        playerScore = 0;

        ScoreManager.Instance.LoadNewGamemode(ScoreManager.Gamemode.SPS);

    }

    private void Update()
    {
        scoretextInstance.GetComponent<TMP_Text>().text = "Score: " + ScoreManager.Instance.GetCurrentGameScore();
    }

    #endregion

    #region Functions   

    public void PlayerScores()
    {
        ScoreManager.Instance.AddCurrentGameScore(baseScore * ComboManager.Instance.GetCurrentCombo());
    }

    #endregion
}
