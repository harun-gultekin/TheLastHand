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
            GetComponent<Collider>().enabled = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            StartCoroutine(PowerUpWearOff(5));
            powerUpActive = false;
            StartCoroutine(Cooldown(30));
        }
    }

    IEnumerator PowerUpWearOff(float waitTime)
    {
        int jump = pc.jumpHeight;
        pc.jumpHeight = pc.jumpHeight * 2;
        yield return new WaitForSeconds(waitTime);
        pc.jumpHeight = jump;
    }

    IEnumerator Cooldown(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GetComponent<Collider>().enabled = true;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}
