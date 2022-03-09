using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumSwing : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody2D;
    private float velocityMax;
    [SerializeField] private float speed;
    
    void Update()
    {
        velocityMax = speed * Mathf.Sin(Time.unscaledTime);
        rigidbody2D.angularVelocity = velocityMax;
        transform.Rotate(Vector3.forward, velocityMax);

    }

}
