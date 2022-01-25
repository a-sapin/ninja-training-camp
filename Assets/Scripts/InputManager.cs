using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private float maxGrappleDistance;
    private Transform playerTransform;
    private GrapplingGun grapple;
    Vector2 moveInput = Vector2.zero;

    private void Start()
    {
        grapple = FindObjectOfType<GrapplingGun>();
        playerTransform = grapple.transform;
    }

    private void Update()
    {
        TickMoveInput();
        HandleGrapple();
    }

    public bool Jump() { return ReadButton("Jump"); }
    public bool Dash() { return ReadButton("Dash"); }
    public Vector2 Move() { return moveInput; }

    /// <summary>
    /// Checks if the player is holding a move input.
    /// </summary>
    /// <returns>True when th input is held, False otherwise.</returns>
    public bool IsMoveInput()
    {
        if (moveInput.sqrMagnitude > 0.001f) // sqrMagnitude is faster than .magnitude
            return true;
        else
            return false;
    }

    /// <summary>
    /// Used locally. Read value of a button.
    /// </summary>
    /// <param name="inputName">The name of the button axis.</param>
    /// <returns>True if the button is pressed, False otherwise.</returns>
    private bool ReadButton(string inputName)
    {
        if(Input.GetAxisRaw(inputName) > 0f)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Updates the movement input.
    /// </summary>
    void TickMoveInput()
    {
        var temp = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveInput = Vector2.ClampMagnitude(temp, 1f);
    }

    void HandleGrapple()
    {
        if (Input.GetAxis("Grapple") > 0 && !grapple.isGrapplingWithPad)
        {
            Vector2 nearestTarget = grapple.GetNearestTargetPos(playerTransform.position);

            if (Vector2.Distance(nearestTarget, playerTransform.position) < maxGrappleDistance)
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
