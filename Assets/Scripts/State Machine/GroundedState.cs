namespace State_Machine
{
    /// <summary>
    /// When the player is touching the ground and not trying to move
    /// </summary>
    public class GroundedState : State
    {
        public override void HandleSurroundings(PlayerManager player)
        {
            if (player.GetLocomotion().IsGrounded())
            {
                // if ground is detected, stay grounded
            }
            else
            {
                player.ChangeState(airborn);
            }
        }

        public override void HandleInputs(PlayerManager player)
        {
            if (IsInputLadder(player) && player.GetInput().Move().y < 0) // if holding down
            {
                player.GetLocomotion().DisablePlatform(); // let player fall through platform is standing on one
            }

            if (player.GetInput().Jump())
            {
                player.ChangeState(jumping);
                return;
            }

            if (player.GetLocomotion().IsTouchingLadder() && IsInputLadder(player))
            {
                player.ChangeState(ladderGrab);
                return;
            }

            player.ChangeState(CheckDashInput(player, running, grounded));
        }

        public override void LogicUpdate(PlayerManager player)
        {
        
        }

        public override void PhysicsUpdate(PlayerManager player)
        {
            player.GetLocomotion().SlowDown();
            player.GetLocomotion().ApplyFallAccel();
        }
    }
}
