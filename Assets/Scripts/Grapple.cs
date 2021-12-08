using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{

    public Camera mainCamera;
    public LineRenderer _lineRenderer;
    public SpringJoint2D _springJoint;
    public AnimationCurve affectCurve;

    private PlayerLocomotion myPlayerLocomotion;
    private Vector3 visualRopeEnd; // where the line renderer ends, between the player and the grapple target

    // The max range of the grapple
    public float maxDistance = 1.0f;
    public bool hasCooldown = false;
    public float cooldownTime = 1.0f;
    private float currentCooldownLeft = 1.0f;
    public bool refreshOnLand = true;

    [SerializeField] private LayerMask grappleLayerMask;
    [Header("Rope Appearance")]
    public int resolution = 300; // how many points are calculated in the rope
    public float wiggleHeight = 2.0f; // how far sideways the rope wiggles
    public float wiggleFrequency = 3.0f; // how many waves do we see along the length of the rope
    public float travelSpeed = 12.0f; // visual only, how fast the end of the rope travels towards target

    [Header("Wiggle Simulation Variables")]
    public float damper = 14f;
    public float strength = 800f;
    public float initialWiggleVelocity = 15f;
    public float currentWiggleVelocity = 0f;
    private float wiggleModifier = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _springJoint.enabled = false;
        myPlayerLocomotion = GetComponent<PlayerLocomotion>();
        currentWiggleVelocity = initialWiggleVelocity;
        _lineRenderer.positionCount = resolution + 1;
    }

    private Vector3 targetLocation;
    // Update is called once per frame
    void Update()
    {
        HandleGrapple();
        HandleCooldown(Time.deltaTime);
    }

    private void HandleCooldown(float delta)
    {
        if (hasCooldown)
        {
            if (currentCooldownLeft <= 0.0f)
            {
                myPlayerLocomotion.EnableGrapple();
                currentCooldownLeft = cooldownTime;
            }
            else
            {
                currentCooldownLeft -= delta;
            }
        }
    }

    public void TryRefreshOnLand()
    {
        if (refreshOnLand)
        {
            myPlayerLocomotion.EnableGrapple();
        }
        if (hasCooldown)
        {
            currentCooldownLeft = cooldownTime;
        }
    }

    private void HandleGrapple()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && myPlayerLocomotion.CanGrapple() && myPlayerLocomotion.GetHasGrapplePower())
        {
            // convert mouse position to world position
            Vector2 evaluateTargetPos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);

            if (DetectGrapplePoint(transform.position, ref evaluateTargetPos))
            {
                // keep target location for later use
                targetLocation = evaluateTargetPos;

                // Activate grapple and its visuals
                DrawRope(targetLocation);
                myPlayerLocomotion.isUsingLadder = false; // make player let go of ladder upon grappling
                _springJoint.connectedAnchor = targetLocation;
                _springJoint.enabled = true;
                _lineRenderer.enabled = true;
            }

        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            ForceDetachGrapple();
        }

        if (_springJoint.enabled) // update position of origin, i.e.: when the button is held down
        {
            DrawRope(targetLocation);
        }
        else
        {
            visualRopeEnd = transform.position; // reset rope end to player origin when not grappling
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

    public void ForceDetachGrapple()
    {
        myPlayerLocomotion.SetCantGrapple(); // disable grapple until another script enables it again
        _springJoint.enabled = false; // disable joint
        _lineRenderer.enabled = false;

        // reset wiggle
        wiggleModifier = 0f;
        currentWiggleVelocity = initialWiggleVelocity;
    }

    private void DrawRope(Vector3 targetLocation)
    {
        //_lineRenderer.SetPosition(0, transform.position);
        Vector3 towardsTarget = targetLocation - transform.position; // vector pointing to target
        Vector3 perpendicularToTarget = Quaternion.Euler(0, 0, 90) * towardsTarget; // rotate 90 deg around Z axis
        WiggleSimulation(Time.deltaTime); // get the wiggle amplitude for this current frame

        // Move rope end towards the target location
        visualRopeEnd = Vector3.Lerp(visualRopeEnd, targetLocation, Time.deltaTime * travelSpeed);

        // Offset each point along the rope following a sine wave and the affectCurve
        for(var i = 0; i < resolution + 1; i++)
        {
            var delta = i / (float)resolution;
            var offset = perpendicularToTarget * wiggleHeight * Mathf.Sin(delta * wiggleFrequency * Mathf.PI) 
                * wiggleModifier * affectCurve.Evaluate(delta);
            _lineRenderer.SetPosition(i, Vector3.Lerp(transform.position, visualRopeEnd, delta) + offset);
        }
    }

    // Make wiggleModifier vary like a step response in a second order system
    // bruh
    // Basically a sine wave that starts with high amplitude and quickly (relative to damper) 
    // drops to a low amplitude
    private void WiggleSimulation(float deltaTime)
    {
        var direction =  wiggleModifier < 0 ? 1f : -1f; // whether the wave is flipped or not
        var force = Mathf.Abs(wiggleModifier) * strength; // how strong the wiggle will be
        currentWiggleVelocity += (force * direction - currentWiggleVelocity * damper) * deltaTime;
        wiggleModifier += currentWiggleVelocity * deltaTime;
    }
}
