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

    public virtual void Enter()
    {
        startTime = Time.time;
        Debug.Log("Entering " + ToString());
    }

    public virtual void Exit()
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
}
