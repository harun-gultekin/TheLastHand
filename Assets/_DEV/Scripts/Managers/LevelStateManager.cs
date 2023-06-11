using LastHand;
using UnityEngine;

public class LevelStateManager : MonoBehaviour
{
    public static LevelStateManager Instance;
    public LevelState currentState;

    public bool isCraneActive;
    public bool isMinimapPuzzleActive;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        Events.Menu.StartGameButton += StartGameButton;
        Events.GamePlay.OnMinimapCollider += OnMinimapCollider;
        Events.GamePlay.OnPuzzleWin += OnPuzzleWin;
        Events.GamePlay.OnCraneCollider += OnCraneCollider;
        Events.UIGamePlay.OnCraneClose += OnCraneClose;
    }
    
    private void OnDisable()
    {
        Events.Menu.StartGameButton -= StartGameButton;
        Events.GamePlay.OnMinimapCollider -= OnMinimapCollider;
        Events.GamePlay.OnPuzzleWin -= OnPuzzleWin;
        Events.GamePlay.OnCraneCollider -= OnCraneCollider;
        Events.UIGamePlay.OnCraneClose -= OnCraneClose;
    }
    
    private void StartGameButton()
    {
        currentState = LevelState.Started;
    }
    
    private void OnMinimapCollider()
    {
        isMinimapPuzzleActive = true;
    }
    
    private void OnPuzzleWin()
    {
        currentState = LevelState.MinimapTaken;
        isMinimapPuzzleActive = false;
    }
    
    private void OnCraneCollider()
    {
        isCraneActive = true;
    }
    
    private void OnCraneClose()
    {
        isCraneActive = false;
    }
}

public enum LevelState 
{
    OnMenu,
    Started,
    MinimapTaken,
    SteamDischarged,
    DoorControlled,
    PressMachine,
    CraneControl
}