using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMovementSpeed : MonoBehaviour
{
    PlayerController pc = null;
    private bool powerUpActive = false;

    void Start()
    {
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !powerUpActive)
        {
            powerUpActive = true;
            StartCoroutine(PowerUpWearOff(5));
            powerUpActive = false;
        }
    }

    IEnumerator PowerUpWearOff(float waitTime)
    {
        float speed = pc.movementSpeed;
        pc.movementSpeed = pc.movementSpeed * 2;
        yield return new WaitForSeconds(waitTime);
        pc.movementSpeed = speed;
    }
}