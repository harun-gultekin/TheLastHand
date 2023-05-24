using System.Collections;
using System.Collections.Generic;
using LastHand;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup menuPanel;
    [SerializeField] private CanvasGroup mainPanel;
    [SerializeField] private CanvasGroup minimapPanel;
    [SerializeField] private CanvasGroup alertPanel;
    [SerializeField] private CanvasGroup puzzlePanel;
    [SerializeField] private CanvasGroup cranePanel;
    
    [SerializeField] private Text alertText;
    
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button puzzleBackButton;
    [SerializeField] private Button craneBackButton;
    
    private void OnEnable()
    {
        Events.GamePlay.OnPuzzleWin += OnPuzzleWin;
        Events.GamePlay.OnMinimapCollider += OnMinimapCollider;
        Events.GamePlay.OnCraneCollider += OnCraneCollider;
        
        startGameButton.onClick.AddListener(() =>
        {
            Events.Menu.StartGameButton.Call();
            DeActivatePanel(menuPanel);
            ActivatePanel(mainPanel);
        });
        
        puzzleBackButton.onClick.AddListener(() =>
        {
            Events.UIGamePlay.OnPuzzleClose.Call();
            DeActivatePanel(puzzlePanel);
            
            LevelStateManager.Instance.isMinimapPuzzleActive = false;
        });
        
        craneBackButton.onClick.AddListener(() =>
        {
            Events.UIGamePlay.OnCraneClose.Call();

            DeActivatePanel(cranePanel);
            ActivatePanel(mainPanel);

            LevelStateManager.Instance.isCraneActive = false;
        });
    }
    
    private void OnDisable()
    {
        Events.GamePlay.OnPuzzleWin -= OnPuzzleWin;
        Events.GamePlay.OnMinimapCollider -= OnMinimapCollider;
        Events.GamePlay.OnCraneCollider -= OnCraneCollider;
        
        startGameButton.onClick.RemoveAllListeners();
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
        DeActivatePanel(mainPanel);
    }

    private void OnCraneCollider()
    {
        ActivatePanel(cranePanel);
        DeActivatePanel(mainPanel);
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