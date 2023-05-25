using System.Collections;
using System.Collections.Generic;
using LastHand;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject puzzlePrefab;
    [SerializeField] private GameObject cranePrefab;
    [SerializeField] private GameObject steamPrefab;
    [SerializeField] private ParticleSystem steamParticle;

    //Minimap alana kadar countdown yok
    //countdown icerideki pc (door control panel) ile baslar 
    //Puzzle cozunce kapilar blocklanir, agents kapıyı acamaz 
    //O arada ventilatordan kacar countdown baslar
    
    //Sis icin de countdown olabilir sis kalma suresi olur
    //Agent dolanirken sis basilir 

    private void OnEnable()
    {
        Events.GamePlay.OnMinimapCollider += OnMinimapCollider;
        Events.GamePlay.OnPuzzleWin += OnPuzzleWin;
        Events.GamePlay.OnSteamDischarged += OnSteamDischarged;
        Events.GamePlay.OnDoorControlled += OnDoorControlled;
        Events.GamePlay.OnCraneCollider += OnCraneCollider;
        Events.UIGamePlay.OnPuzzleClose += OnPuzzleClose;
        Events.UIGamePlay.OnCraneClose += OnCraneClose;
    }
    
    private void OnDisable()
    {
        Events.GamePlay.OnMinimapCollider -= OnMinimapCollider;
        Events.GamePlay.OnPuzzleWin -= OnPuzzleWin;
        Events.GamePlay.OnSteamDischarged -= OnSteamDischarged;
        Events.GamePlay.OnDoorControlled -= OnDoorControlled;
        Events.GamePlay.OnCraneCollider -= OnCraneCollider;
        Events.UIGamePlay.OnPuzzleClose -= OnPuzzleClose;
        Events.UIGamePlay.OnCraneClose -= OnCraneClose;
    }
    
    private void OnMinimapCollider()
    {
        puzzlePrefab.SetActive(true);
    }
    
    private void OnPuzzleWin()
    {
        puzzlePrefab.SetActive(false);
    }
    
    private void OnSteamDischarged()
    {
        Debug.Log("steam");
        steamPrefab.SetActive(true);
        steamParticle.Play();
    }
    
    private void OnDoorControlled()
    {
        steamPrefab.SetActive(false);
    }

    private void OnPuzzleClose()
    {
        puzzlePrefab.SetActive(false);
    }
    
    private void OnCraneCollider()
    {
        cranePrefab.SetActive(true);
    }
    
    private void OnCraneClose()
    {
        cranePrefab.SetActive(false);
    }
}