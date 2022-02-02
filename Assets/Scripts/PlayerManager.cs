using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerLocomotion myPlayerLocomotion;
    [SerializeField] State currentState;
    InputManager inputManager;
    [SerializeReference] Animator myAnimator;
    VFXManager mySoundManager;

    [Header("Available Powers")] // TODO: these should (ideally) be set by the level, not in Start() or Awake() functions
    [SerializeField] bool hasDashPower = false;
    [SerializeField] bool hasDoubleJumpPower = false;
    [SerializeField] bool hasGrapplePower = false;

    [Header("Power Logic Variables")]
    [SerializeField] float dashCooldown = 1.0f;
    public float DashCooldown { get { return dashCooldown; } }

    [SerializeField] float dashDuration = 0.2f;
    public float DashDuration { get { return dashDuration; } }

    // getters
    public PlayerLocomotion GetLocomotion() { return myPlayerLocomotion; }
    public InputManager GetInput() { return inputManager; }

    public bool HasDash() { return hasDashPower; }
    public bool HasDoubleJump() { return hasDoubleJumpPower; }
    public bool HasGrapple() { return hasGrapplePower; }


    // Used by other scripts to take away powers
    public void RemoveDash() { hasDashPower = false; }
    public void RemoveDoubleJump() { hasDoubleJumpPower = false; }
    public void RemoveGrapple() { hasGrapplePower = false; }

    /// <summary>
    /// Properly changes the state of the player by calling Exit() function 
    /// of the current state and Enter() function of the next state.
    /// </summary>
    /// <param name="nextState">The next state to switch to.</param>
    public void ChangeState(State nextState)
    {
        if(currentState.GetType() == nextState.GetType())
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

    private bool canDash = true;
    public bool CanDash()
    {
        if (!hasDashPower)
            return false; // no dash power, no need to calculate cooldown

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

    void Start()
    {
        mySoundManager = FindObjectOfType<VFXManager>();
        myPlayerLocomotion = GetComponent<PlayerLocomotion>();
        inputManager = GetComponent<InputManager>();
        currentState = State.grounded; // set a default state at the start
    }

    void Update()
    {
        currentState.HandleSurroundings(this);
        currentState.HandleInputs(this);
        currentState.LogicUpdate(this);
    }

    void FixedUpdate()
    {
        currentState.PhysicsUpdate(this);
    }

    #region Animation

    public void SetAnimIdle()
    {
        myAnimator.SetBool("runLeft", false); // not running
        myAnimator.SetBool("runRight", false);

        myAnimator.SetBool("jump", false); // not jumping (left or right)
        myAnimator.SetBool("jumpL", false);// so grounded or on ladder

        myAnimator.SetBool("dash", false); // not dashing
    }

    public void SetAnimRun(bool runRight)
    {
        myAnimator.SetBool("runLeft", !runRight);
        myAnimator.SetBool("runRight", runRight);
    }

    public void SetAnimDashTo(bool setBoolToThis) 
    {
        myAnimator.SetBool("dash", setBoolToThis);
    }

    public void SetAnimJump(bool inputRight)
    {
        // empty for now
        // TODO: add proper jump animation
    }
    #endregion

    #region Sound Effects

    public void PlayJumpSound()
    {
        mySoundManager.Play("Jump");
    }

    public void PlayDashSound()
    {
        mySoundManager.Play("Dash");
    }

    #endregion

}
