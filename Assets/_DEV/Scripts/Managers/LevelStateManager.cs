using LastHand;
using UnityEngine;

public class LevelStateManager : MonoBehaviour
{
    public LevelState currentState;
    
    private void OnEnable()
    {
        Events.Menu.StartGameButton += StartGameButton;
        
        Events.GamePlay.OnPuzzleWin += OnPuzzleWin;
    }
    
    private void OnDisable()
    {
        Events.Menu.StartGameButton -= StartGameButton;

        Events.GamePlay.OnPuzzleWin -= OnPuzzleWin;
    }
    
    private void StartGameButton()
    {
        currentState = LevelState.Start;
    }
    
    private void OnPuzzleWin()
    {
        currentState = LevelState.GetMinimap;
    }
}

public enum LevelState 
{
    Start,
    GetMinimap,
    HideAgents,
    TurnValve
}