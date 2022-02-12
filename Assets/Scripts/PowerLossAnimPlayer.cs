using System.Collections;
using UnityEngine;

public class PowerLossAnimPlayer : MonoBehaviour
{
    private Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myAnimator.fireEvents = false;
        StartCoroutine(Animate());
    }
    
    IEnumerator Animate()
    {
        yield return new WaitForSeconds(1f);
        myAnimator.SetBool("attack", true);
        yield return new WaitForSeconds(0.5f);
        myAnimator.SetBool("attack", false);
        yield return new WaitForSeconds(0.3f);
        myAnimator.SetBool("fadeOut", true);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    public void fadeIn()
    {
        myAnimator.enabled = false;
        myAnimator.enabled = true;
    }

    public IEnumerator attackAnim()
    {
        myAnimator.SetBool("attack", true);
        yield return new WaitForSeconds(0.5f);
        myAnimator.SetBool("attack", false);
    }

    public IEnumerator fadeOut()
    {
        myAnimator.SetBool("fadeOut", true);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
