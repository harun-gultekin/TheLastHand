using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PressMachineManager : MonoBehaviour
{
    [SerializeField] private GameObject moveMesh;

    private void Start()
    {
        SetAnimation();
    }

    private void SetAnimation()
    {
        moveMesh.transform.DOLocalMoveZ(0.26f, 1f).OnComplete(() =>
        {
            moveMesh.transform.DOLocalMoveZ(-0.4f, 1f).OnComplete(SetAnimation);
        });
    }
}