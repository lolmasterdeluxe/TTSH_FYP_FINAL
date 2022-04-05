using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public float backgroundAnimSpeed = 1f;
    private float InitialAnimSpeed;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        InitialAnimSpeed = backgroundAnimSpeed;
    }

    private void Update()
    {
        if (!FlappyGameManager.Instance.m_gameStarted || FlappyGameManager.Instance.m_gameEnded)
            return;
        backgroundAnimSpeed = InitialAnimSpeed + (FlappyGameManager.Instance.SpeedMultiplier / 100);
        meshRenderer.material.mainTextureOffset += new Vector2(backgroundAnimSpeed * Time.deltaTime, 0);
    }
}
