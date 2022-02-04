using UnityEngine;

public class BlastZone : MonoBehaviour
{
    public float blastZoneYLevel;
    [Header("GameObject References")]
    public GameObject respawnLocation;

    [Header("Level Specific Values")]
    public int currentDeathCount = 0; // in other words, how many times the objective was touched
    private PlayerLocomotion playerLocomotion;

    private Transition transition;

    private bool isTransition = false;
    // Start is called before the first frame update
    private void Start()
    {
        transition = FindObjectOfType<Transition>();
        playerLocomotion = gameObject.GetComponent<PlayerLocomotion>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (gameObject.transform.position.y < blastZoneYLevel && !isTransition)
        {
            transition.TransitToCanvas(respawnLocation, respawnLocation);
            isTransition = true;
            Invoke(nameof(Waiter), 1f);
            
            currentDeathCount++;
        }
    }
    
    private void Waiter()
    {
        playerLocomotion.ResetPlayerAndPosition(respawnLocation.transform.position);
        isTransition = false;
    }

    public void RespawnPlayer()
    {
        isTransition = true;
        Waiter();
        currentDeathCount++;
    }
}
