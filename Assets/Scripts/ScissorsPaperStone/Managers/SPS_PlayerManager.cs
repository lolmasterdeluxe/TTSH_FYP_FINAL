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
    bool b_playerJumped, b_playerFall;
    float f_playerJumpLifetime;
    Vector3 v_finalJumpPosition;
    Vector3 v_playerOriginalPosition;

    //variables involving player attacking
    bool b_playerAttacked, b_playerMakesChoice;
    float f_playerAttackLifetime;

    //variables involving player stunned
    float f_playerStunnedLifetime;

    //variables for combo
    public GameObject g_comboGroup, g_comboText, g_comboText_finalPos;

    //variables for player idle character (endgame)
    public GameObject current_player_sprite, end_player_sprite;

    //Audio Source: for player-related Sounds
    public AudioSource jumpSFX, stunnedSFX, scissorsAtkSFX, paperAtkSFX, stoneAtkSFX, powerupSFX;


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
        b_playerFall = false;
        b_playerMakesChoice = false;

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
        if (b_playerJumped)
        {
            f_playerJumpLifetime += Time.deltaTime;

            //set the player's position higher
            gameObject.transform.DOMove(v_finalJumpPosition, 0.25f);

            if (f_playerJumpLifetime >= 0.75f)
            {
                //reset the boolean flag
                b_playerJumped = false;
                b_playerFall = true;
                f_playerJumpLifetime = 0;
                Debug.Log("Is player jumping: " + b_playerJumped);
            }

        }
        else if (b_playerFall)
        {
            f_playerJumpLifetime += Time.deltaTime;
            //reset the player's position back to the original
            gameObject.transform.DOMove(v_playerOriginalPosition, 0.25f);

            if (f_playerJumpLifetime >= 0.25f)
            {
                //reset the animations
                ResetAnimationsAndChoice();

                //reset collider position
                MoveBoxCollider();

                //reset the jump timer lifetime
                f_playerJumpLifetime = 0;

                b_playerFall = false;
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
            PlayerChoosesScissors();

        if (Input.GetKeyDown(KeyCode.S))
            PlayerChoosesPaper();

        if (Input.GetKeyDown(KeyCode.D))
            PlayerChoosesStone();

        if (Input.GetKeyDown(KeyCode.Space))
            PlayerJumps();

        #endregion
    }

    #endregion

    #region Player Functions

    public void PlayerChoosesScissors()
    {
        //set the enum
        player_choice = PlayerChoice.PLAYER_SCISSOR;
        g_PlayerActionSprite.transform.localPosition = new Vector3(0.5f, -0.25f, 0f);
        g_PlayerActionSprite.transform.localScale = new Vector3(2f, 2f, 2f);
        //set the booleans on ACs
        playerAC.SetBool("PlayerAttackingWithScissors", true);
        playeractionAC.SetBool("PlayerActionWithScissors", true);
        MoveBoxCollider();
        //play sounds HERE
        scissorsAtkSFX.Play();
        b_playerMakesChoice = true;
    }

    public void PlayerChoosesPaper()
    {
        //set the enum
        player_choice = PlayerChoice.PLAYER_PAPER;
        g_PlayerActionSprite.transform.localPosition = new Vector3(1.5f, 0.75f, 0f);
        g_PlayerActionSprite.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        //set the booleans on ACs
        playerAC.SetBool("PlayerAttackingWithPaper", true);
        playeractionAC.SetBool("PlayerActionWithPaper", true);
        MoveBoxCollider();
        //play sounds HERE
        paperAtkSFX.Play();
        b_playerMakesChoice = true;
    }

    public void PlayerChoosesStone()
    {
        //set the enum
        player_choice = PlayerChoice.PLAYER_STONE;
        g_PlayerActionSprite.transform.localPosition = new Vector3(0.03f, 0f, 0f);
        g_PlayerActionSprite.transform.localScale = new Vector3(1.75f, 1.75f, 1.75f);
        //set the booleans on ACs
        playerAC.SetBool("PlayerAttackingWithStone", true);
        playeractionAC.SetBool("PlayerActionWithStone", true);
        MoveBoxCollider();
        //play sounds HERE
        stoneAtkSFX.Play();
        b_playerMakesChoice = true;
    }

    public void PlayerJumps()
    {
        player_choice = PlayerChoice.PLAYER_JUMP;
        playerAC.SetBool("PlayerJumped", true);
        MoveBoxCollider();
        //play sounds HERE
        jumpSFX.Play();
        b_playerMakesChoice = true;
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

        if (player_choice == PlayerChoice.PLAYER_JUMP && !b_playerFall)
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

    private void OnTriggerStay2D(Collider2D other)
    {
        //player is attacking HERE
        if (b_playerMakesChoice)
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

                    //Destroy(other.gameObject);
                    //Destroy(other.gameObject.GetComponent<Rigidbody2D>());
                    other.gameObject.transform.DOKill(true);
                    other.gameObject.SetActive(false);
                    Debug.Log("Paper Beaten");

                    #region Unused

                    //////call the coroutine HERE
                    ////StartCoroutine(objectManagerInstance.EndsEnemy(other.GetComponent<Animator>(), other.gameObject));

                    ////handle enemy anims HERE
                    //Tween myTween = other.gameObject.GetComponent<SpriteRenderer>().DOFade(0f, 0.25f);
                    //bool isComplete = myTween.IsComplete();
                    //if (isComplete == true)
                    //{
                    //    other.gameObject.GetComponent<Animator>().SetBool("e_died", true);
                    //}

                    ////handle enemy killing HERE
                    //if (other.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("enemy_paper_defeated"))
                    //{
                    //    Destroy(other.gameObject);
                    //    Destroy(other.gameObject.GetComponent<Rigidbody2D>());
                    //}

                    #endregion
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


                    //Destroy(other.gameObject);
                    //Destroy(other.gameObject.GetComponent<Rigidbody2D>());
                    other.gameObject.transform.DOKill(true);
                    other.gameObject.SetActive(false);
                    Debug.Log("Rock Beaten");

                    #region Unused
                    ////call the coroutine HERE
                    //StartCoroutine(objectManagerInstance.EndsEnemy(other.GetComponent<Animator>(), other.gameObject));

                    //handle enemy anims HERE
                    //Tween myTween = other.gameObject.GetComponent<SpriteRenderer>().DOFade(0f, 0.25f);
                    //bool isComplete = myTween.IsComplete();
                    //if (isComplete == true)
                    //{
                    //    other.gameObject.GetComponent<Animator>().SetBool("e_died", true);
                    //}
                    //
                    ////handle enemy killing HERE
                    //if (other.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName//("enemy_stone_defeated"))
                    //{
                    //    Destroy(other.gameObject);
                    //    Destroy(other.gameObject.GetComponent<Rigidbody2D>());
                    //}
                    #endregion

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

                    //Destroy(other.gameObject);
                    //Destroy(other.gameObject.GetComponent<Rigidbody2D>());
                    other.gameObject.transform.DOKill(true);
                    other.gameObject.SetActive(false);
                    Debug.Log("Scissors Beaten");

                    #region Unused

                    ////call the coroutine HERE
                    //StartCoroutine(objectManagerInstance.EndsEnemy(other.GetComponent<Animator>(), other.gameObject));

                    //handle enemy anims HERE
                    //Tween myTween = other.gameObject.GetComponent<SpriteRenderer>().DOFade(0f, 0.25f);
                    //bool isComplete = myTween.IsComplete();
                    //if (isComplete == true)
                    //{
                    //    other.gameObject.GetComponent<Animator>().SetBool("e_died", true);
                    //}
                    //
                    ////handle enemy killing HERE
                    //if (other.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName//("enemy_scissors_defeated"))
                    //{
                    //    Destroy(other.gameObject);
                    //    Destroy(other.gameObject.GetComponent<Rigidbody2D>());
                    //}
                    //
                    #endregion

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
                    //Destroy(other.gameObject);
                    //Destroy(other.gameObject.GetComponent<Rigidbody2D>());
                    other.gameObject.SetActive(false);
                    
                    Debug.Log("Powerup Falsed");
                    //play sounds HERE
                    powerupSFX.Play();
                    

                }
                //we take damage if we hit with the wrong typing
                else
                {
                    //player gets stunned

                    playerAC.SetBool("PlayerStunned", true);

                    ComboManager.Instance.BreakCombo();

                    //fade out the enemy so that it looks natural
                    other.gameObject.GetComponent<SpriteRenderer>().DOFade(0, 1f);
                    other.transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(0, 1f);

                    //play sounds HERE
                    stunnedSFX.Play();
                }
            }
            b_playerMakesChoice = false;
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
                other.transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(0, 1f);

                //play sounds HERE
                stunnedSFX.Play();

            }

            else if (other.gameObject.tag == "Obstacle")
            {
                //player gets stunned
                playerAC.SetBool("PlayerStunned", true);

                //do combo calculations HERE
                ComboManager.Instance.BreakCombo();

                //fade out the enemy so that it looks natural
                other.gameObject.GetComponent<SpriteRenderer>().DOFade(0, 1f);

                //play sounds HERE
                stunnedSFX.Play();

            }

            else if (other.gameObject.tag == "Powerup")
            {
                print("enter");
                //add to the objective value
                uiManagerInstance.AddObjectiveValue();

                //remove the object from the list
                objectManagerInstance.objectWaveList.Remove(other.gameObject);


                //destroy it since it has been collected
                //Destroy(other.gameObject);
                //Destroy(other.gameObject.GetComponent<Rigidbody2D>());
                other.gameObject.SetActive(false);
                transform.GetChild(1).GetComponent<ParticleSystem>().time = 0;
                transform.GetChild(1).GetComponent<ParticleSystem>().Play();

                //play sounds HERE
                powerupSFX.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Powerup")
        {
            Destroy(other.gameObject);
            //transform.GetChild(1).GetComponent<ParticleSystem>().Simulate(0,false,true);;
            transform.GetChild(1).GetComponent<ParticleSystem>().Stop();
            Debug.Log("I am the Elden Lord");
        }
            
    }

    #endregion



}
