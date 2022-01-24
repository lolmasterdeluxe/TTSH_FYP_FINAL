using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    #region CodeMonkey
    //[SerializeField] private Camera uiCamera;
    //[SerializeField] private Sprite arrowSprite;
    //[SerializeField] private Sprite starSprite;

    //private Chapteh chapteh;
    //private Vector3 targetPosition;
    //private RectTransform pointerRectTransform;
    //private Image pointerImage;

    //Vector3 targetPositionScreenPoint;
    //float borderSize;
    //bool isOffScreen;
    #endregion

    #region So Called Working
    public GameObject Pointer;
    public GameObject Target;

    Renderer rd;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //chapteh = GameObject.Find("Chapteh").GetComponent<Chapteh>();

        //float borderSize = 20f;
        //targetPositionScreenPoint = Camera.main.WorldToScreenPoint(chapteh.transform.position);
        //isOffScreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= Screen.width - borderSize ||
        //              targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize;

        rd = GetComponent<Renderer>();
    }

    private void Awake()
    {
        #region CodeMonkey
        //chapteh.transform.position = GameObject.Find("Chapteh").transform.position;
        //pointerRectTransform = transform.Find("Pointer1").GetComponent<RectTransform>();
        //pointerImage = transform.Find("Pointer1").GetComponent<Image>();

        //Hide();
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        #region CodeMonkey

        //if (isOffScreen)
        //{
        //    RotatePointerTowardsTargetPosition();

        //    pointerImage.sprite = arrowSprite;
        //    Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
        //    cappedTargetScreenPosition.x = Mathf.Clamp(cappedTargetScreenPosition.x, borderSize, Screen.width - borderSize);
        //    cappedTargetScreenPosition.y = Mathf.Clamp(cappedTargetScreenPosition.y, borderSize, Screen.height - borderSize);

        //    Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
        //    pointerRectTransform.position = pointerWorldPosition;
        //    pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
        //}
        //else
        //{
        //    pointerImage.sprite = starSprite;
        //    Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(targetPositionScreenPoint);
        //    pointerRectTransform.position = pointerWorldPosition;
        //    pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);

        //    pointerRectTransform.localEulerAngles = Vector3.zero;
        //}
        #endregion

        #region So Called Working
        if (rd.isVisible == false)
        {
            if (Pointer.activeSelf == false)
            {
                Pointer.SetActive(true);
            }

            Vector2 direction = Target.transform.position - transform.position;

            RaycastHit2D ray = Physics2D.Raycast(transform.position, direction);

            if (ray.collider != null)
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
        #endregion
    }

    #region CodeMonkey
    //private void RotatePointerTowardsTargetPosition()
    //{
    //    Vector3 toPosition = chapteh.transform.position;
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
    //    chapteh.transform.position = targetPosition;
    //}
    #endregion
}
