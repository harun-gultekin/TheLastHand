using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour
{
    public float speed;
    private Animator playerAnimator;
    public float movementVerticalInput;
    public float movementHorizontalInput;
    public float movementJumpInput;
    private Rigidbody selfRigidbody;
    public bool isGrounded;
    public int jumpHeight = 150;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponentInChildren<Animator>();
        selfRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Move();
    }

    private void Move()
    {
        // Get movement inputs from Input Manager
        movementVerticalInput = Input.GetAxis("Vertical");
        movementHorizontalInput = Input.GetAxis("Horizontal");
        // Take first position of object as Vector3 beginning of the frame and set new position according to movement inputs and set this Vector3 as new position
        Vector3 currentPosition = transform.position;
        currentPosition.z += movementVerticalInput * speed * Time.deltaTime;
        currentPosition.x += movementHorizontalInput * speed * Time.deltaTime;
        transform.position = currentPosition;

        // Animation play according to movement input, animation transitions handled by "move" and "jump" parameters which set in Animator. 0.1 is threshold for animation plays.
        if ((movementVerticalInput != 0 || movementHorizontalInput != 0) && isGrounded) playerAnimator.SetFloat("move", 0.2f);
        else playerAnimator.SetFloat("move", 0.0f);

    }

    private void Jump()
    {
        // Space Jump input from Input Manager, when it is pressed and character is on Ground, play Jump animation and add force in y-axis.
        movementJumpInput = Input.GetAxis("Jump");
        if (isGrounded && (movementJumpInput != 0))
        {
            playerAnimator.SetFloat("jump", 0.2f);
            selfRigidbody.AddForce(Vector3.up * jumpHeight);
        }

        else playerAnimator.SetFloat("jump", 0.0f);
    }

    private void OnCollisionEnter(Collision other)
    {
    // Function for detecting when player hit ground, collision enter
    if(other.gameObject.tag == "Ground")
        {
            isGrounded = true;
            playerAnimator.SetFloat("jump", 0.0f);
        }
    }
    void OnCollisionExit(Collision other)
    {
        // Function for detecting when player-ground connection end, collision exit.
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
            playerAnimator.SetFloat("jump", 0.0f);
        }
    }

}
