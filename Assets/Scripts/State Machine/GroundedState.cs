using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// When the player is touching the ground and not trying to move
/// </summary>
public class GroundedState : State
{
    public override void HandleSurroundings(PlayerManager player)
    {
        if (player.GetLocomotion().IsGrounded())
        {
            // if ground is detected, stay grounded
        }
        else
        {
            player.ChangeState(airborn);
        }
    }

    public override void HandleInputs(PlayerManager player)
    {
        if (player.GetInput().Jump())
        {
            player.ChangeState(jumping);
            return;
        }

        if (player.GetInput().IsMoveInput())
        {
            player.ChangeState(running);
            return;
        }
    }

    public override void LogicUpdate(PlayerManager player)
    {
        
    }

    public override void PhysicsUpdate(PlayerManager player)
    {
        player.GetLocomotion().SlowDown();
    }
}
