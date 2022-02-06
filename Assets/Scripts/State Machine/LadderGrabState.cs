using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderGrabState : State
{
    float startDistance = 0f;

    public override void Enter(PlayerManager player)
    {
        base.Enter(player);

        // TODO: start of idle ladder animation

        player.GetLocomotion().StopPlayer();
        startDistance = player.GetLocomotion().DistanceToLadder();
    }

    public override void HandleSurroundings(PlayerManager player)
    {
        if (player.GetLocomotion().TouchingLadder() == null)
        {
            if (player.GetLocomotion().IsGrounded())
            {
                player.ChangeState(running);
                return;
            }
            else
            {
                player.ChangeState(airdrift);
                return;
            }
        }
    }

    public override void HandleInputs(PlayerManager player)
    {
        // if holding up or down, climb ladder
    }

    public override void LogicUpdate(PlayerManager player)
    {
        // if is lerping, lerp
        // otherwise move up, down or not at all
    }

    public override void PhysicsUpdate(PlayerManager player)
    {
        // do we need this function?
    }
}
