using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPS_PlayerManager : MonoBehaviour
{
    //this manager handles EVERYTHING related to the player

    #region Enumerations

    //Player's Choice
    public enum PlayerChoice
    { PLAYER_SCISSOR, PLAYER_PAPER, PLAYER_STONE, PLAYER_JUMP, PLAYER_NONE };

    public PlayerChoice player_choice;

    #endregion

    #region Variables

    [Tooltip("Reference to the ObjectManager")]
    SPS_ObjectManager objectManagerInstance;

    [Tooltip("References to Animators")]
    public Animator playerAC, playeractionAC;

    [Tooltip("Reference to the PlayerActionSprite GameObject")]
    [SerializeField]
    GameObject g_PlayerActionSprite;

    //variables involving player jumping
    BoxCollider2D collider_player;
    Vector2 v_originalcolliderposition;
    bool b_playerJumped;
    float f_playerJumpLifetime;

    //variables involving player stunned
    float f_playerStunnedLifetime;

    //variables involving powerups
    bool b_hasPowerup;


    #endregion

    #region Unity Callbacks

    private void Start()
    {
        //set reference to script HERE
        objectManagerInstance = FindObjectOfType<SPS_ObjectManager>();

        //set player choice to be NONE
        player_choice = PlayerChoice.PLAYER_NONE;

        //set boolean flags to be FALSE on start
        b_hasPowerup = false; b_playerJumped = false;

        //set collider box reference HERE
        collider_player = GetComponent<BoxCollider2D>();

        //store the original collider size HERE
        v_originalcolliderposition = collider_player.transform.position;

    }

    private void Update()
    {

        #region Timers

        //for jumping
        if (b_playerJumped == true)
        {
            f_playerJumpLifetime += Time.deltaTime;

            if (f_playerJumpLifetime >= 1.7f)
            {
                //reset collider position
                collider_player.offset = v_originalcolliderposition;
                b_playerJumped = false;

                //reset the jump timer lifetime
                f_playerJumpLifetime = 0;
            }

        }

        //for stunned
        if (playerAC.GetBool("PlayerStunned") == true)
        {
            f_playerStunnedLifetime = Time.deltaTime;

            if (f_playerStunnedLifetime >= 1f)
            {
                playerAC.SetBool("PlayerStunned", false);

                //reset the stunned timer lifetime
                f_playerStunnedLifetime = 0;
            }

        }

        #endregion

        #region Key Button Actions

        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerChoosesScissors();
            g_PlayerActionSprite.transform.localPosition = new Vector3(0.1f, -0.15f, 0f);
            g_PlayerActionSprite.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            ResetAnimationsAndChoice();
            MoveBoxCollider();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayerChoosesPaper();
            g_PlayerActionSprite.transform.localPosition = new Vector3(0.5f, 0.25f, 0f);
            g_PlayerActionSprite.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            ResetAnimationsAndChoice();
            MoveBoxCollider();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayerChoosesStone();
            g_PlayerActionSprite.transform.localPosition = new Vector3(0.03f, 0f, 0f);
            g_PlayerActionSprite.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            ResetAnimationsAndChoice();
            MoveBoxCollider();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerJumps();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            ResetAnimationsAndChoice();
            MoveBoxCollider();
        }

        #endregion
    }

    #endregion

    #region Player Functions

    public void PlayerChoosesScissors()
    {
        //set the enum
        player_choice = PlayerChoice.PLAYER_SCISSOR;
        //set the booleans on ACs
        playerAC.SetBool("PlayerAttackingWithScissors", true);
        playeractionAC.SetBool("PlayerActionWithScissors", true);
        MoveBoxCollider();
    }

    public void PlayerChoosesPaper()
    {
        //set the enum
        player_choice = PlayerChoice.PLAYER_SCISSOR;
        //set the booleans on ACs
        playerAC.SetBool("PlayerAttackingWithPaper", true);
        playeractionAC.SetBool("PlayerActionWithPaper", true);
        MoveBoxCollider();
    }

    public void PlayerChoosesStone()
    {
        //set the enum
        player_choice = PlayerChoice.PLAYER_SCISSOR;
        //set the booleans on ACs
        playerAC.SetBool("PlayerAttackingWithStone", true);
        playeractionAC.SetBool("PlayerActionWithStone", true);
        MoveBoxCollider();
    }

    public void PlayerJumps()
    {
        player_choice = PlayerChoice.PLAYER_JUMP;
        playerAC.SetBool("PlayerJumped", true);
        MoveBoxCollider();
    }

    public void MoveBoxCollider()
    {
        if (player_choice == PlayerChoice.PLAYER_SCISSOR ||
            player_choice == PlayerChoice.PLAYER_PAPER ||
            player_choice == PlayerChoice.PLAYER_STONE)
        {
            //we want to shift the collider to the RIGHT
            collider_player.offset = collider_player.offset = new Vector2(0.9f, 0f);
        }

        if (player_choice == PlayerChoice.PLAYER_JUMP)
        {
            //we want to shift the collider box UP
            collider_player.offset = collider_player.offset = new Vector2(0f, 0.9f);
            //set boolean HERE
            b_playerJumped = true;
        }

        if (player_choice == PlayerChoice.PLAYER_NONE)
        {
            //we want to shift the collider back to the original position
            collider_player.offset = v_originalcolliderposition;
        }
    }

    #endregion

    #region Animation Functions

    public void ResetAnimationsAndChoice()
    {
        playerAC.SetBool("PlayerAttackingWithScissors", false);
        playeractionAC.SetBool("PlayerActionWithScissors", false);
        playerAC.SetBool("PlayerAttackingWithPaper", false);
        playeractionAC.SetBool("PlayerActionWithPaper", false);
        playerAC.SetBool("PlayerAttackingWithStone", false);
        playeractionAC.SetBool("PlayerActionWithStone", false);
        playerAC.SetBool("PlayerJumped", false);
        playerAC.SetBool("PlayerStunned", false);

        //reset the player choice as well
        player_choice = PlayerChoice.PLAYER_NONE;
    }


    #endregion

    #region Trigger Callbacks

    private void OnTriggerEnter2D(Collider2D other)
    {
        //player is attacking HERE
        if (collider_player.transform.position.x != v_originalcolliderposition.x)
        {
            if (other.gameObject.tag == "EnemyTag")
            {
                //now we check to see if we win the matchup
                if (player_choice == PlayerChoice.PLAYER_SCISSOR
                    && other.gameObject.GetComponent<SPS_Enemy>().enemy_type == SPS_Enemy.EnemyType.ENEMY_PAPER)
                {
                    //remove the enemy from the list
                    objectManagerInstance.objectWaveList.Remove(other.gameObject);

                }



            }
        }
        //player is hit/stunned HERE
        else
        {
            if (other.gameObject.tag == "EnemyTag")
            {
                //player gets stunned
                playerAC.SetBool("PlayerStunned", true);
            }
        }
    }

    #endregion
}
