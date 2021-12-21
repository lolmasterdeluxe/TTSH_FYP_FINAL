using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    SPS_ButtonBehaviour buttonBehaviourInstance;

    public GameObject playerActionAnimation;

    BoxCollider playerCollider;
    Vector3 originalBoxColliderPosition;

    public bool hasPowerup;
    public bool playerJumped;

    float playerjumpUptime;
    float playerStunnedCountdown;

    public GameObject enemyEndPosition;

    //for the UI buttons
    public GameObject scissorsButton, paperButton, stoneButton;
    public Vector3 newButtonSize, oldButtonSize;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        p_choice = PlayerChoice.P_NONE;
        hasPowerup = false;
        playerJumped = false;
        playerCollider = GetComponent<BoxCollider>();

        //we store the original collider size for reference
        originalBoxColliderPosition =
        new Vector3(0f, -0.1f, 0f);

        playerAC = GetComponent<Animator>();
        playeractionAC = playerActionAnimation.GetComponent<Animator>();
        attackCollisionInstance = FindObjectOfType<SPS_AttackCollision>();
        livesInstance = FindObjectOfType<SPS_LivesManager>();
        objectspawningInstance = FindObjectOfType<SPS_ObjectSpawningScript>();
        buttonBehaviourInstance = FindObjectOfType<SPS_ButtonBehaviour>();

        //we set the button sizes HERE
        newButtonSize = new Vector3(1.25f, 1.25f, 1.25f);
        oldButtonSize = new Vector3(1f, 1f, 1f);

        //add event listeners HERE
        ComboManager.Instance.e_comboBreak.AddListener(attackCollisionInstance.ComboBroken);

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

        //this code checks to move the player collider center 
        //back to original position after a certain amount of time has passed

        if (playerJumped == true)
        {
            playerjumpUptime += Time.deltaTime;

            if (playerjumpUptime >= 1.75f)
            {
                //reset the collider position
                playerCollider.center = originalBoxColliderPosition;
                playerJumped = false;

                //reset the time
                playerjumpUptime = 0f;
            }
        }

        //we check to see if the player has attacked

        //attack was scissors
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerChoosesScissors();
            buttonBehaviourInstance.ResizeButton(scissorsButton, newButtonSize);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            buttonBehaviourInstance.ResizeButton(scissorsButton, oldButtonSize);
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
            
        //this runs the countdown to reset the stunned back to false
        if (playerAC.GetBool("PlayerStunned") == true)
        {
            playerStunnedCountdown += Time.deltaTime;

            if (playerStunnedCountdown >= 1f)
            {
                playerAC.SetBool("PlayerStunned", false);
                //reset the counter
                playerStunnedCountdown = 0f;
            }
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

        //we shift the attack animation pane such that it fits
        playerActionAnimation.transform.localPosition = new Vector3(0.05f, -0.45f, 1f);
        playerActionAnimation.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void PlayerChoosesPaper()
    {
        p_choice = PlayerChoice.P_PAPER;
        playerAC.SetBool("PlayerAttackingWithPaper", true);
        playeractionAC.SetBool("PlayerActionWithPaper", true);

        //we call the AttackButtonPress function HERE
        attackCollisionInstance.AttackButtonPress();

        //we shift the attack animation such that it fits
        playerActionAnimation.transform.localPosition = new Vector3(0.75f, 0.35f, 1f);
        playerActionAnimation.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
    }

    public void PlayerChoosesStone()
    {
        p_choice = PlayerChoice.P_STONE;
        playerAC.SetBool("PlayerAttackingWithStone", true);
        playeractionAC.SetBool("PlayerActionWithStone", true);

        //we call the AttackButtonPress function HERE
        attackCollisionInstance.AttackButtonPress();

        //we shift the attack animation such that it fits
        playerActionAnimation.transform.localPosition = new Vector3(0.01f, 0.1f, 1f);
        playerActionAnimation.transform.localScale = new Vector3(0.85f, 0.85f, 1f);

    }

    public void PlayerJumps()
    {
        p_choice = PlayerChoice.P_JUMP;
        playerAC.SetBool("PlayerJumped", true);

        //shift the box collider center position UPWARDS
        playerCollider.center = playerCollider.center + new Vector3(0f, 2f, 0f);

        playerJumped = true;

        //we call the JumpButtonPress function HERE
        attackCollisionInstance.JumpButtonPress();
    }

    #endregion
}
