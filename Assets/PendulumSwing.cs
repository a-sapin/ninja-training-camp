using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumSwing : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] [Range(-90,-10f)] [Tooltip("Angle Gauche maximal qui le swing peu aller")] float leftSwingRange;
    [SerializeField] [Range(10f,90)] [Tooltip("Angle Droit maximal qui le swing peu aller")]private float rightSwingRange;
    [SerializeField] [Range(10f,180f)] private float velocityMax;
    void Start()
    {
        rigidbody2D.angularVelocity = velocityMax;
    }

    // Update is called once per frame
    void Update()
    {
        Swing();
    }

    private void Swing()
    {
        if (transform.rotation.z > 0 &&
            transform.rotation.eulerAngles.z < rightSwingRange &&
            rigidbody2D.angularVelocity > 0 &&
            rigidbody2D.angularVelocity < velocityMax)
        {
            rigidbody2D.angularVelocity = velocityMax;
        }
        else if (transform.rotation.z < 0 &&
                 transform.rotation.eulerAngles.z > leftSwingRange &&
                 rigidbody2D.angularVelocity < 0 &&
                 rigidbody2D.angularVelocity > velocityMax * -1)
        {
            rigidbody2D.angularVelocity = -velocityMax;
        }


    }
}
