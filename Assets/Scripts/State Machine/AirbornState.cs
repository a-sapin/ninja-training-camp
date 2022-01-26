using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirbornState : State
{
    protected static bool doubleJumpAvailable = true; // keeps track of the player's double jump

    public override void HandleSurroundings(PlayerManager player)
    {
        if (player.GetLocomotion().IsGrounded())
        {
            doubleJumpAvailable = true; // refresh DJ on land
            player.ChangeState(grounded);
        }
    }

    public override void HandleInputs(PlayerManager player)
    {
        DoubleJumpLogic(player);

        if (player.GetInput().IsMoveInput())
        {
            player.ChangeState(airdrift);
            return;
        }
    }

    public override void LogicUpdate(PlayerManager player)
    {

    }

    public override void PhysicsUpdate(PlayerManager player)
    {
        player.GetLocomotion().ApplyFallAccel();
    }


    protected void DoubleJumpLogic(PlayerManager player)
    {
        if (player.GetInput().JumpButtonDown() && doubleJumpAvailable && player.HasDoubleJump())
        {
            doubleJumpAvailable = false;
            player.GetLocomotion().DoubleJump();
        }
    }
}
