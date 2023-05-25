using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private float totalTime = 20f; // Total time for countdown
    private float _currentTime; // Current time left
    [SerializeField] private Text countdownText; // Reference to the Text component

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
        }
        
        countdownText.text = _currentTime.ToString("F0");
    }
}