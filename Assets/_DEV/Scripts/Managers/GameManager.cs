using System.Collections;
using System.Collections.Generic;
using LastHand;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject puzzlePrefab;
    [SerializeField] private GameObject cranePrefab;
    [SerializeField] private GameObject craneCamera;
    //[SerializeField] private GameObject craneLimits;
    
    private void OnEnable()
    {
        Events.GamePlay.OnMinimapCollider += OnMinimapCollider;
        Events.GamePlay.OnPuzzleWin += OnPuzzleWin;
        Events.GamePlay.OnCraneCollider += OnCraneCollider;
        Events.UIGamePlay.OnPuzzleClose += OnPuzzleClose;
    }
    
    private void OnDisable()
    {
        Events.GamePlay.OnMinimapCollider -= OnMinimapCollider;
        Events.GamePlay.OnPuzzleWin -= OnPuzzleWin;
        Events.GamePlay.OnCraneCollider -= OnCraneCollider;
        Events.UIGamePlay.OnPuzzleClose -= OnPuzzleClose;
    }
    
    private void OnMinimapCollider()
    {
        puzzlePrefab.SetActive(true);
    }
    
    private void OnPuzzleWin()
    {
        puzzlePrefab.SetActive(false);
    }
    
    private void OnCraneCollider()
    {
        //craneLimits.SetActive(true);
        craneCamera.SetActive(true);
        cranePrefab.SetActive(true);
        cranePrefab.GetComponent<CraneController>().enabled = true;
    }
    
    private void OnPuzzleClose()
    {
        puzzlePrefab.SetActive(false);
    }
}