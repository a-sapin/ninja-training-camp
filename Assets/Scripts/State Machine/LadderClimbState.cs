using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClimbState : LadderGrabState
{
    public override void HandleSurroundings(PlayerManager player)
    {
        if (!player.GetLocomotion().IsTouchingLadder()) // not touching ladder
        {
            player.ChangeState(MovementState(player));
        }
    }

    public override void HandleInputs(PlayerManager player)
    {
        if (player.GetInput().JumpButtonDown())
        {
            player.ChangeState(jumping);
            return;
        }

        if (player.GetInput().IsMoveInput())
        { // if holding left or right, release from ladder
            player.ChangeState(MovementState(player));
            return;
        }
    }

    public override void LogicUpdate(PlayerManager player)
    {
        player.GetLocomotion().ClimbLadder(player.GetInput().Move(), Time.deltaTime);
    }

}
