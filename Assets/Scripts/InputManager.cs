using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private float maxGrappleDistance;
    private Transform playerTransform;
    private Grapple grapple;

    private void Start()
    {
        grapple = FindObjectOfType<Grapple>();
        playerTransform = grapple.transform;
    }
    private void Update()
    {
        if (Input.GetAxis("Grapple") > 0 && !grapple.isGrappling)
        {
            Vector2 nearestTarget = grapple.GetNearestTargetPos(playerTransform.position);
            if (Vector2.Distance(nearestTarget,playerTransform.position) < maxGrappleDistance)
            {
                grapple.ThrowGrapple(nearestTarget);
            }
        }
        else if (Input.GetAxis("Grapple") <= 0 && grapple.isGrappling)
        {
            grapple.ForceDetachGrapple();
        }
    }
}
