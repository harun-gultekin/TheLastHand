using LastHand;
using UnityEngine;

public class LevelStateManager : MonoBehaviour
{
    public static LevelStateManager Instance;
    public LevelState currentState;
    
    private void Awake()
    {
        Instance = this;
    }

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
        currentState = LevelState.Started;
    }
    
    private void OnPuzzleWin()
    {
        currentState = LevelState.MinimapTaken;
    }
}

public enum LevelState 
{
    Started,
    MinimapTaken,
}