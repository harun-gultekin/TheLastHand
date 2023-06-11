using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PressMachineManager : MonoBehaviour
{
    public static PressMachineManager Instance;
    
    [SerializeField] private GameObject moveMesh;
    [SerializeField] private float startPoint = 2.527f;
    [SerializeField] private float endPoint = 0.167f;
    [SerializeField] private float slowness = 3f;
    private bool animationStarted;
    [SerializeField] public bool triggerActive;

    private void Awake()
    {
        Instance = this;
    }
    
    private void Update()
    {
        if (!animationStarted && triggerActive)
        {
            SetAnimation();
            animationStarted = true;
        }
    }

    private void SetAnimation()
    {
        moveMesh.transform.DOLocalMoveY(startPoint, slowness).OnComplete(() =>
        {
            moveMesh.transform.DOLocalMoveY(endPoint, slowness).OnComplete(SetAnimation);
        });
    }
}