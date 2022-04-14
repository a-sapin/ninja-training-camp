using System;
using UnityEngine;

public class WaterSplash : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;

    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Vector2 position = collider.ClosestPoint(collider.transform.position);
            print(position);
            playerManager.CreateWaterSplash(false, position);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Vector2 position = collider.ClosestPoint(collider.transform.position);
            print(position);
            playerManager.CreateWaterSplash(true, position);
        }
    }
}
