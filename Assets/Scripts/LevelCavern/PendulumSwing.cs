using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumSwing : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    private float velocityMax;
    [SerializeField] private float speed;


    private void Update()
    {
        if (Math.Abs(Time.timeScale - 1.0f) < 0.01f)
        {
            velocityMax = speed * Mathf.Sin(Time.time);
            transform.Rotate(Vector3.forward, velocityMax);
        }

    }

}
