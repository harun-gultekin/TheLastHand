using System.Collections;
using System.Collections.Generic;
using LastHand;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private float totalTime = 20f;
    private float _currentTime; 
    [SerializeField] private Text countdownText; 
    
    private void Start()
    {
        _currentTime = totalTime;
    }

    private void Update()
    {
        _currentTime -= Time.deltaTime;
        
        if (_currentTime < 0f)
        {
            _currentTime = 0f;
            Events.GamePlay.OnCountdownCompleted.Call();
        }
        
        countdownText.text = _currentTime.ToString("F0");
    }
}