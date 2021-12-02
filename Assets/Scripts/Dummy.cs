using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    [SerializeField] private bool canMove;
    [SerializeField] private float speed;
    [SerializeField] private float force;
    [SerializeField] private GameObject dummy,leftPoint,rightPoint;
    private SpriteRenderer dummySprite;
    private bool goLeft = true;
    // Start is called before the first frame update
    void Start()
    {
        dummySprite = dummy.GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Vector2 dir = (collision.transform.position- this.transform.position).normalized;
            FindObjectOfType<PlayerLocomotion>().ExternForce = FindObjectOfType<PlayerLocomotion>().ExternForce+ dir * force;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!canMove) return;
        if (goLeft)
        {
            dummy.transform.position -= new Vector3(Time.deltaTime * speed, 0, 0);
            if(dummy.transform.position.x <= leftPoint.transform.position.x)
            {
                dummySprite.flipX = !dummySprite.flipX;
                goLeft = !goLeft;
            }
        }
        else
        {
            dummy.transform.position += new Vector3(Time.deltaTime * speed, 0, 0);
            if (dummy.transform.position.x >= rightPoint.transform.position.x)
            {
                dummySprite.flipX = !dummySprite.flipX;
                goLeft = !goLeft;
            }
        }
    }
}
