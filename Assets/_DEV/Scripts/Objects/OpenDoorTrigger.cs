using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorTrigger : MonoBehaviour
{
    public bool isAuto;
    public Animator anim;

    public Transform player;
    public Transform agent;
    public Transform door;
    
/*
    private void onTriggerEnter(Collider collision)
    {
        //if(collision.tag == "Player")
        if(collision.gameObject.name == "Player")
        {
            anim.SetBool("isOpen", true); 
            //Debug.Log(isOpen);      
        }
    }

    private void onTriggerExit(Collider collision)
    {
        if(collision.gameObject.name == "Player")
        {
            anim.SetBool("isOpen", false);  
        }
    }
*/



    void Update()
    {
        float distance1 = Vector3.Distance(player.position, door.position);
        float distance2 = Vector3.Distance(agent.position, door.position);


        if(distance2 <= 2 || distance1 <= 2)
        {
            anim.SetBool("isOpen", true); 
        }
        else
        {
            anim.SetBool("isOpen", false); 
        }


    }


}
