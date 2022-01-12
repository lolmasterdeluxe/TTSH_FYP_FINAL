using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

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

    [Tooltip("Reference to the UIManager")]
    SPS_UIManager uiManagerInstance;

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

    //variables involving player attacking
    bool b_playerAttacked;
    float f_playerAttackLifetime;

    //variables involving player stunned
    float f_playerStunnedLifetime;

    //variables involving powerups
    bool b_hasPowerup;

    //variables for combo
    public GameObject g_comboGroup, g_comboText, g_comboText_finalPos;

    //variables for player idle character (endgame)
    public GameObject current_player_sprite, end_player_sprite;


    #endregion

    #region Unity Callbacks

    private void Start()
    {

        //set reference to scripts HERE
        objectManagerInstance = FindObjectOfType<SPS_ObjectManager>();
        uiManagerInstance = FindObjectOfType<SPS_UIManager>();

        //set player choice to be NONE
        player_choice = PlayerChoice.PLAYER_NONE;

        //set boolean flags to be FALSE on start
        b_hasPowerup = false; b_playerJumped = false;

        //set collider box reference HERE
        collider_player = GetComponent<BoxCollider2D>();

        //set original collider position HERE
        v_originalcolliderposition = new Vector2(0f, 0f);

        //set combo expiry HERE
        ComboManager.Instance.SetComboExpiry(4f);

        //we set the combo manager's alpha to be 0 on start
        TweenManager.Instance.AnimateFade(g_comboGroup.GetComponent<CanvasGroup>(), 0f, 0f);

    }

    private void Update()
    {
        if (uiManagerInstance.b_gameEnded == true)
        {
            ResetAnimationsAndChoice();
            current_player_sprite.SetActive(false);
            end_player_sprite.SetActive(true);
        }

        if (!uiManagerInstance.b_gameStart || uiManagerInstance.b_gameEnded)
            return;

        uiManagerInstance.UpdateComboScore(g_comboText);

        if (playerAC.GetBool("PlayerJumped") == false)
        {
            //set everything back to normal
            this.gameObject.transform.localPosition = new Vector3(-5.76f, -0.9f, -1f);
            this.gameObject.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        }


        #region Timers

        //for attacking 
        if (b_playerAttacked == true)
        {
            f_playerAttackLifetime += Time.deltaTime;

            if (f_playerAttackLifetime >= 0.375f)
            {
                //reset the animations
                ResetAnimationsAndChoice();

                //reset collider position
                MoveBoxCollider();

                //reset the boolean flag
                b_playerAttacked = false;

                //reset the jump timer lifetime
                f_playerAttackLifetime = 0;
            }
        }



        //for jumping
        if (b_playerJumped == true)
        {
            f_playerJumpLifetime += Time.deltaTime;

            if (f_playerJumpLifetime >= 0.795f)
            {
                //reset the animations
                ResetAnimationsAndChoice();

                //reset collider position
                MoveBoxCollider();

                //reset the boolean flag
                b_playerJumped = false;

                //reset the jump timer lifetime
                f_playerJumpLifetime = 0;
            }

        }

        //for stunned
        if (playerAC.GetBool("PlayerStunned") == true)
        {
            f_playerStunnedLifetime += Time.deltaTime;

            if (f_playerStunnedLifetime >= 1f)
            {
                playerAC.SetBool("PlayerStunned", false);

                //reset the stunned timer lifetime
                f_playerStunnedLifetime = 0f;
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

        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayerChoosesPaper();
            g_PlayerActionSprite.transform.localPosition = new Vector3(0.5f, 0.25f, 0f);
            g_PlayerActionSprite.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayerChoosesStone();
            g_PlayerActionSprite.transform.localPosition = new Vector3(0.03f, 0f, 0f);
            g_PlayerActionSprite.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerJumps();
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
        player_choice = PlayerChoice.PLAYER_PAPER;
        //set the booleans on ACs
        playerAC.SetBool("PlayerAttackingWithPaper", true);
        playeractionAC.SetBool("PlayerActionWithPaper", true);
        MoveBoxCollider();
    }

    public void PlayerChoosesStone()
    {
        //set the enum
        player_choice = PlayerChoice.PLAYER_STONE;
        //set the booleans on ACs
        playerAC.SetBool("PlayerAttackingWithStone", true);
        playeractionAC.SetBool("PlayerActionWithStone", true);
        MoveBoxCollider();
    }

    public void PlayerJumps()
    {
        player_choice = PlayerChoice.PLAYER_JUMP;
        this.gameObject.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
        this.gameObject.transform.localPosition = new Vector3(-5.76f, 0.22f, -1f);
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
            collider_player.offset = collider_player.offset = new Vector2(1.3f, 0f);
            //set boolean HERE
            b_playerAttacked = true;
        }

        if (player_choice == PlayerChoice.PLAYER_JUMP)
        {
            //we want to shift the collider box UP
            collider_player.offset = collider_player.offset = new Vector2(0f, 1.185f);

            //set the player size to be larger
            this.gameObject.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);

            //set the position of the player to be different
            this.gameObject.transform.localPosition = new Vector3(-5.76f, 0.22f, -1f);

            //set boolean HERE
            b_playerJumped = true;
        }

        if (player_choice == PlayerChoice.PLAYER_NONE)
        {
            //we want to shift the collider back to the original position
            collider_player.offset = v_originalcolliderposition;
            //set the player size and everything back to normal
            this.gameObject.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
            this.gameObject.transform.localPosition = new Vector3(-5.76f, -0.9f, -1f);
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

                    //add score HERE
                    uiManagerInstance.PlayerScores();

                    //add combo here
                    ComboManager.Instance.AddCombo();

                    Destroy(other.gameObject);
                    Destroy(other.gameObject.GetComponent<Rigidbody2D>());


                }
                else if (player_choice == PlayerChoice.PLAYER_PAPER
                    && other.gameObject.GetComponent<SPS_Enemy>().enemy_type == SPS_Enemy.EnemyType.ENEMY_STONE)
                {
                    //remove the enemy from the list
                    objectManagerInstance.objectWaveList.Remove(other.gameObject);

                    //add score HERE
                    uiManagerInstance.PlayerScores();

                    //add combo here
                    ComboManager.Instance.AddCombo();

                    Destroy(other.gameObject);
                    Destroy(other.gameObject.GetComponent<Rigidbody2D>());

                }
                else if (player_choice == PlayerChoice.PLAYER_STONE
                    && other.gameObject.GetComponent<SPS_Enemy>().enemy_type == SPS_Enemy.EnemyType.ENEMY_SCISSORS)
                {
                    //remove the enemy from the list
                    objectManagerInstance.objectWaveList.Remove(other.gameObject);

                    //add score HERE
                    uiManagerInstance.PlayerScores();

                    //add combo here
                    ComboManager.Instance.AddCombo();

                    Destroy(other.gameObject);
                    Destroy(other.gameObject.GetComponent<Rigidbody2D>());

                }
                //we take damage if we hit with the wrong typing
                else
                {
                    //player gets stunned
                    this.gameObject.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                    this.gameObject.transform.localPosition = new Vector3(-5.76f, -0.9f, -1f);

                    playerAC.SetBool("PlayerStunned", true);


                    ComboManager.Instance.BreakCombo();

                    //fade out the enemy so that it looks natural
                    other.gameObject.GetComponent<SpriteRenderer>().DOFade(0, 1f);
                    GameObject sprite_attackIndicator = 
                        other.gameObject.transform.Find("AttackIndicatorSprite").gameObject;
                    sprite_attackIndicator.GetComponent<SpriteRenderer>().DOFade(0, 1f);

                }
            }
        }

        //player is hit/stunned/collecting powerups HERE
        else
        {
            if (other.gameObject.tag == "EnemyTag")
            {
                this.gameObject.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                this.gameObject.transform.localPosition = new Vector3(-5.76f, -0.9f, -1f);

                //player gets stunned
                playerAC.SetBool("PlayerStunned", true);

                //do combo calculations HERE
                ComboManager.Instance.BreakCombo();

                //fade out the enemy so that it looks natural
                other.gameObject.GetComponent<SpriteRenderer>().DOFade(0, 1f);
                GameObject sprite_attackIndicator = 
                    other.gameObject.transform.Find("AttackIndicatorSprite").gameObject;
                sprite_attackIndicator.GetComponent<SpriteRenderer>().DOFade(0, 1f);

            }

            if (other.gameObject.tag == "Obstacle")
            {
                this.gameObject.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                this.gameObject.transform.localPosition = new Vector3(-5.76f, -0.9f, -1f);

                //player gets stunned
                playerAC.SetBool("PlayerStunned", true);

                //do combo calculations HERE
                ComboManager.Instance.BreakCombo();

                //fade out the enemy so that it looks natural
                other.GetComponent<SpriteRenderer>().DOFade(0, 1f);
            }

            if (other.gameObject.tag == "Powerup")
            {
                //add to the objective value
                uiManagerInstance.UpdatePlayerObjectiveValue();

                //remove the object from the list
                objectManagerInstance.objectWaveList.Remove(other.gameObject);

                //destroy it since it has been collected
                Destroy(other.gameObject);
                Destroy(other.gameObject.GetComponent<Rigidbody2D>());
            }

        }
    }

    #endregion

}
