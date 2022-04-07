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
            else if (AuthoriseWallJump(player)==false && doubleJumpAvailable && player.HasDoubleJump())
            {
                player.CreateSmoke(true);
                doubleJumpAvailable = false;
                player.SetTriggerDoubleJump();
                player.GetLocomotion().DoubleJump();
                //Debug.Log("DJ **********");
            }
        }
        
    }

    protected bool AuthoriseWallJump(PlayerManager player)
    {
        //Debug.Log("Attempt to check for wall jump conditions [ AirbornState.cs ]");
        if (player.HasWallJump())
        {
            if (player.GetLocomotion().IsAgainstWall(Vector2.left) == true && player.isSpriteFlipped() == true)
            {
                player.GetLocomotion().WallJump(new Vector2(1,0.8f));
                //Debug.Log("Successful wall jump [ AirbornState.authoriseWallJump() ]");
                player.NeutralFlip();
                player.CreateSmoke(true);
                return true;
            }
            else if (player.GetLocomotion().IsAgainstWall(Vector2.right) == true && player.isSpriteFlipped() == false)
            {
                player.GetLocomotion().WallJump(new Vector2(-1, 0.8f));
                //Debug.Log("Successful wall jump [ AirbornState.authoriseWallJump() ]");
                player.NeutralFlip();
                player.CreateSmoke(true);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            Debug.Log("Player's wall jump power is disabled [ AirbornState.cs ]");
        }
        return false;

    }
}
