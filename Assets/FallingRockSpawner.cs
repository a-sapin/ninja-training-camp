using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRockSpawner : MonoBehaviour
{
    [SerializeField] private GameObject rockGameObject;
    [SerializeField] [Range(1.0f, 30)] private float timeBetweenSpawns = 5.0f;

    private float timer = 0f;
    
    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            timer = timeBetweenSpawns;
            Instantiate(rockGameObject,gameObject.transform);
        }

        timer -= Time.deltaTime;
    }
}
