using UnityEngine;

public class WaterSplash : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerManager.CreateWaterSplash();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerManager.CreateWaterSplash(true);
        }
    }
}
