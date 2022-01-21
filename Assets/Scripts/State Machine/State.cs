using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The base class for all state objects
/// </summary>
public abstract class State : ScriptableObject
{
    protected float startTime = 0f;

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
