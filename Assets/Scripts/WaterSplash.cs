using System;
using UnityEngine;

public class WaterSplash : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;

    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerManager.CreateWaterSplash(false, collider.ClosestPoint(collider.transform.position));
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerManager.CreateWaterSplash(true, collider.ClosestPoint(collider.transform.position));
        }
    }
}
