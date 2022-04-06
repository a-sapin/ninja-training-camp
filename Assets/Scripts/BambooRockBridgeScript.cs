using UnityEngine;

public class BambooRockBridgeScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private float massToPush;
    private float massWhenNotPushed;
    private void Start()
    {
        massToPush = 1.2f;
        massWhenNotPushed = 9999;
        var nameToLayer = LayerMask.NameToLayer("Spline");
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(nameToLayer, nameToLayer);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            if (Mathf.Abs(col.transform.position.x - transform.position.x) > 10.3) 
                rb.mass =  massToPush;
        }else if (col.collider.GetComponent<FallingRock>())
        {
            rb.mass = massWhenNotPushed;
        }
        
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player")) rb.mass =massWhenNotPushed;
    }
    
}