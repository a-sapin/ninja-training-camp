using System;
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
    
    [SerializeField] private GameObject transition;

    private bool isTransition = false;
    // Start is called before the first frame update
    void Start()
    {
        playerLocomotion = gameObject.GetComponent<PlayerLocomotion>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < blastZoneYLevel && !isTransition)
        {
            isTransition = true;
            StartCoroutine(Waiter());
            currentDeathCount++;
        }
    }
    
    IEnumerator Waiter()
    {
        transition.SendMessage("AnimateTransition");
        yield return new WaitForSecondsRealtime(1f);
        transition.SendMessage("FadeOut", false);
        playerLocomotion.ResetPlayerAndPosition(respawnLocation.transform.position);
        yield return new WaitForSecondsRealtime(1);
        isTransition = false;
    }
}
