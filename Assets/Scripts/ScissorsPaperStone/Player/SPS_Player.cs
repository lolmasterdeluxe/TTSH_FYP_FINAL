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
        P_SCISSOR, P_PAPER, P_STONE, P_JUMP, P_NONE
    };

    public PlayerChoice p_choice;

    #endregion

    #region Variables

    public Animator playerAC, playeractionAC;
    SPS_LivesManager livesInstance;
    SPS_ObjectSpawningScript objectspawningInstance;
    SPS_AttackCollision attackCollisionInstance;

    public GameObject playerActionAnimation;

    BoxCollider playerCollider;
    Vector3 originalColliderSize;

    public bool hasPowerup;
    public bool playerJumped;
    float playerjumpUptime;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        p_choice = PlayerChoice.P_NONE;
        hasPowerup = false;
        playerJumped = false;
        playerCollider = GetComponent<BoxCollider>();

        //we store the original collider size for reference
        originalColliderSize =
        new Vector3(playerCollider.size.x, playerCollider.size.y, playerCollider.size.z);

        playerAC = GetComponent<Animator>();
        playeractionAC = playerActionAnimation.GetComponent<Animator>();
        attackCollisionInstance = FindObjectOfType<SPS_AttackCollision>();
        livesInstance = FindObjectOfType<SPS_LivesManager>();
        objectspawningInstance = FindObjectOfType<SPS_ObjectSpawningScript>();

    }

    private void Update()
    {
        //if the list is empty we know that the current way is completed
        if (objectspawningInstance.objectwaveList.Count == 0)
        {
            objectspawningInstance.waveCompleted = false;
        }
    
        //we check to see if the player has jumped
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerJumps();
        }

        //we run this to code to change collider size as player is "jumping"
        if (playerJumped == true)
        {
            playerjumpUptime += Time.deltaTime;

            if (playerjumpUptime >= 0.75f)
            {
                playerCollider.size = originalColliderSize;
                playerJumped = false;
                playerjumpUptime = 0f;
            }
        }

        //we check to see if the player has attacked

        //attack was scissors
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerChoosesScissors();
        }

        //attack was paper
        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayerChoosesPaper();
        }

        //attack was stone
        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayerChoosesStone();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyTag")
        {
            Debug.Log("Player has collided with an enemy : trigger enter");

            //we set the animation here to be stunned
            playerAC.SetBool("PlayerStunned", true);

            //we now delete the instance of that gameobject in the list
            objectspawningInstance.objectwaveList.Remove(other.gameObject);
            
            //we now delete the object and its rigidbody from the scene
            Destroy(other.gameObject.GetComponent<Rigidbody>());
            Destroy(other.gameObject);

            //we do lives and combo calculations here
            livesInstance.PlayerTakesDamage();
            ComboManager.Instance.BreakCombo();  
        }

        else if (other.gameObject.tag == "Obstacle")
        {
            Debug.Log("Player has collided with an Obstacle: trigger enter");

            //we set the animation here to be stunned
            playerAC.SetBool("PlayerStunned", true);

            //we do lives and combo calculations here
            livesInstance.PlayerTakesDamage();
            ComboManager.Instance.BreakCombo();
        }

        else if (other.gameObject.tag == "Powerup")
        {
            Debug.Log("Player has collected a powerup: trigger enter");

            //we set the boolean to be true here
            hasPowerup = true;

            //remove the instance of the powerup from the object list
            objectspawningInstance.objectwaveList.Remove(other.gameObject);

            //we destroy the powerup gameobject since it has been "collected"
            Destroy(other.gameObject.GetComponent<Rigidbody>());
            Destroy(other.gameObject);
        }
    }

    #endregion

    #region Player Functions

    public void PlayerChoosesScissors()
    {
        p_choice = PlayerChoice.P_SCISSOR;
        playeractionAC.SetBool("PlayerActionWithScissors", true);
        playerAC.SetBool("PlayerAttackingWithScissors", true);

        //we call the AttackButtonPress function HERE
        attackCollisionInstance.AttackButtonPress();
    }

    public void PlayerChoosesPaper()
    {
        p_choice = PlayerChoice.P_PAPER;
        playerAC.SetBool("PlayerAttackingWithPaper", true);
        playeractionAC.SetBool("PlayerActionWithPaper", true);

        //we call the AttackButtonPress function HERE
        attackCollisionInstance.AttackButtonPress();
    }

    public void PlayerChoosesStone()
    {
        p_choice = PlayerChoice.P_STONE;
        playerAC.SetBool("PlayerAttackingWithStone", true);
        playeractionAC.SetBool("PlayerActionWithStone", true);

        //we call the AttackButtonPress function HERE
        attackCollisionInstance.AttackButtonPress();
    }

    public void PlayerJumps()
    {
        p_choice = PlayerChoice.P_JUMP;
        playerAC.SetBool("PlayerJumped", true);

        //we shift the collider up to simulate "jumping"
        playerCollider.size = new Vector3(playerCollider.size.x, playerCollider.size.y + 1.5f, playerCollider.size.z);
        playerJumped = true;

        //we call the JumpButtonPress function HERE
        attackCollisionInstance.JumpButtonPress();
    }

    #endregion
}
