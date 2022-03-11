using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
    [SerializeField] private int blastZoneYLevel = -50;
    [SerializeField] [Range(0.01f, 5)] private float knockbackDuration = 1.0f;
    [SerializeField] [Range(0.01f, 100)] private float knockbackForce = 1.0f;
    [SerializeField] private LayerMask ignoredLayer;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            //we get a vector pointing from rock towards player
            Vector2 pushingDirection = (col.transform.position - gameObject.transform.position).normalized;
            PlayerManager thePlayer = col.gameObject.GetComponentInChildren<PlayerManager>();

            StartCoroutine(KnockbackPlayer(pushingDirection, thePlayer));
        }
    }

    private void Start()
    {
        
        //Physics.IgnoreLayerCollision(ignoredLayer.value,ignoredLayer.value,true);
    }

    private void Update()
    {
        if (gameObject.transform.position.y < blastZoneYLevel)
            Destroy(gameObject);
    }

    /// <summary>
    /// Knocks the player using an impulse and disables grapple
    /// for knockbackDuration second(s).
    /// </summary>
    /// <param name="knockbackDirection">Impulse sends player towards this direction.</param>
    /// <param name="player">The player we are knocking back.</param>
    /// <returns>Yields for knockbackDuration second(s) before returning 0.</returns>
    IEnumerator KnockbackPlayer(Vector2 knockbackDirection, PlayerManager player)
    {
        player.GetGrapplingGun().StopGrappling(); // disconnects grapple
        player.SetCanGrapple(false); // stops player from grapling
        player.GetLocomotion().ApplyExternForce(knockbackDirection * knockbackForce);
        
        yield return new WaitForSeconds(knockbackDuration); // keep grapple disabled for this amount of time

        player.SetCanGrapple(true); // until we enable grapple again
        yield return 0;
    }

}
