using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// When in the air and moving
/// </summary>
public class AirDriftState : AirbornState
{
    public override void HandleSurroundings(PlayerManager player)
    {
        if (player.GetLocomotion().IsGrounded())
        {
            doubleJumpAvailable = true; // refresh DJ on land
            player.ChangeState(running);
        }
    }

    public override void LogicUpdate(PlayerManager player)
    {

    }

    public override void PhysicsUpdate(PlayerManager player)
    {
        base.PhysicsUpdate(player);
        player.GetLocomotion().RunTowards(player.GetInput().Move());
    }
}
