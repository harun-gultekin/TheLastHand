using System.Collections;
using UnityEngine;
using LastHand;
using Event = LastHand.Event;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5;
    public float rotationSpeed;
    private Animator _playerAnimator;
    public float movementVerticalInput;
    public float movementHorizontalInput;
    public float movementJumpInput;
    private Rigidbody _selfRigidbody;
    private Collider _selfCollider;
    public bool isGrounded;
    public bool jumpAction;
    public bool falling;
    public int jumpHeight = 150;
    private float _keepY;

    [SerializeField] private ParticleSystem steamParticle;

    enum JumpState
    {
        Ground, JumpPrepare, Jumping, Landing
    }
    JumpState _jState = JumpState.Ground;
    enum HideState
    {
        Ground, JumpPrepare, Jumping, HoldDrawer, Landing
    }
    HideState _hState = HideState.Ground;

    enum ValveState
    {
        Ground, JumpPrepare, Jumping, RotateValve, Landing
    }
    ValveState _vState = ValveState.Ground;
    
    public bool hidingDrawer = false;
    private bool hideCapability = false;
    private GameObject holdPoint;
    private float enableColliderPoint;
    private GameObject hidingTrigger;
    private GameObject closeDrawer;
    public bool hidingStatus = false;
    private float distHoldPoint;
    public Transform cameraTransform;
    private Coroutine _activateCollider;

    public bool steamMove = false;
    public bool steamTrigger = false;

    void Start()
    {
        _playerAnimator = GetComponentInChildren<Animator>();
        _selfRigidbody = GetComponent<Rigidbody>();
        _selfCollider = GetComponent<Collider>();
    }
    
    void LateUpdate()
    {
        if (LevelStateManager.Instance)
        {
            if (LevelStateManager.Instance.currentState != LevelState.OnMenu)
            {
                HideCheck();
                steamCheck();
                if (!hidingDrawer && !steamMove)
                {
                    GroundCheck();
                    Jump();
                    Move();
                    Rotate();
                }
                else
                {
                    if (hidingDrawer)
                    {
                        HideDrawer();
                    }

                    if (steamMove)
                    {
                        MoveValve();
                    }
                }
            }
        }
    }

    private void OnEnable()
    {
        Events.GamePlay.OnMinimapCollider += OnMinimapCollider;
        Events.GamePlay.OnPuzzleWin += OnPuzzleWin;
        Events.UIGamePlay.OnPuzzleClose += OnPuzzleClose;
    }
    
    private void OnDisable()
    {
        Events.GamePlay.OnMinimapCollider -= OnMinimapCollider;
        Events.GamePlay.OnPuzzleWin -= OnPuzzleWin;
        Events.UIGamePlay.OnPuzzleClose -= OnPuzzleClose;
    }

    #region Move Valve

    private void MoveValve()
    {
        switch(_vState)
        {
            case ValveState.Ground:
            {
                if (isGrounded && (_playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Walk") || _playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("IdleJump") || _playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("IdleJump 0")) )
                {
                    enableColliderPoint = transform.position.y;
                    _playerAnimator.SetBool("Jump", true);
                    _playerAnimator.SetBool("Walk", false);
                    _playerAnimator.SetBool("JumpRelease", false);
                    _playerAnimator.SetBool("Land", false);
                    _playerAnimator.SetBool("Hold", false);
                    _vState = ValveState.JumpPrepare;
                }

                break;
            }
            case ValveState.JumpPrepare:
            {
                if (_playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("JumpPrepare"))
                {
                    _playerAnimator.SetBool("Jump", true);
                    _playerAnimator.SetBool("Walk", false);
                    _playerAnimator.SetBool("JumpRelease", true);
                    _playerAnimator.SetBool("Land", false);
                    _playerAnimator.SetBool("Hold", false);
                    //movementVerticalInput = Input.GetAxis("Vertical");
                    //movementHorizontalInput = Input.GetAxis("Horizontal");
                    distHoldPoint = Vector3.Distance(holdPoint.transform.position, transform.position);
                    _selfCollider.enabled = false;
                    _selfRigidbody.useGravity = false;
                    _vState = ValveState.Jumping;
                }
                break;
            }
            case ValveState.Jumping:
            {
                float step = movementSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, holdPoint.transform.position, step);
                if (Vector3.Distance(holdPoint.transform.position, transform.position) < distHoldPoint*0.1f)
                {
                    _selfRigidbody.constraints = RigidbodyConstraints.FreezeAll;
                    _playerAnimator.SetBool("Jump", true);
                    _playerAnimator.SetBool("Walk", false);
                    _playerAnimator.SetBool("JumpRelease", true);
                    _playerAnimator.SetBool("Land", false);
                    _playerAnimator.SetBool("Hold", true);
                    _vState = ValveState.RotateValve;
                }
                break;
            }
            case ValveState.RotateValve:
            {
                if (_playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hold"))
                {
                    float step = movementSpeed * Time.deltaTime;
                    Quaternion target = Quaternion.Euler(270f, 0, 0);
                    holdPoint.transform.GetChild(0).gameObject.transform.localRotation = Quaternion.Slerp(holdPoint.transform.GetChild(0).gameObject.transform.localRotation, target, step);
                    if (holdPoint.transform.GetChild(0).gameObject.transform.eulerAngles.x == 270f)
                    {
                        _selfRigidbody.constraints = RigidbodyConstraints.None;
                        _selfRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                        _selfRigidbody.useGravity = true;
                        _playerAnimator.SetBool("Jump", false);
                        _playerAnimator.SetBool("Walk", false);
                        _playerAnimator.SetBool("JumpRelease", false);
                        _playerAnimator.SetBool("Land", true);
                        _playerAnimator.SetBool("Hold", false);
                        _vState = ValveState.Landing;
                    }
                }

                break;
            }

            case ValveState.Landing:
            {
                steamParticle.gameObject.SetActive(true);
                steamParticle.Play();
                Events.GamePlay.OnSteamDischarged.Call();
                
                if(enableColliderPoint + 0.2f > transform.position.y)
                {
                    _selfCollider.enabled = true;
                }
                if (_selfCollider.enabled && _playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Land"))
                {
                    _playerAnimator.SetBool("Jump", false);
                    _playerAnimator.SetBool("Walk", false);
                    _playerAnimator.SetBool("JumpRelease", false);
                    _playerAnimator.SetBool("Land", false);
                    jumpAction = false;
                    _vState = ValveState.Ground;
                    steamMove = false;
                }
                break;
            }
        }
    }

    private void steamCheck()
    {
        if (steamTrigger && Input.GetKeyDown(KeyCode.E))
        {
            steamMove = true;
        }
    }

    #endregion

    #region Drawer Hide

    private void HideDrawer()
    {
        switch(_hState)
        {
            case HideState.Ground:
            {
                if (isGrounded && (_playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Walk") || _playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("IdleJump") || _playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("IdleJump 0")) )
                {
                    enableColliderPoint = transform.position.y;
                    _playerAnimator.SetBool("Jump", true);
                    _playerAnimator.SetBool("Walk", false);
                    _playerAnimator.SetBool("JumpRelease", false);
                    _playerAnimator.SetBool("Land", false);
                    _playerAnimator.SetBool("Hold", false);
                    _hState = HideState.JumpPrepare;
                }

                break;
            }
            case HideState.JumpPrepare:
            {
                if (_playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("JumpPrepare"))
                {
                    _playerAnimator.SetBool("Jump", true);
                    _playerAnimator.SetBool("Walk", false);
                    _playerAnimator.SetBool("JumpRelease", true);
                    _playerAnimator.SetBool("Land", false);
                    _playerAnimator.SetBool("Hold", false);
                    //movementVerticalInput = Input.GetAxis("Vertical");
                    //movementHorizontalInput = Input.GetAxis("Horizontal");
                    distHoldPoint = Vector3.Distance(holdPoint.transform.position, transform.position);
                    _selfCollider.enabled = false;
                    _selfRigidbody.useGravity = false;
                    _hState = HideState.Jumping;
                }
                break;
            }
            case HideState.Jumping:
            {
                float step = movementSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, holdPoint.transform.position, step);
                if (Vector3.Distance(holdPoint.transform.position, transform.position) < distHoldPoint*0.1f)
                {
                    _selfRigidbody.constraints = RigidbodyConstraints.FreezeAll;
                    _playerAnimator.SetBool("Jump", true);
                    _playerAnimator.SetBool("Walk", false);
                    _playerAnimator.SetBool("JumpRelease", true);
                    _playerAnimator.SetBool("Land", false);
                    _playerAnimator.SetBool("Hold", true);
                    _hState = HideState.HoldDrawer;
                }
                break;
            }
            case HideState.HoldDrawer:
            {
                if (_playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hold"))
                {
                    float step = movementSpeed * Time.deltaTime;
                    holdPoint.transform.localPosition = Vector3.MoveTowards(holdPoint.transform.localPosition, new Vector3(-0.01f,1.887f,0.05f), step);
                    transform.position = Vector3.MoveTowards(transform.position, holdPoint.transform.position, step);
                    if (holdPoint.transform.localPosition == new Vector3(-0.01f,1.887f,0.05f))
                    {
                        _selfRigidbody.constraints = RigidbodyConstraints.None;
                        _selfRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                        _selfRigidbody.useGravity = true;
                        _playerAnimator.SetBool("Jump", false);
                        _playerAnimator.SetBool("Walk", false);
                        _playerAnimator.SetBool("JumpRelease", false);
                        _playerAnimator.SetBool("Land", true);
                        _playerAnimator.SetBool("Hold", false);
                        _hState = HideState.Landing;
                    }
                }

                break;
            }

            case HideState.Landing:
            {
                if(enableColliderPoint + 0.2f > transform.position.y)
                {
                    _selfCollider.enabled = true;
                }
                if (_selfCollider.enabled && _playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Land"))
                {
                    _playerAnimator.SetBool("Jump", false);
                    _playerAnimator.SetBool("Walk", false);
                    _playerAnimator.SetBool("JumpRelease", false);
                    _playerAnimator.SetBool("Land", false);
                    jumpAction = false;
                    _hState = HideState.Ground;
                    hidingDrawer = false;
                }
                break;
            }
        }
    }

    private void HideCheck()
    {
        if (hideCapability && Input.GetKeyDown(KeyCode.E) && !hidingDrawer)
        {
            hideCapability = false;
            hidingDrawer = true;
            Destroy(hidingTrigger.GetComponent<Collider>());
        }
    }
    
    #endregion

    #region Movement
    private void Move()
    {
        if (!LevelStateManager.Instance.isMinimapPuzzleActive)
        {
            movementVerticalInput = Input.GetAxis("Vertical");
            movementHorizontalInput = Input.GetAxis("Horizontal");
        }
        else
        {
            movementVerticalInput = 0;
            movementHorizontalInput = 0;
        }

        Vector3 movePosition = Vector3.zero;
        movePosition += cameraTransform.forward * movementVerticalInput * movementSpeed * Time.deltaTime;
        movePosition += cameraTransform.right * movementHorizontalInput * movementSpeed * Time.deltaTime;
        movePosition.y = 0;

        movePosition.Normalize();
        
        _selfRigidbody.velocity = new Vector3(movePosition.x * movementSpeed, _selfRigidbody.velocity.y, movePosition.z * movementSpeed);

        if ((movementVerticalInput != 0 || movementHorizontalInput != 0) && isGrounded && (_jState == JumpState.Ground))
        {
            _playerAnimator.SetBool("Walk", true);
        }
        else
        {
            _playerAnimator.SetBool("Walk", false);
        }
    }

    private void Jump()
    {
        if (!LevelStateManager.Instance.isMinimapPuzzleActive)
        {
            movementJumpInput = Input.GetAxis("Jump");
        }
        else
        {
            movementJumpInput = 0;
        }
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
                    //movementVerticalInput = Input.GetAxis("Vertical");
                    //movementHorizontalInput = Input.GetAxis("Horizontal");
                    Vector3 JumpForce = Vector3.zero;
                    //JumpForce += cameraTransform.forward * movementVerticalInput * jumpHeight/2;
                    //JumpForce += cameraTransform.right * movementHorizontalInput * jumpHeight/2;
                    //JumpForce.y = 0;
                    JumpForce += Vector3.up * jumpHeight;
                    _selfRigidbody.AddForce(JumpForce);
                    _jState = JumpState.Jumping;
                }
                break;
            }
            case JumpState.Jumping:
            {
                /*if (Physics.Raycast(transform.position, new Vector3(0, -0.50f), out RaycastHit hit, 0.50f) && _playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))*/
                if ((falling || isGrounded) && _playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
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
        if (_selfRigidbody.velocity.y>0)
        {
            jumpAction = true;
            falling = false;
            isGrounded = false;
        }

        if (jumpAction && (_selfRigidbody.velocity.y<0))
        {
            jumpAction = true;
            falling = true;
            isGrounded = false;
        }

        if (falling && (_selfRigidbody.velocity.y == 0))
        {
            jumpAction = false;
            falling = false;
            isGrounded = true;
        }

        RaycastHit hit;
        float distance = 1f;
        Vector3 dir = new Vector3(0, -0.08f);
        Vector3 groundPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        Debug.DrawRay(groundPosition + transform.forward * 0.1f, dir, Color.green, 5);
        Debug.DrawRay(groundPosition - transform.forward * 0.1f, dir, Color.green, 5);
        Debug.DrawRay(groundPosition + transform.right * 0.1f, dir, Color.green, 5);
        Debug.DrawRay(groundPosition - transform.right * 0.1f, dir, Color.green, 5);
        Debug.DrawRay((new Vector3(transform.position.x, transform.position.y, transform.position.z)), dir, Color.green, 5);

        if(Physics.Raycast(transform.position, dir, out hit, distance) ||
            Physics.Raycast(transform.position + transform.forward * 0.1f, dir, out hit, distance) ||
            Physics.Raycast(transform.position - transform.forward * 0.1f, dir, out hit, distance) ||
            Physics.Raycast(transform.position + transform.right * 0.1f, dir, out hit, distance) ||
            Physics.Raycast(transform.position - transform.right * 0.1f, dir, out hit, distance))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Minimap"))
        {
            Events.GamePlay.OnMinimapCollider.Call();
            collision.enabled = false;
            
            if (LevelStateManager.Instance.currentState != LevelState.MinimapTaken)
            {
               StartCoroutine(ActivateCollider(collision));
            }
        }
        
        if (collision.gameObject.CompareTag("Crane"))
        {
            Events.GamePlay.OnCraneCollider.Call();
            collision.enabled = false;
            
            StartCoroutine(ActivateCollider(collision));
        }
        
        if (collision.gameObject.name == "HidingTrigger")
        {
            hidingTrigger = collision.gameObject;
            hideCapability = true;
            holdPoint = collision.gameObject.transform.GetChild(0).gameObject;
        }
        
        if (collision.gameObject.name == "Steam")
        {
          
            Debug.Log("steam");
            UIManager.Instance.alertText.text = AlertUITexts.TURN_VALVE;
        }

        if (collision.gameObject.name == "ValveHoldPoint")
        {
            holdPoint = collision.gameObject;
            steamTrigger = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name == "HidingTrigger")
        {
            hideCapability = false;
        }

        if (collision.gameObject.name == "ValveHoldPoint")
        {
            steamTrigger = false;
        }
    }
    
    IEnumerator ActivateCollider(Collider collision)
    {
        yield return new WaitForSeconds(10);
        collision.enabled = true;
    }
    
    private void OnMinimapCollider()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }
    
    private void OnPuzzleWin()
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }
    
    private void OnPuzzleClose()
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
