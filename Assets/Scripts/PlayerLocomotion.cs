using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerLocomotion : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;

    Rigidbody2D rb;
    Vector3 groundNormal; // indicates the slope the player is on (equal to Vector2.up when airborn)

    Ladder currentLadderObject;
    FootstepHandler footsteps;

    public float gravityMultiplier = 1.0f;
    public float maxVelocity = 5.0f;
    public float accelerationMultiplier = 1.0f;
    public float jumpForceMultiplier = 10.0f;
    public float relativeDoubleJumpForceMultiplier = 0.75f; // is relative to jumpForceMultiplier
    public float counterForceMult = 10000.0f;
    public float dashSpeedGain = 40.0f;
    public float ladderClimbSpeed = 1.0f;
    public float groundDetectionDistance = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        footsteps = GetComponent<FootstepHandler>();
    }

    public bool IsTouchingLadder() { return currentLadderObject != null; }

    /// <summary>
    /// Used when external actor or component applies an impulse force to the player.
    /// </summary>
    public void ApplyExternForce(Vector2 impulseForce)
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(impulseForce, ForceMode2D.Impulse);
    }

    public void ResetPlayerAndPosition(Vector2 position)
    {
        // myGrapple.ForceDetachGrapple();
        rb.velocity = Vector3.zero; // reset velocity

        transform.position = position;
    }

    private const int LadderLayer = 9; // for some reason a layermask doesnt work

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LadderLayer)
        {
            currentLadderObject = collision.gameObject.GetComponent<Ladder>();
        }
    }

    // TODO: May need to remove this for proper ladder behaviour
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LadderLayer)
        {
            currentLadderObject.EnableTopPlatform();
            currentLadderObject = null;
        }
    }
    #region State Machine

    [SerializeField] float groundDetectCircleRadius = 0.35f;
    public bool IsGrounded()
    {
        // Cast a ray straight down.
        Vector3 position = transform.position;
        RaycastHit2D hit = Physics2D.CircleCast(position, groundDetectCircleRadius, Vector2.down,
            groundDetectionDistance - groundDetectCircleRadius, groundLayerMask);
        // subtract circle radius so the max distance to detect ground is still equal to groundDetectionDistance
        Debug.DrawRay(position, Vector2.down * groundDetectionDistance, Color.red, 0.02f);

        if (hit.collider != null)
        {
            groundNormal = hit.normal;
            UpdateGroundTypeFromHit(hit);
            Debug.DrawRay(hit.point, groundNormal, Color.green, 0.02f);
            return true;
        }
        else
        {
            groundNormal = Vector2.up;
            Debug.DrawRay(hit.point, groundNormal, Color.green, 0.02f);
            return false;
        }
    }

    private TileBase groundTile; // used for footstep sfx
    public void UpdateGroundTypeFromHit(RaycastHit2D hit)
    {
        Tilemap tileMap;

        if(hit.collider.TryGetComponent(out tileMap))
        {
            Vector2 hitPos = hit.point - new Vector2(0f, 0.1f); // offset the point slightly down to make sure we are not above the tile we want
            Vector3Int tilePos = tileMap.WorldToCell(hitPos);
            TileBase tileBase = tileMap.GetTile(tilePos);

            if (groundTile != tileBase) // if ground type changed, change to new ground type
            {
                groundTile = tileBase;
                UpdateGroundType(tileBase);
            }
        }
        else if (hit.collider.TryGetComponent<PlatformEffector2D>(out _))
        {
            groundTile = null; // TODO: a ladder was detected!! maybe wood?
        }
        else
        {
            groundTile = null;
        }
    }

    private GroundType groundType = GroundType.Null;
    public GroundType GetGroundType() { return groundType; }
    private void UpdateGroundType(TileBase tile)
    {
        groundType = footsteps.DetermineGroundType(tile);
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
            Debug.DrawRay(transform.position, angledMoveDir * accelerationMultiplier, Color.blue, 0.1f);
        }
    }

    public void ApplySlideFriction()
    {
        if (Mathf.Abs(rb.velocity.x) > maxVelocity * 1.08f) // TODO: remove magic number
        {
            rb.AddForce(-rb.velocity * counterForceMult); // slow down
        }
    }

    public void SlowDown()
    {
        if (rb.velocity.magnitude > 0.4f)
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

    public float HorizDistanceToLadder()
    {
        if (IsTouchingLadder())
        {
            // the position horizontally aligned with the ladder that is closest to player
            Vector3 position = transform.position;
            Vector2 closestLadderMid = new Vector2(currentLadderObject.transform.position.x, position.y);

            return Vector2.Distance(closestLadderMid, position);
        }

        return 0f;
    }

    public void StopPlayer()
    {
        rb.velocity = Vector2.zero;
    }

    [SerializeField] float ladderGrabSpeed = 1.0f;
    public void MoveToLadder(float delta)
    {
        if (IsTouchingLadder())
        {
            Vector3 position = transform.position;
            Vector2 closestLadderMid = new Vector2(currentLadderObject.transform.position.x, position.y);
            position = Vector2.MoveTowards(position, closestLadderMid, ladderGrabSpeed * delta);
            transform.position = position;
        }
    }

    public void DisablePlatform()
    {
        // Cast a circle straight down.
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, groundDetectCircleRadius, Vector2.down,
            groundDetectionDistance - groundDetectCircleRadius, groundLayerMask);
        // subtract circle radius so the max distance to detect ground is still equal to groundDetectionDistance

        if (hit.collider != null)
        {
            if(hit.collider.gameObject.TryGetComponent(out PlatformEffector2D effector))
            {
                effector.gameObject.GetComponentInParent<Ladder>().DisableTopPlatform();
            }
            
        }
    }

    public void ClimbLadder(Vector2 input, float delta)
    {
        if(IsGrounded())
        {
            ApplyFallAccel(); // make sure player doesn't float above ground
            if(input.y < 0f)
                return; // dont let player clip through ground
        }

        Vector3 position = transform.position;
        Vector2 climbOffset = position + (input.y * 10f * Vector3.up); 
        // 10f just to make sure the target is not reachable in a single update
        
        position = Vector2.MoveTowards(position, climbOffset, ladderClimbSpeed * delta);
        transform.position = position;
    }

    #endregion
    
    // DONT USE THIS, IT WILL BE REMOVED
    public IEnumerator PlayerKnockback(float knockbackDuration, float knockbackPower, Vector2 knockbackDirection)
    {
        float timer = 0f;
        //grapple.StopGrappling();
        while (knockbackDuration > timer) // BRO WHAT IS THIIIIIIIIIIIIIS
        {
            // currently, what this does is apply a force over a single frame. This loop is never paused until the next frame
            // so all the force is applied at once. Also, multiplying x and y components by a scalar is kinda strange, 
            // since you can simply multiply a vector by a scalar and get the same result.

            //canGrapple = false;
            timer += Time.deltaTime; // AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
            rb.AddForce(new Vector2(knockbackDirection.x * -knockbackPower, knockbackDirection.y * -knockbackPower ));
            // unless it is an impulse, you want to apply forces inside the FixedUpdate() function
            // all coroutines are technically inside the Update() function 
            // also why is the a minus sign? shouldn't the knockbackDirection argument already be in the right direction?
        }

        //canGrapple = true;
        yield return 0;
    }

}
