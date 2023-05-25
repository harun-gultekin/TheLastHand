using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    public bool thePlayerInBustedRange = false;
    public AgentAlexNavMesh theAgentAlexNavMesh;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(theAgentAlexNavMesh.playerInBustedRange && theAgentAlexNavMesh.thePlayerInEnemyFOV)
        {
            thePlayerInBustedRange = true;
        }
        else
        {
           thePlayerInBustedRange = false; 
        }

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
    }
}