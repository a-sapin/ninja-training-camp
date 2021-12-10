using System.Collections;
using UnityEngine;


public class PowerRemover : MonoBehaviour
{

    [SerializeField] 
    private GameObject UICanvas;
    private GameObject transition;
    
    [Header("Order of powers to remove")]
    [Range(1,10)]
    [SerializeField] private int dash = 1;
    [Range(1, 10)]
    [SerializeField] private int doubleJump = 1;
    [Range(1, 10)]
    [SerializeField] private int grapple = 1;

    [Header("GameObject References")]
    [SerializeReference] private GameObject respawnLocation;
    [SerializeReference] private GameObject playerObject;

    [Header("Level Specific Values")]
    public int currentLoopCount = 0; // in other words, how many times the objective was touched
    [SerializeField] private int maxLoopCount = 0;

    private PlayerLocomotion playerLocomotion;
    private bool ended = false;

    // Start is called before the first frame update
    void Start()
    {
        playerLocomotion = playerObject.GetComponent<PlayerLocomotion>();

        // just to make sure the level doesn't end immediately
        if(maxLoopCount <= currentLoopCount)
        {
            maxLoopCount = currentLoopCount + 1;
        }

        transition = UICanvas.transform.Find("ShurikenTransition").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(maxLoopCount <= currentLoopCount && !ended)
        {
            ended = true;
            transition.SendMessage("DisplayScore");
            Time.timeScale = 0;
            //SceneManager.LoadScene("VictoryScreen");
        }
    }

    private void HandleRemoveDash()
    {
        if(dash == currentLoopCount)
        {
            playerLocomotion.RemoveDash();
            transition.SendMessage("RemoveDash");
        }
    }

    private void HandleRemoveDoubleJump()
    {
        if (doubleJump == currentLoopCount)
        {
            playerLocomotion.RemoveDoubleJump();
            transition.SendMessage("RemoveDoubleJump");
        }
    }

    private void HandleRemoveGrapple()
    {
        if (grapple == currentLoopCount)
        {
            playerLocomotion.RemoveGrapple();
            transition.SendMessage("RemoveGrapple");
        }
    }

    private void ResetPlayer()
    {
        playerLocomotion.ResetPlayerAndPosition(respawnLocation.transform.position);
    }

    private const int playerLayer = 3;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
        {
            currentLoopCount++;
            if(maxLoopCount > currentLoopCount){
                transition.SendMessage("DoTransition");
                StartCoroutine(Waiter());
            }
        }
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSecondsRealtime(0.55f);
        HandleRemoveDash();
        HandleRemoveDoubleJump();
        HandleRemoveGrapple();
        yield return new WaitForSecondsRealtime(3.5f);
        transition.SendMessage("FadeOut", false);
        ResetPlayer();
        yield return new WaitForSecondsRealtime(1);
    }

}
