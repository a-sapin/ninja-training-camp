using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private GrappleTest myGrapple;
    [SerializeReference] private Animator myAnimator;

    Rigidbody2D rb;
    Vector2 moveDirection;
    Vector2 angledMoveDir;
    Vector2 inputDirection;
    Vector3 groundNormal;
    bool wantsToJump;
    bool holdingJump;
    public bool isGrounded;
    bool isInputingMove;
    public bool canMove;
    public bool isUsingLadder;
    public bool isCloseToLadder;

    //POWERS BOOLEANS : Those booleans are used to let the code know whether the player is allowed to use some powers or not
    bool hasDashPower;
    bool hasDoubleJumpPower;
    bool hasGrapplePower;

    bool canGrapple;


    //Other variables used by POWERS
    bool doubleJumpAvailable;

    bool isDashing;
    float dashCooldownTimer;

    public float dashCooldown = 1.0f;
    public float dashDuration = 0.2f;

    public float gravityMultiplier = 1.0f;
    public float maxVelocity = 5.0f;
    public float accelerationMultiplier = 1.0f;
    public float jumpForceMultiplier = 10.0f;
    public float relativeDoubleJumpForceMultiplier = 0.75f; // is relative to jumpForceMultiplier
    public float counterForceMult = 1.0f;
    public float dashSpeedGain = 40.0f;
    public float ladderClimbSpeed = 1.0f;

    public float groundDetectionDistance = 1.0f;
    public Vector2 ExternForce { get; set; }

    public bool CanGrapple() { return canGrapple; }
    public void SetCantGrapple() { canGrapple = false; }
    public void EnableGrapple() { canGrapple = true; }
    public bool GetHasGrapplePower() { return hasGrapplePower; }

    public void RemoveDash() { hasDashPower = false; }
    public void RemoveDoubleJump() { hasDoubleJumpPower = false; }
    public void RemoveGrapple() { hasGrapplePower = false; }
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
        isUsingLadder = false;
        wantsToJump = false;
        holdingJump = false;
        hasDashPower = true;
        hasDoubleJumpPower = true;
        hasGrapplePower = true;

        dashCooldownTimer = 0.0f;
        isDashing = false;
    }

    

    // Update is called once per frame
    void Update()
    {
        SetAnimation(inputDirection);
        /*DetectGround();

        isInputingMove = !moveDirection.Equals(Vector2.zero); 

        if (Input.GetAxis("Jump") > 0)
        { 
            wantsToJump = true;
        }
        else
        {
            holdingJump = false;
            wantsToJump = false;
        }*/
           
    }

    private void FixedUpdate()
    {/*
        inputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), 0);
        float delta = Time.fixedDeltaTime;
        ApplyGravity();
        ApplyMovement();
        TryToJump();
        HandleDashing(delta);*/
    }

    // TODO: ladder climb animation
    private void SetAnimation(Vector2 direction)
    {

        if (!canMove)
        {
            myAnimator.SetBool("runLeft", false);
            myAnimator.SetBool("runRight", false);
            return;
        }
        if (direction.x < 0)
        {
            myAnimator.SetBool("runLeft", true);
            myAnimator.SetBool("runRight", false);
        }
        else if (direction.x > 0)
        {
            myAnimator.SetBool("runLeft", false);
            myAnimator.SetBool("runRight", true);
        }
        else
        {
            myAnimator.SetBool("runLeft", false);
            myAnimator.SetBool("runRight", false);
        }

    }

    private void ApplyMovement()
    {
        if (canMove)
        {
            // if velocity is less than max speed or opposite to the move direction
            if (Mathf.Abs(rb.velocity.x) < maxVelocity || Mathf.Clamp(rb.velocity.x, -1f, 1f) == -moveDirection.x)
            {
                rb.AddForce(angledMoveDir * accelerationMultiplier);
            }
        }
        ApplyExternForce();
        ApplyCounterForce();
    }

    private void ApplyExternForce()
    {
        rb.AddForce(ExternForce, ForceMode2D.Impulse);
        ExternForce = Vector2.zero;
    }
    private void TryToJump()
    {
        if (!canMove) return;
        if (isGrounded || isUsingLadder)
        {
            myAnimator.SetBool("jump", false);
            myAnimator.SetBool("jumpL", false);
        }
        
        if (wantsToJump && (isGrounded || isUsingLadder || isCloseToLadder) && !holdingJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForceMultiplier, ForceMode2D.Impulse);

            myAnimator.SetBool(moveDirection.x >= 0 ? "jumpL" : "jump", true);
            
            wantsToJump = false;
            holdingJump = true;
            isUsingLadder = false; // stop climbing ladder when jump is input
            isCloseToLadder = false; // allow a single jump when close to ladder
            FindObjectOfType<VFXManager>().Play("Jump");
        }

        //DoubleJump
        if (wantsToJump && !isGrounded && hasDoubleJumpPower && doubleJumpAvailable && !holdingJump && !isUsingLadder)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForceMultiplier * relativeDoubleJumpForceMultiplier, ForceMode2D.Impulse);
            wantsToJump = false;
            doubleJumpAvailable = false;
        }
    }

    private void DetectGround()
    {
        // Cast a ray straight down.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundDetectionDistance, groundLayerMask);

        if (hit.collider != null)
        {
            isGrounded = true;
            doubleJumpAvailable = true; //Recover ability to double jump when player is grounded
            groundNormal = hit.normal;
            //myGrapple.TryRefreshOnLand();
        }
        else
        {
            isGrounded = false;
            groundNormal = Vector2.up;
        }

        // if player is on a slope, make the angled move direction parallel to the slope
        angledMoveDir = Vector3.ProjectOnPlane(moveDirection, groundNormal).normalized;
    }

    private void ApplyGravity()
    {
        if (!isGrounded && !isUsingLadder) // not in the air or on ladder
        {
            rb.AddForce(Vector2.down * 9.8f * gravityMultiplier);
        }
            
    }

    // Slows down the player when grounded 
    private void ApplyCounterForce()
    {
        if (isGrounded && !isInputingMove && rb.velocity.magnitude > 0.01f)
        {
            rb.AddForce(-rb.velocity * counterForceMult);
        }  
    }

    private Vector2 previousDash; // stores value of th last dash to stop it after its duration is expired
    private void StopDashAnim() {myAnimator.SetBool("dash", false); }
    private void HandleDashing(float delta)
    {
        // the velocity gained while dashing, as a vector
        Vector2 boost = moveDirection.normalized * dashSpeedGain;

        if (hasDashPower && Input.GetAxis("Dash") > 0 && dashCooldownTimer <= 0.0f && rb.velocity.magnitude < dashSpeedGain)
        {
            isDashing = true;
            myAnimator.SetBool("dash", true);
            FindObjectOfType<VFXManager>().Play("Dash");
            Invoke(nameof(StopDashAnim), 0.5f);
           
            isUsingLadder = false;
            rb.velocity += boost;
            dashCooldownTimer = dashCooldown; // reset cooldown timer
            previousDash = boost;
        }
        else if (dashCooldownTimer > 0.0f)
        {
            
            dashCooldownTimer -= delta;

            //stops the dash after its duration
            if(dashCooldown - dashCooldownTimer >= dashDuration && isDashing == true && Mathf.Abs(rb.velocity.x) >= dashSpeedGain)
            {
                rb.velocity -= previousDash;
                
                isDashing = false;
            }
        }
    }

    public void ResetPlayerAndPosition(Vector2 position)
    {
       // myGrapple.ForceDetachGrapple();
        rb.velocity = Vector3.zero;

        wantsToJump = false;
        holdingJump = false;
        dashCooldownTimer = 0.0f;
        isDashing = false;
        doubleJumpAvailable = false;
        myAnimator.SetBool("jump", false);
        myAnimator.SetBool("dash", false);
        
        //myAnimator.Play("Idle");
        transform.position = position; // reset velocity
    }

    [SerializeField] private bool refreshDoubleJumpOnLadder = true;
    [SerializeField] private bool refreshGrappleOnLadder = true;

    private const int ladderLayer = 9; // for some reason a layermask doesnt work

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == ladderLayer)
        {
            isCloseToLadder = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject.layer == ladderLayer)
        {
            // Get the closest position to the player that is centered on the ladder
            Vector3 onLadder = new Vector3(collision.gameObject.transform.position.x, transform.position.y, 0);

            if (Mathf.Abs(inputDirection.x) >= 0.1 && Mathf.Abs(inputDirection.y) < 0.1)
            { // Moving horizontally, NO DIAGONALS
                isUsingLadder = false;
            }
            else if (Mathf.Abs(inputDirection.y) >= 0.1)
            { // Moving vertically
                transform.position = onLadder;
                isGrounded = false;
                isUsingLadder = true;
                rb.velocity = Vector2.zero;
                if (refreshDoubleJumpOnLadder)
                {
                    doubleJumpAvailable = true;
                }
                if (refreshGrappleOnLadder)
                {
                    //myGrapple.TryRefreshOnLand();
                }
                transform.position = transform.position + (inputDirection.y * ladderClimbSpeed * Vector3.up); // climb
            }
            else if (isUsingLadder == true)
            { // Not moving on ladder
                transform.position = onLadder;
                isGrounded = false;
                rb.velocity = Vector2.zero;
                if (refreshDoubleJumpOnLadder)
                {
                    doubleJumpAvailable = true; // Refresh double jump when on ladder
                }
                if (refreshGrappleOnLadder)
                {
                    //myGrapple.TryRefreshOnLand(); // Refresh grapple when on ladder
                }
            }


        }
    }
     // TODO: May need to remove this for proper ladder behaviour
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == ladderLayer)
        {
            isUsingLadder = false;
            isCloseToLadder = false;
        }
    }

    #region State Machine

    [SerializeField] float groundDetectCircleRadius = 0.35f;
    public bool IsGrounded()
    {
        // Cast a ray straight down.
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, groundDetectCircleRadius, Vector2.down,
            groundDetectionDistance - groundDetectCircleRadius, groundLayerMask);
        // subtract circle radius so the max distance to detect ground is still equal to groundDetectionDistance


        if (hit.collider != null)
        {
            groundNormal = hit.normal;
            return true;
        }
        else
        {
            groundNormal = Vector2.up;
            return false;
        }

    }

    public void ApplyFallAccel()
    {
        rb.AddForce(Vector2.down * 9.8f * gravityMultiplier);
    }

    public void RunTowards(Vector2 input)
    {
        Vector2 horizInput = new Vector2(input.x, 0f); // ignore up & down inputs

        // if player is on a slope, make the angled move direction parallel to the slope
        Vector2 angledMoveDir = Vector3.ProjectOnPlane(horizInput, groundNormal).normalized;

        // if velocity is less than max speed or opposite to the move direction
        //TODO: slope movement can go over maxVelocity, fix it after testing
        if (Mathf.Abs(rb.velocity.x) < maxVelocity || Mathf.Clamp(rb.velocity.x, -1f, 1f) == -horizInput.x)
        {
            rb.AddForce(angledMoveDir * accelerationMultiplier);
        }
    }

    public void SlowDown()
    {
        if (rb.velocity.magnitude > 0.05f)
        {
            rb.AddForce(-rb.velocity * counterForceMult); // slow down
        }
        else
        {
            rb.velocity = Vector2.zero; // if speed is low enough, stop
        }
    }

    public void ApplyJumpForce()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForceMultiplier, ForceMode2D.Impulse);
    }

    public void DoubleJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForceMultiplier * relativeDoubleJumpForceMultiplier, ForceMode2D.Impulse);
    }

    public Vector2 GetVelocity() { return rb.velocity; }
    public void SetVelocity(Vector2 vel) { rb.velocity = vel; }

    public void StartDash(Vector2 dashDir)
    {
        rb.AddForce(dashDir * dashSpeedGain, ForceMode2D.Impulse);
    }

    public void EndDash()
    {
        float subtractedVelMagnitude = rb.velocity.magnitude - dashSpeedGain;
        if (subtractedVelMagnitude < maxVelocity)
            subtractedVelMagnitude = maxVelocity;

        // reduce velocity by the speed gained during the dash (but keep a minimum speed of maxVelocity)
        rb.velocity = rb.velocity.normalized * subtractedVelMagnitude;
    }

    #endregion
}
