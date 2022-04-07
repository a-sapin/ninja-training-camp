using System.Collections;
using UnityEngine;

public class PowerLossAnimCharacter : MonoBehaviour
{
    private Animator myAnimator;
    private VFXManager vfxManager;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myAnimator.fireEvents = false;
        vfxManager = FindObjectOfType<VFXManager>();
        StartCoroutine(Animate());
    }
    
    IEnumerator Animate()
    {
        yield return new WaitForSeconds(1.1f);
        myAnimator.fireEvents = true;
        myAnimator.SetBool("attack", true);
        yield return new WaitForSeconds(1.4f);
        myAnimator.SetBool("attack", false);
        
        myAnimator.fireEvents = false;
        yield return new WaitForSeconds(0.7f);
        myAnimator.SetBool("fadeOut", true);
    }

    public void attackSound()
    {
        vfxManager.Play("Sword");
    }
}
