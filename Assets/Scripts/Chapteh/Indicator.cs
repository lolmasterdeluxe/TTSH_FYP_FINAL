using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    public GameObject chapteh;
    [SerializeField] Camera cam;
    private RectTransform pointerRectTransform;

    private void Awake()
    {
        pointerRectTransform = transform.GetChild(0).GetComponent<RectTransform>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!ChaptehGameManager.Instance.m_gameStarted)
            return;
        else
        {
            if(!chapteh.GetComponent<SpriteRenderer>().isVisible)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                
                Vector3 screenCenter = new Vector3(Screen.width, Screen.height, 0) / 2;
                
                Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(chapteh.transform.position);
                
                Vector3 dir = (targetPositionScreenPoint - screenCenter).normalized;
                
                float angle = Mathf.Atan2(dir.y, dir.x);
                
                //transform.position = screenCenter + new Vector3(Mathf.Cos(angle) * screenCenter.x * 0.95f,Mathf.Sin(angle) * screenCenter.y * 0.9f, 0);
                
                transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);

                Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
                if (cappedTargetScreenPosition.x <= 0)
                {
                    cappedTargetScreenPosition.x = 0;
                    print("Left");
                }
                if (cappedTargetScreenPosition.x >= Screen.width)
                {
                    cappedTargetScreenPosition.x = Screen.width;
                    print("Right");
                }
                if (cappedTargetScreenPosition.y <= 0)
                { 
                    cappedTargetScreenPosition.y = 0;
                    print("Bottom");
                }
                if (cappedTargetScreenPosition.y >= Screen.height)
                {
                    cappedTargetScreenPosition.y = Screen.height;
                    print("Top");
                }

                pointerRectTransform.position = cappedTargetScreenPosition;
                //Vector3 pointerWorldPosition = cam.ScreenToWorldPoint(cappedTargetScreenPosition);
                //transform.localPosition = new Vector3(transform.localPosition.x,transform.localPosition.y,0f);
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
