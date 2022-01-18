using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class SPS_PlayerManager : MonoBehaviour
{
    //this manager handles EVERYTHING related to the player

    private static SPS_PlayerManager _instance;
    public static SPS_PlayerManager Instance { get { return _instance; } }

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
    Vector2 v_originalcolliderOffset;
    bool b_playerJumped;
    float f_playerJumpLifetime;
    Vector3 v_finalJumpPosition;
    Vector3 v_playerOriginalPosition;

    //variables involving player attacking
    bool b_playerAttacked;
    float f_playerAttackLifetime;

    //variables involving player stunned
    float f_playerStunnedLifetime;

    //variables for combo
    public GameObject g_comboGroup, g_comboText, g_comboText_finalPos;

    //variables for player idle character (endgame)
    public GameObject current_player_sprite, end_player_sprite;

    //variables for data HERE
    public int enemyCount;
    public int sweetCount;

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
        b_playerJumped = false;

        //set collider box reference HERE
        collider_player = GetComponent<BoxCollider2D>();

        //set original collider position HERE
        v_originalcolliderOffset = new Vector2(0f, 0f);

        //set jump position HERE
        v_finalJumpPosition = new Vector3(-5.76f, 1.7f, -1f);

        //set original player sprite position HERE
        v_playerOriginalPosition = new Vector3(-5.76f, -0.7f, -1f);

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

            //set the player's position higher
            this.gameObject.transform.DOMove(v_finalJumpPosition, 0.4f);

            if (f_playerJumpLifetime >= 0.6f)
            {
                //reset the player's position back to the original
                this.gameObject.transform.DOMove(v_playerOriginalPosition, 0.4f);

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

            if (f_playerStunnedLifetime >= 0.9f)
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
            g_PlayerActionSprite.transform.localPosition = new Vector3(0.5f, -0.25f, 0f);
            g_PlayerActionSprite.transform.localScale = new Vector3(2f, 2f, 2f);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayerChoosesPaper();
            g_PlayerActionSprite.transform.localPosition = new Vector3(1.5f, 0.75f, 0f);
            g_PlayerActionSprite.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayerChoosesStone();
            g_PlayerActionSprite.transform.localPosition = new Vector3(0.03f, 0f, 0f);
            g_PlayerActionSprite.transform.localScale = new Vector3(1.75f, 1.75f, 1.75f);
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
            collider_player.offset = collider_player.offset = new Vector2(2.75f, 0f);
            //set boolean HERE
            b_playerAttacked = true;
        }

        if (player_choice == PlayerChoice.PLAYER_JUMP)
        {
            //we want to shift the collider box UP
            collider_player.offset = collider_player.offset = new Vector2(0f, 1f);

            //set boolean HERE
            b_playerJumped = true;
        }

        if (player_choice == PlayerChoice.PLAYER_NONE)
        {
            //we want to shift the collider back to the original position
            collider_player.offset = v_originalcolliderOffset;
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
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.Space)))
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

                    enemyCount += 1;
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

                    enemyCount += 1;

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

                    enemyCount += 1;

                }
                else if (other.gameObject.tag == "Powerup")
                {
                    //add to the objective value
                    uiManagerInstance.UpdatePlayerObjectiveValue();

                    //remove the object from the list
                    objectManagerInstance.objectWaveList.Remove(other.gameObject);

                    //add the value HERE
                    uiManagerInstance.AddObjectiveValue();

                    //destroy it since it has been collected
                    Destroy(other.gameObject);
                    Destroy(other.gameObject.GetComponent<Rigidbody2D>());

                    sweetCount += 1;
                }
                //we take damage if we hit with the wrong typing
                else
                {
                    //player gets stunned

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

            else if (other.gameObject.tag == "Obstacle")
            {
                //player gets stunned
                playerAC.SetBool("PlayerStunned", true);

                //do combo calculations HERE
                ComboManager.Instance.BreakCombo();

                //fade out the enemy so that it looks natural
                other.gameObject.GetComponent<SpriteRenderer>().DOFade(0, 1f);

            }

            else if (other.gameObject.tag == "Powerup")
            {
                //add to the objective value
                uiManagerInstance.AddObjectiveValue();

                //remove the object from the list
                objectManagerInstance.objectWaveList.Remove(other.gameObject);

                //destroy it since it has been collected
                Destroy(other.gameObject);
                Destroy(other.gameObject.GetComponent<Rigidbody2D>());

                sweetCount += 1;
            }
        }
    }

    #endregion

}
