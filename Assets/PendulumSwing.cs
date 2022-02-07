using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumSwing : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] [Range(-1,-0f)] [Tooltip("Angle Gauche maximal qui le swing peu aller")] float leftSwingRange;
    [SerializeField] [Range(0f,1)] [Tooltip("Angle Droit maximal qui le swing peu aller")]private float rightSwingRange;
    [SerializeField] [Range(0,1)] private float velocityMax;
    
    // Update is called once per frame
    void Update()
    {
        //Swing();
        if (transform.rotation.normalized.z > rightSwingRange && velocityMax > 0) 
            velocityMax *= -1;
        if (transform.rotation.normalized.z < leftSwingRange && velocityMax < 0) 
            velocityMax *= -1;
        
        transform.Rotate(Vector3.forward, velocityMax);
        
    }

    /*  DEPRECATED FUNCTION. Je vais still laisser la si je veux attemp une swing plus "naturelle"
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


    }*/
}
