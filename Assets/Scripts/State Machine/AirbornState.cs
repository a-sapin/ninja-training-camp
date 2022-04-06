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

        if (player.GetLocomotion().IsTouchingLadder() && IsInputLadder(player))
        {
            doubleJumpAvailable = true; // refresh DJ on ladder
            player.ChangeState(ladderGrab);
            return;
        }

        player.ChangeState(CheckDashInput(player, airdrift, airborn));

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
        if (player.GetInput().JumpButtonDown())
        {
            if (player.GetLocomotion().IsTouchingLadder())
            {
                doubleJumpAvailable = true;
                player.ChangeState(jumping);
                return;
            }
            else if (doubleJumpAvailable && player.HasDoubleJump())
            {
                player.CreateSmoke(true);
                doubleJumpAvailable = false;
                player.GetLocomotion().DoubleJump();
                //Debug.Log("DJ **********");
            }
        }
        
    }

    protected bool authoriseWallJump(PlayerManager player)
    {
        if (player.HasWallJump())
        {
            if (player.GetLocomotion().IsAgainstWall(Vector2.left) == true)
            {
                player.GetLocomotion().WallJump();
                return true;
            }
            else if (player.GetLocomotion().IsAgainstWall(Vector2.left) == true)
            {

            }
            else
            {

            }
        }

    }
}
