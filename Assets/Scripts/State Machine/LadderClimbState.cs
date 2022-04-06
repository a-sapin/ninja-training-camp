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

    public override void LogicUpdate(PlayerManager player)
    {
        player.GetLocomotion().ClimbLadder(player.GetInput().Move(), Time.deltaTime);
    }

}
