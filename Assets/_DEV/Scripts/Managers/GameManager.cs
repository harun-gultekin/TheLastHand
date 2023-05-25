using System.Collections;
using System.Collections.Generic;
using LastHand;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject puzzlePrefab;
    [SerializeField] private GameObject cranePrefab;

    private void OnEnable()
    {
        Events.GamePlay.OnMinimapCollider += OnMinimapCollider;
        Events.GamePlay.OnPuzzleWin += OnPuzzleWin;
        Events.GamePlay.OnCraneCollider += OnCraneCollider;
        Events.UIGamePlay.OnPuzzleClose += OnPuzzleClose;
        Events.UIGamePlay.OnCraneClose += OnCraneClose;
    }
    
    private void OnDisable()
    {
        Events.GamePlay.OnMinimapCollider -= OnMinimapCollider;
        Events.GamePlay.OnPuzzleWin -= OnPuzzleWin;
        Events.GamePlay.OnCraneCollider -= OnCraneCollider;
        Events.UIGamePlay.OnPuzzleClose -= OnPuzzleClose;
        Events.UIGamePlay.OnCraneClose -= OnCraneClose;
    }
    
    private void OnMinimapCollider()
    {
        puzzlePrefab.SetActive(true);
    }
    
    private void OnPuzzleWin()
    {
        puzzlePrefab.SetActive(false);
    }
    
    private void OnPuzzleClose()
    {
        puzzlePrefab.SetActive(false);
    }
    
    private void OnCraneCollider()
    {
        cranePrefab.SetActive(true);
    }
    
    private void OnCraneClose()
    {
        cranePrefab.SetActive(false);
    }
}