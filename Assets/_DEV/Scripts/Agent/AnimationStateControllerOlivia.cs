using UnityEngine;

public class AnimationStateControllerOlivia : MonoBehaviour
{
    Animator animator;
    public bool thePlayerInBustedRange = false;
    public AgentOliviaScript theAgentOliviaScript;

    void Start()
    {
        animator = GetComponent<Animator>();
        //PatrollingState = "Patrolling";
        thePlayerInBustedRange = theAgentOliviaScript.playerInBustedRange;

        //Debug.Log(animator);
    }

    void Update()
    {

            
        thePlayerInBustedRange = theAgentOliviaScript.playerInBustedRange;

        if(thePlayerInBustedRange == false)
        {
            animator.SetBool("isWalking",true);
            animator.SetBool("isBusted",false);
        }
        
        if(thePlayerInBustedRange == true)
        {
            animator.SetBool("isWalking",false);
            animator.SetBool("isBusted",true);
        }


        /*
            if(Input.GetKeyDown("w"))
            {
                animator.SetBool("isWalking",true);
            }

            if(Input.GetKeyUp("w"))
            {
                animator.SetBool("isWalking",false);
            }
        */
    }
}