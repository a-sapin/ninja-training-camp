using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerLocomotion myPlayerLocomotion;
    [SerializeField] State currentState;
    public State getState() { return currentState;}
    InputManager inputManager;
    [SerializeReference] Animator myAnimator;
    [SerializeReference] SpriteRenderer playerSprite;
    VFXManager mySoundManager;
    BlastZone myBlastZone; // handles the player respawning
    GrapplingGun myGrapplingGun;
    Smoke smoke;

    [Header("Available Powers")] // TODO: these should (ideally) be set by the level, not in Start() or Awake() functions
    [SerializeField] bool hasDashPower = false;
    [SerializeField] bool hasDoubleJumpPower = false;
    [SerializeField] bool hasGrapplePower = false;
    [SerializeField] bool hasWallJumpPower = false;


    [Header("Power Logic Variables")]
    [SerializeField] float dashCooldown = 1.0f;
    public float DashCooldown { get { return dashCooldown; } }

    [SerializeField] float dashDuration = 0.2f;
    public float DashDuration { get { return dashDuration; } }

    // getters
    public PlayerLocomotion GetLocomotion() { return myPlayerLocomotion; }
    public InputManager GetInput() { return inputManager; }
    public GrapplingGun GetGrapplingGun() { return myGrapplingGun; }
    public SpriteRenderer GetSpriteRenderer() { return playerSprite; }

    public bool HasDash() { return hasDashPower; }
    public bool HasDoubleJump() { return hasDoubleJumpPower; }
    public bool HasGrapple() { return hasGrapplePower; }
    public bool HasWallJump() { return hasWallJumpPower; }


    // Used by other scripts to take away powers
    public void RemoveDash() { hasDashPower = false; }
    public void RemoveDoubleJump() { hasDoubleJumpPower = false; }
    public void RemoveGrapple() { hasGrapplePower = false; }

    // when false, the player is locked and cannot act
    bool isActionable = true;
    
    public bool IsActionable() { return isActionable; }

    /// <summary>
    /// Properly changes the state of the player by calling Exit() function 
    /// of the current state and Enter() function of the next state.
    /// </summary>
    /// <param name="nextState">The next state to switch to.</param>
    public void ChangeState(State nextState)
    {
        if (currentState.GetType() == nextState.GetType())
        {
            return; // Don't change the state if we are already in that state
        }
        else
        {
            currentState.Exit(this);
            currentState = nextState;
            currentState.Enter(this);
        }
    }

    private bool canGrapple = true;
    public bool CanGrapple()
    {
        if (!hasGrapplePower)
            return false;
        else if (!isActionable) // player cannot grapple when locked
            return false;
        else
            return canGrapple;
    }

    public void SetCanGrapple(bool value) { canGrapple = value; }

    private bool canDash = true;
    /// <summary>
    /// Upon reading value to check if player is able to dash,
    /// disable dash for the duration of cooldown.
    /// </summary>
    /// <returns>TRUE if player can dash, or FALSE if unable or cooling down.</returns>
    public bool CanDash()
    {
        if (!hasDashPower || !isActionable)
            return false; // no dash power or not actionable, so no need to calculate cooldown

        if (canDash)
        {
            StartCoroutine(StartDashCooldown());
            return true;
        }
        return false;
    }

    private IEnumerator StartDashCooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    /// <summary>
    /// Careful using this, player must eventually be set
    /// to actionable after this function is called.
    /// </summary>
    public void LockGameplayInput()
    {
        isActionable = false;
        ChangeState(State.grounded); // force the grounded state to stop moving
    }

    public void UnlockGameplayInput()
    {
        isActionable = true;
    }

    /// <summary>
    /// Respawns the player to the current level's spawn point
    /// </summary>
    public void Respawn()
    {
        myBlastZone.Respawn();
    }
    public void CreateSmoke(bool doubleJump = false)
    {
        if (doubleJump) smoke.CreateDoubleJumpSmoke();
        else smoke.CreateJumpSmoke();
    }
    void Start()
    {
        mySoundManager = FindObjectOfType<VFXManager>();
        myPlayerLocomotion = GetComponent<PlayerLocomotion>();
        myBlastZone = GetComponent<BlastZone>();
        inputManager = GetComponent<InputManager>();
        myGrapplingGun = GetComponentInChildren<GrapplingGun>();
        smoke = GetComponent<Smoke>();
        canGrapple = true;
        currentState = State.grounded; // set a default state at the start
        isActionable = true;
    }

    void Update()
    {
        if (isActionable)
        {
            currentState.HandleSurroundings(this);
            currentState.HandleInputs(this);
            currentState.LogicUpdate(this);
        }
    }

    private void LateUpdate()
    {
        FlipSpriteTowardsMoveInput();
        SetBoolDash(currentState == State.dashing);
        SetBoolGrounded(currentState == State.grounded || currentState == State.running);
        SetBoolJump(currentState == State.jumping);
        SetBoolRun(isActionable && GetInput().IsMoveInput()); // disable run anim when locked
        SetIntLadderInput(GetInput().LadderInputDir());
        if(currentState != State.airborn && currentState != State.airdrift)
        {
            SetBoolWallSlide(false);
        }
        
    }

    void FixedUpdate()
    {
        currentState.PhysicsUpdate(this);
        SetFloatYVelocity(GetLocomotion().GetVelocity().y);
        SetBoolTouchLadder(GetLocomotion().IsTouchingLadder());
    }

    #region Animation

    public Animator GetAnimator() { return myAnimator; }

    public void SetBoolGrounded(bool value) { myAnimator.SetBool("isGrounded", value); }
    public void SetBoolRun(bool value) { myAnimator.SetBool("Run", value); }
    public void SetBoolDash(bool value) { myAnimator.SetBool("Dash", value); }
    public void SetBoolJump(bool value) { myAnimator.SetBool("Jump", value); }
    public void SetBoolGrapple(bool value) { myAnimator.SetBool("Grapple", value); myAnimator.SetTrigger("StartGrapple"); }
    public void SetBoolTouchLadder(bool value) { myAnimator.SetBool("TouchingLadder", value); }
    public void SetBoolWallSlide(bool value) { myAnimator.SetBool("WallSlide", value); }
    public void SetFloatYVelocity(float value) { myAnimator.SetFloat("Y_Velocity", value); }
    public void SetIntLadderInput(int value) { myAnimator.SetInteger("LadderInput", value); }
    public void SetTriggerDoubleJump() { myAnimator.SetTrigger("DoubleJump"); }

    /// <summary>
    /// Flips sprite to make player avater face the direction
    /// the move input is currently held.
    /// </summary>
    public void FlipSpriteTowardsMoveInput()
    {
        if (!isActionable) // don't flip player when locked
            return;

        if (GetInput().Move().x < 0f) // inputting left
        {
            playerSprite.flipX = true;
        }
        else if (GetInput().Move().x > 0f) // inputting right
        {
            playerSprite.flipX = false;
        }
    }

    // Will flip the sprite of the player regardless of any other factors, unlike FlipSpriteTowardsMoveDirection()
    public void NeutralFlip()
    {
        playerSprite.flipX = !playerSprite.flipX;
    }

    public bool isSpriteFlipped()
    {
        return playerSprite.flipX;
    }

    #endregion

    #region Sound Effects
    //I commented the lines to put the sounds all in the same place.

    /*public void PlayJumpSound()
    {
        mySoundManager.Play("Jump");
    }
    */
    public void PlayDashSound()
    {
        mySoundManager.Play("Dash");
    }

    #endregion

}
