using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{

    public Camera mainCamera;
    public LineRenderer _lineRenderer;
    public SpringJoint2D _springJoint;

    private PlayerLocomotion myPlayerLocomotion;

    // The max range of the grapple
    public float maxDistance = 1.0f;

    [SerializeField] private LayerMask grappleLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        //_distanceJoint.enabled = false;
        _springJoint.enabled = false;
        myPlayerLocomotion = GetComponent<PlayerLocomotion>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && myPlayerLocomotion.CanGrapple() && myPlayerLocomotion.GetHasGrapplePower())
        {
            // convert mouse position to world position
            Vector2 targetPos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);

            if(DetectGrapplePoint(transform.position, ref targetPos))
            {
                // set origin and target points of line renderer
                _lineRenderer.SetPosition(0, targetPos);
                _lineRenderer.SetPosition(1, transform.position);

                _springJoint.connectedAnchor = targetPos;
                _springJoint.enabled = true;
                _lineRenderer.enabled = true;
            }
            
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            myPlayerLocomotion.SetCantGrapple(); // disable grapple until another script enables it again
            _springJoint.enabled = false; // disable joint
            _lineRenderer.enabled = false;
        }

        if (_springJoint.enabled) // update position of origin, i.e.: when the button is held down
        {
            _lineRenderer.SetPosition(1, transform.position);
        }
    }

    // Casts a ray going from origin towards target and changes target to the location of the first
    // collision detected. The raycast goes beyond the target, until/unless maxDistance is reached
    private bool DetectGrapplePoint(Vector2 origin, ref Vector2 target)
    {
        Vector2 asDirection = target - origin;
        RaycastHit2D hit = Physics2D.Raycast(origin, asDirection, maxDistance, grappleLayerMask);
        
        if (hit.collider != null)
        {
            // change grapple point
            target = hit.point;
            return true;
        }
        else
        {
            return false;
        }
    }

}
