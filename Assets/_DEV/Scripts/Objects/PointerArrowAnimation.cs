using UnityEngine;
using DG.Tweening;

public class PointerArrowAnimation : MonoBehaviour
{
    [SerializeField] private GameObject arrowMesh;

    private void Start()
    {
        SetAnimation();
    }

    private void SetAnimation()
    {
        arrowMesh.transform.DOLocalMoveZ(0.26f, 1f).OnComplete(() =>
        {
            arrowMesh.transform.DOLocalMoveZ(-0.4f, 1f).OnComplete(SetAnimation);
        });
    }
}