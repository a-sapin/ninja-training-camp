using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
    [SerializeField] private int blastZoneYLevel = -50;
    [SerializeField] [Range(0.01f, 5)] private float knockbackDuration = 1.0f;
    [SerializeField] [Range(0.01f, 100)] private float knockbackForce = 1.0f;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            Vector2 pushingDirection = (gameObject.transform.position - col.transform.position).normalized;
            StartCoroutine(col.collider.GetComponent<PlayerLocomotion>().PlayerKnockback(knockbackDuration,knockbackForce,pushingDirection));
        }
    }

  
    private void Update()
    {
        if (gameObject.transform.position.y < blastZoneYLevel)
            Destroy(gameObject);
    }
}
