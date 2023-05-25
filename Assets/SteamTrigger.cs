using System.Collections;
using System.Collections.Generic;
using LastHand;
using UnityEngine;

public class SteamTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem steamParticle;
    
    private void OnEnable()
    {
        Events.GamePlay.OnCountdownCompleted += OnCountdownCompleted;
    }
    
    private void OnDisable()
    {
        Events.GamePlay.OnCountdownCompleted -= OnCountdownCompleted;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            steamParticle.Play();
        }
    }
    
    private void OnCountdownCompleted()
    {
        //UIManager.Instance.
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            UIManager.Instance.alertText.text = AlertUITexts.TURN_VALVE;
        }
    }
}