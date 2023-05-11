using System.Collections;
using System.Collections.Generic;
using LastHand;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject puzzlePrefab;
    
    private void OnEnable()
    {
        Events.GamePlay.OnMinimapCollider += OnMinimapCollider;
        Events.GamePlay.OnPuzzleWin += OnPuzzleWin;
    }
    
    private void OnDisable()
    {
        Events.GamePlay.OnMinimapCollider -= OnMinimapCollider;
        Events.GamePlay.OnPuzzleWin -= OnPuzzleWin;
    }
    
    private void OnMinimapCollider()
    {
        puzzlePrefab.SetActive(true);
    }
    
    private void OnPuzzleWin()
    {
        puzzlePrefab.SetActive(false);
    }
}