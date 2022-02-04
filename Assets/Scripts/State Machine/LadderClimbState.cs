using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClimbState : State
{
    private bool isLerping = true;
    public override void Enter(PlayerManager player)
    {
        base.Enter(player);
        // start of climbing animation
        // start Lerp to ladder
        isLerping = true;
    }

    public override void HandleSurroundings(PlayerManager player)
    {
        //if is pressing left or right and not lerping, go to airdrift
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
