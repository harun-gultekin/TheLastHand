using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpJump : MonoBehaviour
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
        int jump = pc.jumpHeight;
        pc.jumpHeight = pc.jumpHeight * 2;
        yield return new WaitForSeconds(waitTime);
        pc.jumpHeight = jump;
    }
}
