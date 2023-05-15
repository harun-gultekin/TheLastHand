using System.Collections;
using System.Collections.Generic;
using LastHand;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject puzzlePrefab;
    [SerializeField] private GameObject puzzlePanel;  //TODO UIManager'e tasinacak

    [SerializeField] private Button puzzleBackButton;

    private void OnEnable()
    {
        Events.GamePlay.OnMinimapCollider += OnMinimapCollider;
        Events.GamePlay.OnPuzzleWin += OnPuzzleWin;

        puzzleBackButton.onClick.AddListener(() =>
        {
            puzzlePrefab.SetActive(false);
            puzzlePanel.SetActive(false);
        });
    }
    
    private void OnDisable()
    {
        Events.GamePlay.OnMinimapCollider -= OnMinimapCollider;
        Events.GamePlay.OnPuzzleWin -= OnPuzzleWin;
        
        puzzleBackButton.onClick.RemoveAllListeners();
    }
    
    private void OnMinimapCollider()
    {
        puzzlePrefab.SetActive(true);
        puzzlePanel.SetActive(true);
    }
    
    private void OnPuzzleWin()
    {
        puzzlePrefab.SetActive(false);
        puzzlePanel.SetActive(false);
    }
}