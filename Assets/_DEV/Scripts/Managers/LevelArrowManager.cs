using LastHand;
using UnityEngine;

public class LevelArrowManager : MonoBehaviour
{
    [SerializeField] private GameObject takeMinimapArrow;
    [SerializeField] private GameObject steamArrow;
    [SerializeField] private GameObject doorControlArrow;
    [SerializeField] private GameObject turnPressMachineArrow;
    [SerializeField] private GameObject craneControlArrow;

    private void OnEnable()
    {
        Events.GamePlay.OnPuzzleWin += OnPuzzleWin;
        Events.GamePlay.OnSteamDischarged += OnSteamDischarged;
        Events.GamePlay.OnDoorControlled += OnDoorControlled;
        Events.GamePlay.OnPressMachineOpened += OnPressMachineOpened;
        Events.GamePlay.OnCraneCollider += OnCraneCollider;
    }
    
    private void OnDisable()
    {
        Events.GamePlay.OnPuzzleWin -= OnPuzzleWin;
        Events.GamePlay.OnSteamDischarged -= OnSteamDischarged;
        Events.GamePlay.OnDoorControlled -= OnDoorControlled;
        Events.GamePlay.OnPressMachineOpened -= OnPressMachineOpened;
        Events.GamePlay.OnCraneCollider -= OnCraneCollider;
    }

    private void OnPuzzleWin()
    {
       takeMinimapArrow.SetActive(false);
       steamArrow.SetActive(true);
    }
    
    private void OnSteamDischarged()
    {
        steamArrow.SetActive(false);
        doorControlArrow.SetActive(true);
    }
    
    private void OnDoorControlled()
    {
        doorControlArrow.SetActive(false);
        turnPressMachineArrow.SetActive(true);
    }
    
    private void OnPressMachineOpened()
    {
        turnPressMachineArrow.SetActive(false);
        craneControlArrow.SetActive(true);
    }
    
    private void OnCraneCollider()
    {
        craneControlArrow.SetActive(false);
    }
}