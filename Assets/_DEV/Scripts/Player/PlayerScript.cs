using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour
{
    public float movementSpeed;
    public float rotationSpeed;
    private Animator playerAnimator;
    public float movementVerticalInput;
    public float movementHorizontalInput;
    public float movementJumpInput;
    private Rigidbody selfRigidbody;
    public bool isGrounded;
    public bool JumpAction;
    public int jumpHeight = 150;
    private float keepY;
    enum JumpState    {Ground, JumpPrepare, Jumping, Landing}
    JumpState jState = JumpState.Ground;

     // Camera variables
    public Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponentInChildren<Animator>();
        selfRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GroundCheck();
        Jump();
        if (jState == JumpState.Ground) Move();
        if (jState == JumpState.Ground) Rotate();
    }

    private void Move()
    {

        // Get movement inputs from Input Manager
        movementVerticalInput = Input.GetAxis("Vertical");
        movementHorizontalInput = Input.GetAxis("Horizontal");
        // Take first position of object as Vector3 beginning of the frame and set new position according to movement inputs and set this Vector3 as new position
        Vector3 currentPosition = transform.position;
        Vector3 movePosition = Vector3.zero;
        movePosition += cameraTransform.forward * movementVerticalInput * movementSpeed * Time.deltaTime;
        movePosition += cameraTransform.right * movementHorizontalInput * movementSpeed * Time.deltaTime;
        //currentPosition += cameraTransform.forward * movementVerticalInput * movementSpeed * Time.deltaTime;
        //currentPosition += cameraTransform.right * movementHorizontalInput * movementSpeed * Time.deltaTime;
        movePosition.y = 0;
        transform.position = currentPosition + movePosition;

        // Animation play according to movement input, animation transitions handled by "move" and "jump" parameters which set in Animator. 0.1 is threshold for animation plays.
        if ((movementVerticalInput != 0 || movementHorizontalInput != 0) && isGrounded) playerAnimator.SetBool("Walk", true);
        else playerAnimator.SetBool("Walk", false);

    }

    private void Jump()
    {
        Debug.Log(jState);
        movementJumpInput = Input.GetAxis("Jump");
        switch(jState)
        {
            case JumpState.Ground:
            {
                if (movementJumpInput != 0 && isGrounded && (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Walk") || playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("IdleJump") || playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("IdleJump 0")) )
                {
                    playerAnimator.SetBool("Jump", true);
                    playerAnimator.SetBool("Walk", false);
                    playerAnimator.SetBool("JumpRelease", false);
                    playerAnimator.SetBool("Land", false);
                    jState = JumpState.JumpPrepare;
                }
                break;
            }
            case JumpState.JumpPrepare:
            {
                if (movementJumpInput == 0 && playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("JumpPrepare"))
                {
                    playerAnimator.SetBool("Jump", true);
                    playerAnimator.SetBool("Walk", false);
                    playerAnimator.SetBool("JumpRelease", true);
                    playerAnimator.SetBool("Land", false);
                    movementVerticalInput = Input.GetAxis("Vertical");
                    movementHorizontalInput = Input.GetAxis("Horizontal");
                    Vector3 JumpForce = Vector3.zero;
                    JumpForce += cameraTransform.forward * movementVerticalInput * jumpHeight/2;
                    JumpForce += cameraTransform.right * movementHorizontalInput * jumpHeight/2;
                    JumpForce.y = 0;
                    JumpForce += Vector3.up * jumpHeight;
                    selfRigidbody.AddForce(JumpForce);
                    jState = JumpState.Jumping;
                }
                break;
            }
            case JumpState.Jumping:
            {
                if (Physics.Raycast(transform.position, new Vector3(0, -0.50f), out RaycastHit hit, 0.50f) && playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
                {
                    playerAnimator.SetBool("Jump", false);
                    playerAnimator.SetBool("Walk", false);
                    playerAnimator.SetBool("JumpRelease", false);
                    playerAnimator.SetBool("Land", true);
                    jState = JumpState.Landing;
                }
                break;
            }
            case JumpState.Landing:
            {
                if (isGrounded && playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Land"))
                {
                    playerAnimator.SetBool("Jump", false);
                    playerAnimator.SetBool("Walk", false);
                    playerAnimator.SetBool("JumpRelease", false);
                    playerAnimator.SetBool("Land", false);
                    JumpAction = false;
                    jState = JumpState.Ground;
                }
                break;
            }
                default: break;
        }

        // Space Jump input from Input Manager, when it is pressed and character is on Ground, play Jump animation and add force in y-axis.
        //else playerAnimator.SetBool("Jump", false);
    }

    private void Rotate()
    {
        Vector3 targetDirection = Vector3.zero;

        movementVerticalInput = (-1)*Input.GetAxis("Vertical");
        movementHorizontalInput = Input.GetAxis("Horizontal");
        targetDirection = cameraTransform.forward * movementHorizontalInput;
        targetDirection += cameraTransform.right * movementVerticalInput;
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    private void GroundCheck()
    {
        RaycastHit hit;
        float distance = 1f;
        Vector3 dir = new Vector3(0, -0.08f);

        Debug.DrawRay((new Vector3(transform.position.x, transform.position.y, transform.position.z)), dir, Color.green, 5);

        if(Physics.Raycast(transform.position, dir, out hit, distance))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }


}
