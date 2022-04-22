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
        CheckWallJumpLogic(player);
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
            { // wall jump does NOT consume DJ because WJ condition is verified before DJ
                player.CreateSmoke(true);
                doubleJumpAvailable = false;
                player.SetTriggerDoubleJump();
                player.GetLocomotion().DoubleJump();
                //Debug.Log("DJ **********");
            }
        }
        
    }

    /// <summary>
    /// Checks if an available wall is close enough to the player and if  
    /// player is facing the right way. Also sets the wall slide animation.
    /// </summary>
    /// <param name="player">The player manager in this context</param>
    /// <returns></returns>
    protected void CheckWallJumpLogic(PlayerManager player)
    {
        if (!player.HasWallJump()) // no power, no anim
        {
            player.SetBoolWallSlide(false);
            return;
        }

        if (player.GetLocomotion().IsAgainstWall(Vector2.left) && player.isSpriteFlipped())
        {
            player.SetBoolWallSlide(true);
        }
        else if (player.GetLocomotion().IsAgainstWall(Vector2.right) && !player.isSpriteFlipped())
        {
            player.SetBoolWallSlide(true);
        }
        else
        {
            player.SetBoolWallSlide(false);
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
                player.PlayWallJumpSound();
                return true;
            }
            else if (player.GetLocomotion().IsAgainstWall(Vector2.right) == true && player.isSpriteFlipped() == false)
            {
                player.GetLocomotion().WallJump(new Vector2(-1, 0.8f));
                //Debug.Log("Successful wall jump [ AirbornState.authoriseWallJump() ]");
                player.NeutralFlip();
                player.CreateSmoke(true);
                player.PlayWallJumpSound();
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
