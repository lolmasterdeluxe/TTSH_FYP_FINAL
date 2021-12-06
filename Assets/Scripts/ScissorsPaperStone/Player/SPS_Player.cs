using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SPS_Player : MonoBehaviour
{
    //this script handles all things involved with the player

    #region Enumerations
    public enum PlayerChoice
    {
        P_SCISSOR, P_PAPER, P_STONE, P_JUMP, P_SLIDE, P_NONE
    };

    public PlayerChoice p_choice;

    #endregion

    #region Variables

    Animator ac;
    ComboManager combomanagerInstance;
    SPS_LivesManager livesInstance;
    SPS_ObjectSpawningScript objectspawningInstance;
    
    #endregion

    #region Unity Callbacks

    private void Start()
    {
        p_choice = PlayerChoice.P_NONE;
        ac = GetComponent<Animator>();
        livesInstance = FindObjectOfType<SPS_LivesManager>();
        combomanagerInstance = FindObjectOfType<ComboManager>();
        objectspawningInstance = FindObjectOfType<SPS_ObjectSpawningScript>();
    }

    private void Update()
    {
        //if the list is empty we know that the current way is completed
        if (objectspawningInstance.objectwaveList.Count == 0)
        {
            objectspawningInstance.waveCompleted = false;
            Debug.Log("List is empty");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyTag")
        {
            Debug.Log("Player has collided with an enemy : trigger enter");

            //we set the animation here to be stunned
            ac.SetBool("PlayerStunned", true);

            //we now delete the instance of that gameobject in the list
            objectspawningInstance.objectwaveList.Remove(other.gameObject);
            
            //we now delete the object and its rigidbody from the scene
            Destroy(other.gameObject.GetComponent<Rigidbody>());
            Destroy(other.gameObject);

            //we do lives and combo calculations here
            livesInstance.PlayerTakesDamage();
            combomanagerInstance.BreakCombo();  
        }

        else if (other.gameObject.tag == "Obstacle")
        {
            Debug.Log("Player has collided with an Obstacle: trigger enter");

            //we set the animation here to be stunned
            ac.SetBool("PlayerStunned", true);

            //we do lives and combo calculations here
            livesInstance.PlayerTakesDamage();
            combomanagerInstance.BreakCombo();

        }
    }

    #endregion

    #region Functions

    public void PlayerChoosesScissors()
    {
        p_choice = PlayerChoice.P_SCISSOR;
        ac.SetBool("PlayerAttackingWithScissors", true);
    }
    public void PlayerChoosesPaper()
    {
        p_choice = PlayerChoice.P_PAPER;
        ac.SetBool("PlayerAttackingWithPaper", true);
    }
    public void PlayerChoosesStone()
    {
        p_choice = PlayerChoice.P_STONE;
        ac.SetBool("PlayerAttackingWithStone", true);
    }

    public void PlayerJumps()
    {
        p_choice = PlayerChoice.P_JUMP;
        ac.SetBool("PlayerJumped", true);
    }

    #endregion
}
