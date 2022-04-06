using System.Collections;
using UnityEngine;

public class PowerLossPowerAnim : MonoBehaviour
{
    [SerializeField] private GameObject topHalf;
    private Animator topHalfAnimator;
    
    [SerializeField] private GameObject bottomHalf;
    private Animator bottomHalfAnimator;
    
    [SerializeField] private GameObject text;
    private Animator textAnimator;
    private static readonly int Break = Animator.StringToHash("break");

    void Start()
    {
        topHalfAnimator = topHalf.GetComponent<Animator>();
        
        bottomHalfAnimator = bottomHalf.GetComponent<Animator>();

        textAnimator = text.GetComponent<Animator>();
        
        topHalfAnimator.fireEvents = false;
        bottomHalfAnimator.fireEvents = false;
        StartCoroutine(Animate());
    }
    
    IEnumerator Animate()
    {
        yield return new WaitForSeconds(1.45f);
        
        topHalfAnimator.SetBool(Break, true);
        bottomHalfAnimator.SetBool(Break, true);
        yield return new WaitForSeconds(1.5f);
        
        topHalfAnimator.SetBool(Break, false);
        bottomHalfAnimator.SetBool(Break, false);
        yield return new WaitForSeconds(0.35f);
        
        textAnimator.SetTrigger("FadeOut");
    }
}
