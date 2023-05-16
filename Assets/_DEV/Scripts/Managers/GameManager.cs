using System.Collections;
using System.Collections.Generic;
using LastHand;
using UnityEngine;
using Button = UnityEngine.UI.Button;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject puzzlePrefab;
    [SerializeField] private GameObject puzzlePanel;  //TODO UIManager'e tasinacak

    [SerializeField] private Button puzzleBackButton;
    
    [SerializeField] private GameObject cranePrefab;
    [SerializeField] private GameObject craneCamera;
    [SerializeField] private GameObject cranePanel;  //TODO UIManager'e tasinacak

    [SerializeField] private Button craneBackButton;

    private void OnEnable()
    {
        Events.GamePlay.OnMinimapCollider += OnMinimapCollider;
        Events.GamePlay.OnPuzzleWin += OnPuzzleWin;
        Events.GamePlay.OnCraneCollider += OnCraneCollider;

        puzzleBackButton.onClick.AddListener(() =>
        {
            puzzlePrefab.SetActive(false);
            puzzlePanel.SetActive(false);
        });
        
        craneBackButton.onClick.AddListener(() =>
        {
            cranePrefab.SetActive(false);
            cranePanel.SetActive(false);
            craneCamera.SetActive(false);

            cranePrefab.GetComponent<CraneController>().enabled = false;
        });
    }
    
    private void OnDisable()
    {
        Events.GamePlay.OnMinimapCollider -= OnMinimapCollider;
        Events.GamePlay.OnPuzzleWin -= OnPuzzleWin;
        Events.GamePlay.OnCraneCollider -= OnCraneCollider;

        puzzleBackButton.onClick.RemoveAllListeners();
        craneBackButton.onClick.RemoveAllListeners();
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
    
    private void OnCraneCollider()
    {
        cranePrefab.SetActive(true);
        cranePanel.SetActive(true);
        craneCamera.SetActive(true);

        cranePrefab.GetComponent<CraneController>().enabled = true;
    }
}