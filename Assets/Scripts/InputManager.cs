using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private float maxGrappleDistance;
    private Transform playerTransform;
    private GrapplingGun grapple;

    private void Start()
    {
        grapple = FindObjectOfType<GrapplingGun>();
        playerTransform = grapple.transform;
    }
    private void Update()
    {
        if (Input.GetAxis("Grapple") > 0 && !grapple.isGrapplingWithPad)
        {
            Vector2 nearestTarget = grapple.GetNearestTargetPos(playerTransform.position);

            if (Vector2.Distance(nearestTarget,playerTransform.position) < maxGrappleDistance)
            {
                //LANCER LE GRAPPIN
                Debug.Log("Grapplinnnnnnnnnnng");
               grapple.SetGrapplePoint(nearestTarget, true);
            }
        }
        else if (Input.GetAxis("Grapple") <= 0 && grapple.isGrapplingWithPad)
        {
              grapple.StopGrappling();

        }
    }
}
