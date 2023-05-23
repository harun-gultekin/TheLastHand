using System.Collections;
using System.Collections.Generic;
using LastHand;
using UnityEngine;
using UnityEngine.UI;
using Event = LastHand.Event;

public class UIManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup minimapPanel;
    
    [SerializeField] private CanvasGroup alertPanel;
    [SerializeField] private Text alertText;

    [SerializeField] private CanvasGroup puzzlePanel;
    [SerializeField] private Button puzzleBackButton;
    
    [SerializeField] private CanvasGroup cranePanel;
    [SerializeField] private Button craneBackButton;
    
    private void OnEnable()
    {
        Events.GamePlay.OnPuzzleWin += OnPuzzleWin;
        Events.GamePlay.OnMinimapCollider += OnMinimapCollider;
        Events.GamePlay.OnCraneCollider += OnCraneCollider;
        
        puzzleBackButton.onClick.AddListener(() =>
        {
            Events.UIGamePlay.OnPuzzleClose.Call();
            DeActivatePanel(puzzlePanel);
        });
        
        craneBackButton.onClick.AddListener(() =>
        {
            DeActivatePanel(cranePanel);
        });
    }
    
    private void OnDisable()
    {
        Events.GamePlay.OnPuzzleWin -= OnPuzzleWin;
        Events.GamePlay.OnMinimapCollider -= OnMinimapCollider;
        Events.GamePlay.OnCraneCollider -= OnCraneCollider;
        
        puzzleBackButton.onClick.RemoveAllListeners();
        craneBackButton.onClick.RemoveAllListeners();
    }
    
    private void OnPuzzleWin()
    {
        DeActivatePanel(puzzlePanel);
        ActivatePanel(minimapPanel);
        ActivatePanel(alertPanel);
        alertText.text = AlertUITexts.HIDE_AGENTS;
    }
    
    private void OnMinimapCollider()
    {
        ActivatePanel(puzzlePanel);
    }

    private void OnCraneCollider()
    {
        ActivatePanel(cranePanel);
    }
    
    private void ActivatePanel(CanvasGroup panel)
    {
        panel.alpha = 1;
        panel.interactable = true;
        panel.blocksRaycasts = true;
    }
    
    private void DeActivatePanel(CanvasGroup panel)
    {
        panel.alpha = 0;
        panel.interactable = false;
        panel.blocksRaycasts = false;
    }
}