using UnityEngine;

public class SoundAnimation : MonoBehaviour
{
    VFXManager vfxManager;
    PlayerLocomotion myPlayerLocomotion;

    private void Start()
    {
        vfxManager = FindObjectOfType<VFXManager>();
        myPlayerLocomotion = FindObjectOfType<PlayerLocomotion>();
    }
    private void IdleNoSound()
    {
        //vfxManager.StopAllSFXMovement();
    }
    private void JumpSound()
    {
        //vfxManager.StopAllSFXMovement();
        vfxManager.Play("Jump");
    }
    private void MovementSound()
    {
        switch (myPlayerLocomotion.GetGroundType())
        {
            case GroundType.GRASS:
                //play sound
                vfxManager.Play("Movement Grass");
                break;

            case GroundType.STONE:
                //play sound
                vfxManager.Play("Movement Stone");
                break;

            case GroundType.WOOD:
                //play sound
                vfxManager.Play("Movement Wood");
                break;

            case GroundType.DIRT:
                //play sound
                vfxManager.Play("Movement Dirt");
                break;

            default:
                break;
        }
    }
    private void DoubleJumpSound()
    {
        vfxManager.Play("Double Jump");
    }
    private void LadderSound()
    {
        vfxManager.Play("Ladder");
    }

    private void LeverSound()
    {
        vfxManager.Play("Lever");
    }

    private void DieSound()
    {
        vfxManager.Play("Player Die");
    }

    private void WallJumpSound()
    {
        vfxManager.Play("Wall Jump");
    }
}
