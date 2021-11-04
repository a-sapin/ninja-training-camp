using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerRemover : MonoBehaviour
{
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void HandleRemoveDash()
    {
        if(dash == currentLoopCount)
        {
            playerLocomotion.RemoveDash();
        }
    }

    private void HandleRemoveDoubleJump()
    {
        if (doubleJump == currentLoopCount)
        {
            playerLocomotion.RemoveDoubleJump();
        }
    }

    private void HandleRemoveGrapple()
    {
        if (grapple == currentLoopCount)
        {
            playerLocomotion.RemoveGrapple();
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
            Debug.Log(currentLoopCount);
        }
        HandleRemoveDash();
        HandleRemoveDoubleJump();
        HandleRemoveGrapple();

        ResetPlayer();
    }

}
