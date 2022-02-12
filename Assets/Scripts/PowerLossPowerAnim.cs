using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerLossPowerAnim : MonoBehaviour
{
    [SerializeField] private Animator upHalfAnimator;
    [SerializeField] private Animator downHalfAnimator;
    // Start is called before the first frame update
    void Start()
    {
        upHalfAnimator.fireEvents = false;
        downHalfAnimator.fireEvents = false;
        StartCoroutine(Animate());
    }
    
    IEnumerator Animate()
    {
        yield return new WaitForSeconds(1.5f);
        
        upHalfAnimator.SetBool("break", true);
        downHalfAnimator.SetBool("break", true);
        yield return new WaitForSeconds(1.5f);
        
        upHalfAnimator.SetBool("break", false);
        downHalfAnimator.SetBool("break", false);
        //yield return new WaitForSeconds(0.5f);
        
        //upHalfAnimator.SetBool("flash", false);
        //downHalfAnimator.SetBool("flash", false);
        //yield return new WaitForSeconds(1f);
        //gameObject.SetActive(false);
    }
}
