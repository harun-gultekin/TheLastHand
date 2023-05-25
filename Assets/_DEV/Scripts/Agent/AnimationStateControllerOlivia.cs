using UnityEngine;

public class AnimationStateControllerOlivia : MonoBehaviour
{
    Animator animator;
    public bool thePlayerInBustedRange = false;
    public AgentOliviaScript theAgentOliviaScript;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(theAgentOliviaScript.playerInBustedRange && theAgentOliviaScript.thePlayerInEnemyFOV)
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