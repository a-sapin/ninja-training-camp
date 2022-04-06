using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// When the player is dashing
/// </summary>
public class DashState : AirDriftState
{
    Vector2 dashDirection = Vector2.zero;
    float horizontalVelocity;

    public override void Enter(PlayerManager player)
    {
        base.Enter(player);
        dashDirection = new Vector2(player.GetInput().Move().x, 0f); // save horizontal player input
        dashDirection.Normalize();

        player.PlayDashSound();
        // TODO: do we really need this?
        /*
        horizontalVelocity = player.GetLocomotion().GetVelocity().x; // save player horizontal velocity as they start dashing
        if(horizontalVelocity > player.GetLocomotion().maxVelocity)
        {
            horizontalVelocity = player.GetLocomotion().maxVelocity; // clamp velocity to max
        }*/
        player.GetLocomotion().StartDash(dashDirection);
    }

    public override void Exit(PlayerManager player)
    {
        player.GetLocomotion().EndDash();
    }

    public override void HandleSurroundings(PlayerManager player)
    {
        if(Time.time - startTime >= player.DashDuration)
        {
            base.HandleSurroundings(player); // return to airdrift or running state when dash is finished
        }
    }

    public override void HandleInputs(PlayerManager player)
    {
        if (player.GetLocomotion().IsTouchingLadder() && IsInputLadder(player))
        {
            player.ChangeState(ladderGrab);
            return;
        }
    }

    public override void LogicUpdate(PlayerManager player)
    {

    }

    public override void PhysicsUpdate(PlayerManager player)
    {

    }
}
