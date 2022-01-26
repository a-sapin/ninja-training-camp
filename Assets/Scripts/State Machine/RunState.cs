using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// When the player is grounded and trying to run
/// </summary>
public class RunState : GroundedState
{
    public override void HandleSurroundings(PlayerManager player)
    {
        if (player.GetLocomotion().IsGrounded())
        {
            // if ground is detected, keep running
        }
        else
        {
            player.ChangeState(airdrift);
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
            if (player.GetInput().Dash() && player.HasDash())
            {
                player.ChangeState(dashing);
                return; // dash state is higher priority, so return is called
            }
            // otherwise keep moving by staying in this state
        }
        else
        {
            player.ChangeState(grounded);
            return;
        }
    }

    public override void LogicUpdate(PlayerManager player)
    {

    }

    public override void PhysicsUpdate(PlayerManager player)
    {
        player.GetLocomotion().RunTowards(player.GetInput().Move());
    }
}
