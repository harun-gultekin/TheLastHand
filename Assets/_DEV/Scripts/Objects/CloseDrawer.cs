using System.Collections;
using System.Collections.Generic;
using LastHand;
using UnityEngine;

public class CloseDrawer : MonoBehaviour
{
    private bool startMove = false;
    private bool b_move = false;
    private bool close = false;
    private bool open = false;
    
    void Start()
    {
        close = true;
        open = false;
    }

    void Update()
    {
        if (b_move && Input.GetKeyDown(KeyCode.E))
        {
            startMove = true;
        }

        if(startMove)
        {
            if(close)
            {
                float step = 1.6f * Time.deltaTime;
                this.transform.parent.transform.parent.transform.localPosition = Vector3.MoveTowards(this.transform.parent.transform.parent.transform.localPosition, new Vector3(0.478f,1.887f,0.05f), step);
                if (this.transform.parent.transform.parent.transform.localPosition == new Vector3(0.478f,1.887f,0.05f))
                {
                    close = false;
                    open = true;
                    startMove = false;
                }
            }
            else if(open)
            {
                float step = 1.6f * Time.deltaTime;
                this.transform.parent.transform.parent.transform.localPosition = Vector3.MoveTowards(this.transform.parent.transform.parent.transform.localPosition, new Vector3(-0.01f,1.887f,0.05f), step);
                if (this.transform.parent.transform.parent.transform.localPosition == new Vector3(-0.01f,1.887f,0.05f))
                {
                    close = true;
                    open = false;
                    startMove = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().hidingStatus = true;
            b_move = true;
            
            UIManager.Instance.alertText.text = AlertUITexts.TURN_VALVE;

            Events.GamePlay.OnDrawerTrigger.Call();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().hidingStatus = false;
            b_move = false;
            
            Events.GamePlay.OnDrawerExit.Call();
        }
    }
}