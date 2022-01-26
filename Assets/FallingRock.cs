using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
    [SerializeField] private int blastZoneYLevel = -50;
    [SerializeField] [Range(0.01f, 5)] private float knockbackDuration = 1.0f;
    [SerializeField] [Range(0.01f, 5)] private float knockbackForce = 1.0f;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            StartCoroutine(col.GetComponent<PlayerLocomotion>().PlayerKnockback(knockbackDuration,knockbackForce,col.transform.position));
            
        }
    }

    private void Update()
    {
        if (gameObject.transform.position.y < blastZoneYLevel)
            Destroy(gameObject);
    }
}
