using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesScript : MonoBehaviour
{
   

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Collide");
            col.gameObject.GetComponent<BlastZone>().RespawnPlayer();
        }
    }
}
    

