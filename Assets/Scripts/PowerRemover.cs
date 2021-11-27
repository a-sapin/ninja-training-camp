using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;


public class PowerRemover : MonoBehaviour
{

    [SerializeField] private GameObject UICanvas;
    private GameObject shurikenTransition;
    
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

    // Start is called before the first frame update
    void Start()
    {
        playerLocomotion = playerObject.GetComponent<PlayerLocomotion>();

        // just to make sure the level doesn't end immediately
        if(maxLoopCount <= currentLoopCount)
        {
            maxLoopCount = currentLoopCount + 1;
        }

        shurikenTransition = UICanvas.transform.Find("ShurikenTransition").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(maxLoopCount <= currentLoopCount)
        {
            shurikenTransition.SendMessage("displayScore");
            Time.timeScale = 0;
            //SceneManager.LoadScene("VictoryScreen");
        }
    }

    private void HandleRemoveDash()
    {
        if(dash == currentLoopCount)
        {
            playerLocomotion.RemoveDash();
            shurikenTransition.SendMessage("removeDash");
        }
    }

    private void HandleRemoveDoubleJump()
    {
        if (doubleJump == currentLoopCount)
        {
            playerLocomotion.RemoveDoubleJump();
            shurikenTransition.SendMessage("removeDoubleJump");
        }
    }

    private void HandleRemoveGrapple()
    {
        if (grapple == currentLoopCount)
        {
            playerLocomotion.RemoveGrapple();
            shurikenTransition.SendMessage("removeGrapple");
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
                shurikenTransition.SendMessage("doTransition");
                StartCoroutine(Waiter());
            }
        }
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        HandleRemoveDash();
        HandleRemoveDoubleJump();
        HandleRemoveGrapple();
        yield return new WaitForSecondsRealtime(4);
        shurikenTransition.SendMessage("fadeOut");
        ResetPlayer();
        yield return new WaitForSecondsRealtime(1);
    }

}
