using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    public bool thePlayerInBustedRange = false;
    public AgentAlexNavMesh theAgentAlexNavMesh;

    void Start()
    {
        animator = GetComponent<Animator>();
        //PatrollingState = "Patrolling";
        thePlayerInBustedRange = theAgentAlexNavMesh.playerInBustedRange;

        //Debug.Log(animator);
    }

    void Update()
    {

            
        thePlayerInBustedRange = theAgentAlexNavMesh.playerInBustedRange;

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