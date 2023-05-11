using UnityEngine;
using LastHand;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public float rotationSpeed;
    private Animator _playerAnimator;
    public float movementVerticalInput;
    public float movementHorizontalInput;
    public float movementJumpInput;
    private Rigidbody _selfRigidbody;
    public bool isGrounded;
    public bool jumpAction;
    public int jumpHeight = 150;
    private float _keepY;
    enum JumpState    {Ground, JumpPrepare, Jumping, Landing}
    JumpState _jState = JumpState.Ground;

    public Transform cameraTransform;

    void Start()
    {
        _playerAnimator = GetComponentInChildren<Animator>();
        _selfRigidbody = GetComponent<Rigidbody>();
    }
    
    void LateUpdate()
    {
        GroundCheck();
        Jump();
        if (_jState == JumpState.Ground) Move();
        if (_jState == JumpState.Ground) Rotate();
    }

    private void OnEnable()
    {
        Events.GamePlay.OnMinimapCollider += OnMinimapCollider;
        Events.GamePlay.OnPuzzleWin += OnPuzzleWin;
    }
    
    private void OnDisable()
    {
        Events.GamePlay.OnMinimapCollider -= OnMinimapCollider;
        Events.GamePlay.OnPuzzleWin -= OnPuzzleWin;
    }

    #region Movement
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
        if ((movementVerticalInput != 0 || movementHorizontalInput != 0) && isGrounded) _playerAnimator.SetBool("Walk", true);
        else _playerAnimator.SetBool("Walk", false);
    }

    private void Jump()
    {
        //Debug.Log(_jState);
        movementJumpInput = Input.GetAxis("Jump");
        switch(_jState)
        {
            case JumpState.Ground:
            {
                if (movementJumpInput != 0 && isGrounded && (_playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Walk") || _playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("IdleJump") || _playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("IdleJump 0")) )
                {
                    _playerAnimator.SetBool("Jump", true);
                    _playerAnimator.SetBool("Walk", false);
                    _playerAnimator.SetBool("JumpRelease", false);
                    _playerAnimator.SetBool("Land", false);
                    _jState = JumpState.JumpPrepare;
                }
                break;
            }
            case JumpState.JumpPrepare:
            {
                if (movementJumpInput == 0 && _playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("JumpPrepare"))
                {
                    _playerAnimator.SetBool("Jump", true);
                    _playerAnimator.SetBool("Walk", false);
                    _playerAnimator.SetBool("JumpRelease", true);
                    _playerAnimator.SetBool("Land", false);
                    movementVerticalInput = Input.GetAxis("Vertical");
                    movementHorizontalInput = Input.GetAxis("Horizontal");
                    Vector3 JumpForce = Vector3.zero;
                    JumpForce += cameraTransform.forward * movementVerticalInput * jumpHeight/2;
                    JumpForce += cameraTransform.right * movementHorizontalInput * jumpHeight/2;
                    JumpForce.y = 0;
                    JumpForce += Vector3.up * jumpHeight;
                    _selfRigidbody.AddForce(JumpForce);
                    _jState = JumpState.Jumping;
                }
                break;
            }
            case JumpState.Jumping:
            {
                if (Physics.Raycast(transform.position, new Vector3(0, -0.50f), out RaycastHit hit, 0.50f) && _playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
                {
                    _playerAnimator.SetBool("Jump", false);
                    _playerAnimator.SetBool("Walk", false);
                    _playerAnimator.SetBool("JumpRelease", false);
                    _playerAnimator.SetBool("Land", true);
                    _jState = JumpState.Landing;
                }
                break;
            }
            case JumpState.Landing:
            {
                if (isGrounded && _playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Land"))
                {
                    _playerAnimator.SetBool("Jump", false);
                    _playerAnimator.SetBool("Walk", false);
                    _playerAnimator.SetBool("JumpRelease", false);
                    _playerAnimator.SetBool("Land", false);
                    jumpAction = false;
                    _jState = JumpState.Ground;
                }
                break;
            }
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
    #endregion
    
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

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Minimap"))
        {
            Events.GamePlay.OnMinimapCollider.Call();
        }
    }
    
    private void OnMinimapCollider()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }
    
    private void OnPuzzleWin()
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }
}