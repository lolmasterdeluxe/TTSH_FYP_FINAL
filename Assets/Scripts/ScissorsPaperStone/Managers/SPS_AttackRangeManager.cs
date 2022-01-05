using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SPS_AttackRangeManager : MonoBehaviour
{
    //manager to handle attack range UI popup

    #region Unity Callbacks

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnemyTag")
        {
            //we now set the attack indicator of the enemy to be true
            GameObject sprite_attackIndicator = other.gameObject.transform.Find("AttackIndicatorSprite").gameObject;
            sprite_attackIndicator.GetComponent<SpriteRenderer>().DOFade(1, 0.5f);
        }
    }

    #endregion


}
