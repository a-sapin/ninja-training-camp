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
        //if(touchGround)
        //{
        vfxManager.Play("Movement");
        //}
    }

    /*private void DashSound() //Problème avec le son qui ne ce joue pas tout le temps donc j'ai réactiver son bout de script dans le playerManager
    {
        vfxManager.Stop("Movement");
        vfxManager.Play("Dash");
    }*/
    private void DoubleJumpSound() // ne fonctionne pas car l'état double jump ne s'active jamais... SAD :(
    {
        //vfxManager.StopAllSFXMovement();
        vfxManager.Play("Double Jump");

    }
    private void LadderSound() // ne fonctionne pas car l'état double jump ne s'active jamais... SAD :(
    {
        //vfxManager.StopAllSFXMovement();
        vfxManager.Play("Ladder");
    }
}
