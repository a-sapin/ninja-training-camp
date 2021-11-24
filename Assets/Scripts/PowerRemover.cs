using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;


public class PowerRemover : MonoBehaviour
{

    public GameObject shurikenTransition;
    
    [Header("Order of powers to remove")]
    [Range(1,10)]
    [SerializeField] private int dash = 1;
    public RawImage dashImage;
    [Range(1, 10)]
    [SerializeField] private int doubleJump = 1;
    public RawImage doubleJumpImage;
    [Range(1, 10)]
    [SerializeField] private int grapple = 1;
    public RawImage grappleImage;

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
    }

    // Update is called once per frame
    void Update()
    {
        if(maxLoopCount <= currentLoopCount)
        {
            SceneManager.LoadScene("VictoryScreen");
        }
    }

    private void HandleRemoveDash()
    {
        if(dash == currentLoopCount)
        {
            playerLocomotion.RemoveDash();
            try
            {
                dashImage.enabled = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    private void HandleRemoveDoubleJump()
    {
        if (doubleJump == currentLoopCount)
        {
            playerLocomotion.RemoveDoubleJump();
            try
            {
                doubleJumpImage.enabled = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    private void HandleRemoveGrapple()
    {
        if (grapple == currentLoopCount)
        {
            playerLocomotion.RemoveGrapple();
            try
            {
                grappleImage.enabled = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
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
            Time.timeScale = 0;
            StartCoroutine(ShurikenTransition());
        }
        HandleRemoveDash();
        HandleRemoveDoubleJump();
        HandleRemoveGrapple();

    }

    IEnumerator ShurikenTransition()
    {
        {
            Vector3 currentPos = shurikenTransition.transform.position;
            float t = 0f;
            while(t < 1)
            {
                t += Time.fixedDeltaTime / 10;
                shurikenTransition.transform.position = Vector3.Lerp(currentPos, new Vector3(-(Screen.width+100), 540, 0), t);
                yield return null;
            }
            
            t = 0f;
            while(t < 1)
            {
                t += Time.fixedDeltaTime / 10;
                shurikenTransition.transform.GetChild(0).GetComponent<RawImage>().color = new Color(0, 0, 0, 1-t);
                yield return null;
            }
            
            shurikenTransition.transform.position = new Vector3(960, 540, 0);
            shurikenTransition.transform.GetChild(0).GetComponent<RawImage>().color = new Color(0, 0, 0, 1);
            
            
            ResetPlayer();
            Time.timeScale = 1;
        }
    }

}
