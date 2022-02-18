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


    public override void LogicUpdate(PlayerManager player)
    {
        
    }

    public override void PhysicsUpdate(PlayerManager player)
    {
        player.GetLocomotion().RunTowards(player.GetInput().Move());
        player.GetLocomotion().ApplySlideFriction();
    }
}
