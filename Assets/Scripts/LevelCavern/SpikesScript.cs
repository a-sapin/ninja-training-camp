using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<BlastZone>().Waiter(1.0f);
            col.gameObject.GetComponent<PlayerManager>().GetGrapplingGun().StopGrappling();
            RestartMap();
        }
    }

    public void RestartMap()
    {
        var objects = FindObjectsOfType<FallingRock>();
        foreach (var t in objects)
        {
            Destroy(t.gameObject);
        }

        var leviers = FindObjectsOfType<TriggerLevier>();
        foreach (var t in leviers)
        {
            t.ResetLevier();
        }
    }
}
    

