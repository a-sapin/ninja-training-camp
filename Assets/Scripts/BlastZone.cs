using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastZone : MonoBehaviour
{
    public float blastZoneYLevel;
    [Header("GameObject References")]
    [SerializeReference] private GameObject respawnLocation;

    [Header("Level Specific Values")]
    public int currentDeathCount = 0; // in other words, how many times the objective was touched
    private PlayerLocomotion playerLocomotion;


    // Start is called before the first frame update
    void Start()
    {
        playerLocomotion = this.gameObject.GetComponent<PlayerLocomotion>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.position.y < blastZoneYLevel)
        {
            playerLocomotion.ResetPlayerAndPosition(respawnLocation.transform.position);
            currentDeathCount++;
        }
    }
}
