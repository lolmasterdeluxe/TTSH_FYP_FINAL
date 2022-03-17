using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNShoot : MonoBehaviour
{
    [SerializeField] private float power = 10f;
    [SerializeField] private Vector2 minPower, maxPower;
    [SerializeField] private Chapteh chapteh;
    [SerializeField] private Transform kickPoint;
    [SerializeField] private Trajectory TrajectoryLine;
    [SerializeField] private PlayerButtonMove playerMove;
    [SerializeField] private ChargeBar chargeBar;
    [SerializeField] private GameObject dPad;
    [SerializeField] private PauseMenu PauseManager;

    [HideInInspector] public Vector2 force;
    public AudioSource[] audioSources;

    private Camera cam;
    private Vector3 startPoint, endPoint, inputStartPoint, inputEndPoint, inputOffset;
    private bool InitStartPoint = true;
    private float kickBuffer;


    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (!playerMove.isRunning && !chapteh.inPlay && !PauseManager.isPaused)
            kickBuffer -= Time.deltaTime;
        else
            kickBuffer = 0.1f;

        if (kickBuffer <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                dPad.SetActive(false);
                startPoint = new Vector3(kickPoint.position.x, kickPoint.position.y, 15);
                if (InitStartPoint)
                {
                    inputStartPoint = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y + 1, Input.mousePosition.z));
                    InitStartPoint = false;
                }
            }

            if (Input.GetMouseButton(0))
            {
                inputEndPoint = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y + 1, Input.mousePosition.z));
                inputOffset = inputEndPoint - inputStartPoint;
                inputOffset = Vector3.ClampMagnitude(inputOffset, 4.0f);
                Vector3 currentPoint = (startPoint - inputOffset);
                currentPoint.z = 15;
                audioSources[0].Play();
                chargeBar.SetFillBar(inputOffset.magnitude);
                TrajectoryLine.RenderLine(startPoint, currentPoint);
            }

            if (Input.GetMouseButtonUp(0))
            {
                dPad.SetActive(true);
                endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                endPoint.z = 15;

                InitStartPoint = true;
                Debug.Log("Input offset" + inputOffset);
                force = inputOffset * power;
                Debug.Log("force " + force);
                force.Set(Mathf.Clamp(force.x, minPower.x, maxPower.x), Mathf.Clamp(force.y, minPower.y, maxPower.y));
                chapteh.Kick(-force);
                audioSources[1].Play();
                TrajectoryLine.EndLine();
            }
        }
        else if (!chapteh.inPlay)
        {
            dPad.SetActive(true);
            force.Set(0, 0);
            chargeBar.SetFillBar(4);
            InitStartPoint = true;
            TrajectoryLine.EndLine();
        }
    }
}
