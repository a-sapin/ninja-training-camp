using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumSwing : MonoBehaviour
{
    [SerializeField] private float totalAngleRange = 60f;
    [SerializeField] private float frequency = 1f;
    [SerializeField] private float middleAngleOffset = 0f;

    private GameObject playerPos; // used to keep track of the player's relative position
    private GameObject player; // a ref to the player, both are handled in the script

    private void Start()
    {
        playerPos = new GameObject("PlayerPosition");
        playerPos.transform.parent = transform;
    }

    private float time = 0;
    private void Update()
    {
        if (Mathf.Abs(Time.timeScale - 1.0f) < 0.01f)
        {
            time += Time.deltaTime;
            if(player != null)
            {
                playerPos.transform.position = player.transform.position; // save the player's position before rotating
                player.transform.parent = null; // unlink player
            }

            float amplitude = totalAngleRange/2f;

            float sinusoid = amplitude * Mathf.Sin(frequency * time) + middleAngleOffset;

            transform.rotation = Quaternion.Euler(0f, 0f, sinusoid);

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
