using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LastHand;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button startButton;

    private void OnEnable()
    {
        startButton.onClick.AddListener(() =>
        {
            Events.Menu.StartGameButton.Call();
            SceneManager.LoadScene("GameScene");
        });
    }

    private void OnDisable()
    {
        startButton.onClick.RemoveAllListeners();
    }
}