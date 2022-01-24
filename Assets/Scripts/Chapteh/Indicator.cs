﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    #region CodeMonkey
    //[SerializeField] private Camera uiCamera;
    //[SerializeField] private Sprite arrowSprite;
    //[SerializeField] private Sprite crossSprite;

    //private Chapteh chapteh;
    //private Vector3 targetPosition;
    //private RectTransform pointerRectTransform;
    //private Image pointerImage;
    #endregion

    public GameObject Pointer;
    public GameObject Target;

    Renderer rd;

    // Start is called before the first frame update
    void Start()
    {
        //chapteh = GameObject.Find("Chapteh").GetComponent<Chapteh>();

        rd = GetComponent<Renderer>();
    }

    private void Awake()
    {
        #region CodeMonkey
        //pointerRectTransform = transform.Find("Indicator").GetComponent<RectTransform>();
        //pointerImage = transform.Find("Indicator").GetComponent<Image>();

        //Hide();
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        #region CodeMonkey
        //float borderSize = 100f;
        //Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        //bool isOffScreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= Screen.width - borderSize ||
        //                   targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize;

        //if(isOffScreen)
        //{
        //    RotatePointerTowardsTargetPosition();

        //    pointerImage.sprite = arrowSprite;
        //    Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
        //    if (cappedTargetScreenPosition.x <= borderSize)
        //        cappedTargetScreenPosition.x = borderSize;
        //    if (cappedTargetScreenPosition.x >= Screen.width - borderSize)
        //        cappedTargetScreenPosition.x = Screen.width - borderSize;
        //    if (cappedTargetScreenPosition.y <= borderSize)
        //        cappedTargetScreenPosition.y = borderSize;
        //    if (cappedTargetScreenPosition.y >= Screen.height - borderSize)
        //        cappedTargetScreenPosition.y = Screen.height - borderSize;

        //    Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
        //    pointerRectTransform.position = pointerWorldPosition;
        //    pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
        //}
        //else
        //{
        //    pointerImage.sprite = crossSprite;
        //    Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(targetPositionScreenPoint);
        //    pointerRectTransform.position = pointerWorldPosition;
        //    pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);

        //    pointerRectTransform.localEulerAngles = Vector3.zero;
        //}
        #endregion

        if (rd.isVisible == false)
        {
            if(Pointer.activeSelf == false)
            {
                Pointer.SetActive(true);
            }

            Vector2 direction = Target.transform.position - transform.position;

            RaycastHit2D ray = Physics2D.Raycast(transform.position, direction);

            if(ray.collider != null)
            {
                Pointer.transform.position = ray.point;
            }
        }
        else
        {
            if (Pointer.activeSelf == true)
            {
                Pointer.SetActive(false);
            }
        }
    }

    #region CodeMonkey
    //private void RotatePointerTowardsTargetPosition()
    //{
    //    Vector3 toPosition = targetPosition;
    //    Vector3 fromPosition = Camera.main.transform.position;
    //    fromPosition.z = 0f;

    //    Vector2 dir = (toPosition - fromPosition).normalized;
    //    float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) % 360;
    //    pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);
    //}

    //public void Hide()
    //{
    //    gameObject.SetActive(false);
    //}

    //public void Show(Vector3 targetPosition)
    //{
    //    gameObject.SetActive(true);
    //    this.targetPosition = targetPosition;
    //}
    #endregion
}
