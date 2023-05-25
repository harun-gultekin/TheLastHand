using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem steamParticle;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            UIManager.Instance.alertText.text = AlertUITexts.TURN_VALVE;
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            steamParticle.Play();
        }
    }
}