using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : AirDriftState
{
    public override void Enter(PlayerManager player)
    {
        base.Enter(player);
        player.GetLocomotion().ApplyJumpForce();
    }

    public override void HandleSurroundings(PlayerManager player)
    {
        // when jumping, dont detect ground

        if(Time.time - startTime >= 0.07f)
        { // stay in this state for a short time (to avoid applying a jump many frames in a row)
            player.ChangeState(airdrift);
        }
    }

    public override void HandleInputs(PlayerManager player)
    {
        if (player.GetInput().IsMoveInput())
        {
            if (player.GetInput().Dash() && player.HasDash())
            {
                player.ChangeState(dashing);
                return; // dash state is higher priority, so return is called
            }
        }
        else
        {
            player.ChangeState(airborn);
        }

        // cannot double jump in this state, to avoid consuming double jump 
    }

    public override void LogicUpdate(PlayerManager player)
    {

    }
}
