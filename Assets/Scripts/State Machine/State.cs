using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The base class for all state objects
/// </summary>
public abstract class State : ScriptableObject
{
    protected float startTime = 0f;

    // All state instances, used when changing state
    public static GroundedState grounded = CreateInstance<GroundedState>();
    protected static RunState running = CreateInstance<RunState>();
    protected static JumpState jumping = CreateInstance<JumpState>();
    protected static AirbornState airborn = CreateInstance<AirbornState>();
    protected static AirDriftState airdrift = CreateInstance<AirDriftState>();
    protected static DashState dashing = CreateInstance<DashState>();

    public virtual void Enter(PlayerManager player)
    {
        startTime = Time.time;
        Debug.Log("Entering " + ToString());
    }

    public virtual void Exit(PlayerManager player)
    {

    }

    /// <summary>
    /// Handles player surroundings (ground detection raycasts, out of bounds check, etc.)
    /// <param name="player">The player that is in this state.</param>
    public virtual void HandleSurroundings(PlayerManager player) { }

    /// <summary>
    /// Handles player inputs.
    /// <param name="player">The player that is in this state.</param>
    public virtual void HandleInputs(PlayerManager player) { }

    /// <summary>
    /// Call this function in Update().
    /// </summary>
    /// <param name="player">The player that is in this state.</param>
    public virtual void LogicUpdate(PlayerManager player) { }

    /// <summary>
    /// Call this function in FixedUpdate().
    /// </summary>
    /// <param name="player">The player that is in this state.</param>
    public virtual void PhysicsUpdate(PlayerManager player) { }

    /// <summary>
    /// Determines the state to switch to depending on movement and dash input.
    /// DOES NOT CHANGE THE STATE.
    /// </summary>
    /// <param name="player">The playerManager context.</param>
    /// <param name="noDashInputNextState">State to return when trying to move, but not dashing.</param>
    /// <param name="noMoveInputNextState">State to return when no move input is detected.</param>
    /// <returns>The next state, either dashing, noDashInputNextState, or noMoveInputNextState</returns>
    protected State CheckDashInput(PlayerManager player, State noDashInputNextState, State noMoveInputNextState)
    {
        if (player.GetInput().IsMoveInput())
        {
            if (player.GetInput().Dash() && player.CanDash())
            {
                return dashing; // dash state is higher priority, so return is called
            }
            else
            {
                return noDashInputNextState;
            }
        }
        else
        {
            return noMoveInputNextState;
        }
    }

    protected bool IsInputingRight(PlayerManager player)
    {
        if(player.GetInput().Move().x > 0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Check if the player is holding the button / joystick
    /// to point up.
    /// </summary>
    /// <param name="player"></param>
    /// <returns>True if player is holding up, False otherwise.</returns>
    protected bool IsInputLadder(PlayerManager player)
    {
        Vector2 input = player.GetInput().Move();
        
        // if holding diagonal, accept only diagonal close enough to y-axis than x axis.
        // Also with minimum y input
        return Mathf.Abs(input.y) >= Mathf.Abs(input.x) && Mathf.Abs(input.y) >= 0.2f; // TODO: magic numberrrrrrr
    }
}
