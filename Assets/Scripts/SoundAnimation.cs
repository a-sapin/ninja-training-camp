using UnityEngine;

public class SoundAnimation : MonoBehaviour
{
    public PlayerLocomotion touchGround;

    private void IdleNoSound()
    {
        FindObjectOfType<VFXManager>().Stop("Movement");
    }
    private void JumpSound()
    {
        FindObjectOfType<VFXManager>().Stop("Movement");
    }
    private void MovementSound()
    {/*
        if(touchGround.isGrounded)
        {
            FindObjectOfType<VFXManager>().Play("Movement");
        }*/
    }
    private void DashSound()
    {
        FindObjectOfType<VFXManager>().Stop("Movement");
    }
}
