using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerLocomotion myPlayerLocomotion;
    [SerializeField] State currentState;
    InputManager inputManager;
    [SerializeReference] Animator myAnimator;

    [Header("Available Powers")] // TODO: these should (ideally) be set by the level, not in Start() or Awake() functions
    [SerializeField] bool hasDashPower = false;
    [SerializeField] bool hasDoubleJumpPower = false;
    [SerializeField] bool hasGrapplePower = false;

    // Used by other scripts to take away powers
    public void RemoveDash() { hasDashPower = false; }
    public void RemoveDoubleJump() { hasDoubleJumpPower = false; }
    public void RemoveGrapple() { hasGrapplePower = false; }

    [Header("Power Logic Variables")]
    [SerializeField] float dashCooldown = 1.0f;
    public float DashCooldown { get { return dashCooldown; } }

    [SerializeField] float dashDuration = 0.2f;
    public float DashDuration { get { return dashDuration; } }

    public PlayerLocomotion GetLocomotion() { return myPlayerLocomotion; }
    public InputManager GetInput() { return inputManager; }

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
            currentState.Exit();
            currentState = nextState;
            currentState.Enter();
        }
    }

    void Start()
    {
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
}
