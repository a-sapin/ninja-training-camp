using UnityEngine;

public class PendulumSwing : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    private float velocityMax;
    [SerializeField] private float speed;


    private void Update()
    {
        velocityMax = speed * Mathf.Sin(Time.unscaledTime);
        rb2d.angularVelocity = velocityMax;
        transform.Rotate(Vector3.forward, velocityMax);

    }

}
