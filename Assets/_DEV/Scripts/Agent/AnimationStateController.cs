using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        //Debug.Log(animator);
    }

    void Update()
    {
        if(Input.GetKeyDown("w"))
        {
            animator.SetBool("isWalking",true);
        }

        if(Input.GetKeyUp("w"))
        {
            animator.SetBool("isWalking",false);
        }
    }
}