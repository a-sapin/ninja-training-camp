using UnityEngine;

public class BlastZone : MonoBehaviour
{
    public float blastZoneYLevel;
    [Header("GameObject References")]
    public GameObject respawnLocation;

    [Header("Level Specific Values")]
    public int currentDeathCount = 0; // in other words, how many times the objective was touched
    private PlayerLocomotion playerLocomotion;
    private PlayerManager player;

    private Transition transition;

    private VFXManager vfxManager;

    private bool isTransition = false;
    // Start is called before the first frame update
    private void Start()
    {
        transition = FindObjectOfType<Transition>();
        player = FindObjectOfType<PlayerManager>();
        playerLocomotion = FindObjectOfType<PlayerLocomotion>();
        vfxManager = FindObjectOfType<VFXManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (gameObject.transform.position.y < blastZoneYLevel && !isTransition)
            Waiter(1f);
        
    }
    
    public void Waiter(float timer)
    {
        ScreenShake.Shake(0.3f, 3f);
        vfxManager.Play("Player Die");
        isTransition = true;
        player.LockGameplayInput();
        currentDeathCount++;
        transition.TransitToCanvas(respawnLocation, respawnLocation);
        Invoke(nameof(Respawn), timer*0.8f);
        Invoke(nameof(reloadLevel), timer);
    }

    /// <summary>
    /// Resets the player position to the respawnLocation
    /// </summary>
    public void Respawn()
    {
        playerLocomotion.ResetPlayerAndPosition(respawnLocation.transform.position);
    }

    public void reloadLevel()
    {
        player.UnlockGameplayInput();
        var spikeRef = FindObjectOfType<SpikesScript>();
        if(spikeRef)
            spikeRef.RestartMap();

        isTransition = false;
    }
    
}
