using System.Collections;
using System.Collections.Generic;
using LastHand;
using UnityEngine;

public class LevelArrowManager : MonoBehaviour
{
    [SerializeField] private GameObject takeMinimapArrow;
    [SerializeField] private GameObject steamArrow;
    [SerializeField] private GameObject doorControlArrow;
    [SerializeField] private GameObject systemControlArrow;
    [SerializeField] private GameObject openVentilationArrow;
    [SerializeField] private GameObject turnPressMachineArrow;
    
    
    private void OnEnable()
    {
        Events.GamePlay.OnPuzzleWin += OnPuzzleWin;
        Events.GamePlay.OnSteamDischarged += OnSteamDischarged;
        Events.GamePlay.OnDoorControlled += OnDoorControlled;
        Events.GamePlay.OnSystemControlled += OnSystemControlled;
        Events.GamePlay.OnVentilationOpened += OnVentilationOpened;
        Events.GamePlay.OnPressMachineOpened += OnPressMachineOpened;

    }
    
    private void OnDisable()
    {
        Events.GamePlay.OnPuzzleWin -= OnPuzzleWin;
        Events.GamePlay.OnSteamDischarged -= OnSteamDischarged;
        Events.GamePlay.OnDoorControlled -= OnDoorControlled;
        Events.GamePlay.OnSystemControlled -= OnSystemControlled;
        Events.GamePlay.OnVentilationOpened -= OnVentilationOpened;
        Events.GamePlay.OnPressMachineOpened -= OnPressMachineOpened;
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
        systemControlArrow.SetActive(true);
    }
    
    private void OnSystemControlled()
    {
        systemControlArrow.SetActive(false);
        openVentilationArrow.SetActive(true);
    }
    
    private void OnVentilationOpened()
    {
        openVentilationArrow.SetActive(false);
        turnPressMachineArrow.SetActive(true);
    }
    
    private void OnPressMachineOpened()
    {
        turnPressMachineArrow.SetActive(false);
    }
}