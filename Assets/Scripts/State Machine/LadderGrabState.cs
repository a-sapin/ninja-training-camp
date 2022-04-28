using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderGrabState : State
{
    
    public override void HandleSurroundings(PlayerManager player)
    {
        if (!player.GetLocomotion().IsTouchingLadder()) // not touching ladder
        {
            player.ChangeState(MovementState(player));
        }
        else if (player.GetLocomotion().HorizDistanceToLadder() < 0.01f) // touching ladder and close enough
        {
            player.ChangeState(ladderClimb);
        }
    }

    public override void HandleInputs(PlayerManager player)
    {
        // Player inputs are locked in ladder grab state until close enough to center of ladder
    }

    public override void LogicUpdate(PlayerManager player)
    {
        player.GetLocomotion().MoveToLadder(Time.deltaTime);
    }

    public override void PhysicsUpdate(PlayerManager player)
    {
        player.GetLocomotion().SlowDown();
    }

    /// <summary>
    /// Checks if the player is moving grounded or airborn for proper 
    /// state transition.
    /// </summary>
    /// <param name="player"></param>
    /// <returns>The state running or airdrift</returns>
    protected State MovementState(PlayerManager player)
    {
        if (player.GetLocomotion().IsGrounded())
        {
            return running;
        }
        else
        {
            return airdrift;
        }
    }

}
