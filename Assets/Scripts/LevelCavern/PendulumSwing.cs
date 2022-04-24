using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumSwing : MonoBehaviour
{
    private float velocityMax;
    [SerializeField] private float speed;

    private GameObject playerPos; // used to keep track of the player's relative position
    private GameObject player; // a ref to the player, both are handled in the script

    private void Start()
    {
        playerPos = new GameObject("PlayerPosition");
        playerPos.transform.parent = transform;
    }

    private void Update()
    {
        if (Mathf.Abs(Time.timeScale - 1.0f) < 0.01f)
        {
            if(player != null)
            {
                playerPos.transform.position = player.transform.position; // save the player's position before rotating
                player.transform.parent = null; // unlink player
            }
            
            velocityMax = speed * Mathf.Sin(Time.time);
            transform.Rotate(Vector3.forward, velocityMax);

            if (player != null)
            {
                player.transform.position = playerPos.transform.position; // apply the new position to the player
                player.transform.parent = transform; // re-parent player
            }
        }

    }

    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.CompareTag("Player")) // on contact with player
        {
            player = collider.gameObject;
            collider.transform.parent = transform; // make player a child of this object
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player")) // on contact with player
        {
            //collision.transform.up = Vector2.up; // make sure player does not rotate
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player")) // on contact with player
        {
            player = null;
            collision.transform.parent = null; // unlink both objects
            collision.transform.up = Vector2.up; // make sure player does not rotate
        }
    }
}
