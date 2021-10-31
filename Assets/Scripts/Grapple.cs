using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{

    public Camera mainCamera;
    public LineRenderer _lineRenderer;
    public SpringJoint2D _springJoint;

    // Start is called before the first frame update
    void Start()
    {
        //_distanceJoint.enabled = false;
        _springJoint.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // convert mouse position to world position
            Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);

            // set origin and target points of line renderer
            _lineRenderer.SetPosition(0, mousePos);
            _lineRenderer.SetPosition(1, transform.position);

            _springJoint.connectedAnchor = mousePos;
            _springJoint.enabled = true;
            _lineRenderer.enabled = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _springJoint.enabled = false; // disable joint
            _lineRenderer.enabled = false;
        }

        if (_springJoint.enabled)
        {
            _lineRenderer.SetPosition(1, transform.position);
        }
    }
}
