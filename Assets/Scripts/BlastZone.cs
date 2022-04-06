using UnityEngine;

public class BlastZone : MonoBehaviour
{
    public float blastZoneYLevel;
    [Header("GameObject References")]
    public GameObject respawnLocation;

    [Header("Level Specific Values")]
    public int currentDeathCount; // in other words, how many times the objective was touched
    private PlayerLocomotion playerLocomotion;
    private PlayerManager player;

    private Transition transition;

    private bool isTransition;
    // Start is called before the first frame update
    private void Start()
    {
        transition = FindObjectOfType<Transition>();
        player = FindObjectOfType<PlayerManager>();
        playerLocomotion = FindObjectOfType<PlayerLocomotion>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (gameObject.transform.position.y < blastZoneYLevel && !isTransition)
        {
            ScreenShake.Shake(0.3f, 3f);
            player.LockGameplayInput();
            transition.TransitToCanvas(respawnLocation, respawnLocation);
            isTransition = true;
            Invoke(nameof(Waiter), 1f);
            
            currentDeathCount++;
        }
    }
    
    private void Waiter()
    {
        Respawn();
        isTransition = false;
    }

    /// <summary>
    /// Resets the player position to the respawnLocation
    /// </summary>
    public void Respawn()
    {
        player.UnlockGameplayInput();
        playerLocomotion.ResetPlayerAndPosition(respawnLocation.transform.position);
    }
    
    public void RespawnPlayer()
    {
        isTransition = true;
        Waiter();
        currentDeathCount++;

    }
}
