using System.Collections;
using System.Collections.Generic;
using LastHand;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public CanvasGroup minimapPanel;
    public CanvasGroup alertPanel;

    [SerializeField] private Text alertText;
    
    private void OnEnable()
    {
        Events.GamePlay.OnPuzzleWin += OnPuzzleWin;
    }
    
    private void OnDisable()
    {
        Events.GamePlay.OnPuzzleWin -= OnPuzzleWin;
    }
    
    private void OnPuzzleWin()
    {
        ActivatePanel(minimapPanel);
        ActivatePanel(alertPanel);
        alertText.text = AlertUITexts.HIDE_AGENTS;
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