using UnityEngine;

public class Dummy : MonoBehaviour
{
    [SerializeField] private bool canMove;
    [SerializeField] private float speed;
    [SerializeField] private float force;
    [SerializeField] private GameObject leftPoint, rightPoint;
    private SpriteRenderer dummySprite;
    private bool goLeft = true;

    void Start()
    {
        dummySprite = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player")) // on contact with player, ...
        {
            Vector2 dir = (collision.transform.position - transform.position).normalized; // pointing away from this dummy

            if (dir.y <= 0.7f)
            {
                dir.y = 0.7f;
                dir.Normalize(); // raise it to about 45 degrees if under 45 degrees
            }

            Debug.DrawRay(collision.transform.position, dir, Color.red, 1f);
            // Apply a force on the player, towards dir
            collision.gameObject.GetComponent<PlayerLocomotion>().ApplyExternForce(dir * force);
        }
    }
    
    void Update()
    {
        if (!canMove) return;
        if (goLeft)
        {
            transform.position -= new Vector3(Time.deltaTime * speed, 0, 0);
            if(transform.position.x <= leftPoint.transform.position.x)
            {
                dummySprite.flipX = !dummySprite.flipX;
                goLeft = !goLeft;
            }
        }
        else
        {
            transform.position += new Vector3(Time.deltaTime * speed, 0, 0);
            if (transform.position.x >= rightPoint.transform.position.x)
            {
                dummySprite.flipX = !dummySprite.flipX;
                goLeft = !goLeft;
            }
        }
    }
}
