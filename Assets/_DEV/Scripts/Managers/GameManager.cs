using System.Collections;
using System.Collections.Generic;
using LastHand;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject puzzlePrefab;
    [SerializeField] private GameObject cranePrefab;

    //Minimap alana kadar countdown yok
    //countdown icerideki pc (door control panel) ile baslar 
    //Puzzle cozunce kapilar blocklanir, agents kapıyı acamaz 
    //O arada ventilatordan kacar countdown baslar
    
    //Sis icin de countdown olabilir sis kalma suresi olur
    //Agent dolanirken sis basilir 

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
        cranePrefab.SetActive(true);
    }
    
    private void OnPuzzleClose()
    {
        puzzlePrefab.SetActive(false);
    }
}