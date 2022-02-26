using UnityEngine;

public class SoundAnimation : MonoBehaviour
{
    VFXManager vfxManager;
    public PlayerManager touchGround;

    private void Start()
    {
        vfxManager = FindObjectOfType<VFXManager>();
    }
    private void IdleNoSound()
    {
        vfxManager.Stop("Movement");
    }
    private void JumpSound()
    {
        vfxManager.Stop("Movement");
        vfxManager.Play("Jump");
    }
    private void MovementSound()
    {
        //if(touchGround.GetLocomotion().IsGrounded())
        //{
            vfxManager.Play("Movement");
        //}
    }

   private void MovementV2Sound()
    {
        //if (touchGround.GetLocomotion().IsGrounded())
        //{
            vfxManager.Play("Movement3");
        //}
    }

    /*private void DashSound() //Problème avec le son qui ne ce joue pas tout le temps donc j'ai réactiver son bout de script dans le playerManager
    {
        vfxManager.Stop("Movement");
        vfxManager.Play("Dash");
    }*/
    private void DoubleJumpSound() // ne fonctionne pas car l'état double jump ne s'active jamais... SAD :(
    {
        vfxManager.Stop("Movement");
        vfxManager.Play("Double Jump");

    }
}
