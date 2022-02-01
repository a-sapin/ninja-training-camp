using System;
using UnityEngine;

[Serializable]public enum Power {Dash,DoubleJump,Grapple}

public class PowerRemover : MonoBehaviour
{
    [SerializeField] Power[] powerToRemove;
    int currentIndex = 0;
    EndLevel endLevel;
    PlayerLocomotion player;
    private void Start()
    {
        player = FindObjectOfType<PlayerLocomotion>();
        endLevel = FindObjectOfType<EndLevel>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            RemovePower();
        }
    }

    private void RemovePower()
    {
        if (currentIndex >= powerToRemove.Length)
        {
            endLevel.DisplayEnd();
            return;
        }
        switch (powerToRemove[currentIndex])
        {
            case Power.Dash:
                endLevel.DisplayDashLost();
                player.RemoveDash();
                break;
            case Power.DoubleJump:
                endLevel.DisplayDoubleJumpLost();
                player.RemoveDoubleJump();
                break;
            case Power.Grapple:
                endLevel.DisplayGrappleLost();
                player.RemoveGrapple();
                break;
            default:
                break;
        }
        Invoke(nameof(ResetPlayerPos), 1f);
        currentIndex++;
    }
    private void ResetPlayerPos()
    {
        player.ResetPlayerAndPosition(FindObjectOfType<BlastZone>().respawnLocation.transform.position);
    }
}
