using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;


    Rigidbody2D rb;
    Vector2 moveDirection;
    Vector2 angledMoveDir;
    Vector2 inputDirection;
    Vector3 groundNormal;
    bool wantsToJump;
    public bool isGrounded;
    bool isInputingMove;
    bool hasDashPower;
    bool hasDoubleJumpPower;

    bool isDashing;
    float dashCooldownTimer;

    public float dashCooldown = 1.0f;
    public float dashDuration = 0.2f;

    public float gravityMultiplier = 1.0f;
    public float maxVelocity = 5.0f;
    public float accelerationMultiplier = 1.0f;
    public float jumpForceMultiplier = 10.0f;
    public float counterForceMult = 1.0f;
    public float dashSpeedGain = 40.0f;

    public float groundDetectionDistance = 1.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        wantsToJump = false;
        hasDashPower = true;
        hasDoubleJumpPower = true;

        dashCooldownTimer = 0.0f;
        isDashing = false;
    }

    

    // Update is called once per frame
    void Update()
    {
        inputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        moveDirection = new Vector2(Input.GetAxis("Horizontal"), 0);

        DetectGround();



        if (moveDirection.Equals(Vector2.zero))
            isInputingMove = false;
        else
            isInputingMove = true;

        if (Input.GetAxis("Jump") > 0)
            wantsToJump = true;
        else
            wantsToJump = false;

        
    }

    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;
        ApplyGravity();
        ApplyMovement();
        TryToJump();
        HandleDashing(delta);
    }

    private void ApplyMovement()
    {
        // if velocity is less than max speed or opposite to the move direction
        if (Mathf.Abs(rb.velocity.x) < maxVelocity || Mathf.Clamp(rb.velocity.x, -1f, 1f) == -moveDirection.x)
        {
            rb.AddForce(angledMoveDir * accelerationMultiplier);
        }

        ApplyCounterForce();
    }

    private void TryToJump()
    {
        if (wantsToJump && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForceMultiplier, ForceMode2D.Impulse);
            wantsToJump = false;
        }
    }

    private void DetectGround()
    {
        // Cast a ray straight down.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundDetectionDistance, groundLayerMask);

        if (hit.collider != null)
        {
            isGrounded = true;
            groundNormal = hit.normal;
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
        if (!isGrounded)
            rb.AddForce(Vector2.down * 9.8f * gravityMultiplier);
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
    private void HandleDashing(float delta)
    {
        // the velocity gained while dashing, as a vector
        Vector2 boost = moveDirection.normalized * dashSpeedGain;

        if (hasDashPower && Input.GetAxis("Fire3") > 0 && dashCooldownTimer <= 0.0f && rb.velocity.magnitude < dashSpeedGain)
        {
            isDashing = true;
            rb.velocity += boost;
            //rb.AddForce(inputDirection.normalized * dashForceMult, ForceMode2D.Impulse);
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
}
