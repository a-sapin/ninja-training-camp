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

    private bool jumpButtonHeld;
    /// <summary>
    /// Detects a jump button press, but not a hold.
    /// </summary>
    /// <returns> True if the button is pressed, false if it is held or released.</returns>
    public bool JumpButtonDown() 
    { 
        if(Input.GetAxisRaw("Jump") > 0f)
        {
            if (jumpButtonHeld)
            {
                return false;
            }
            else
            {
                jumpButtonHeld = true;
                return true;
            }
        }
        jumpButtonHeld = false;

        return false;
    }
    public bool Jump() { jumpButtonHeld = true;  return ReadButton("Jump"); }
    public bool Dash() { return ReadButton("Dash"); }
    public Vector2 Move() { return moveInput; }

    /// <summary>
    /// Checks if the player is holding a horizontal move input
    /// that is closer to horizontal than vertical.
    /// </summary>
    /// <returns>True when th input is held, False otherwise.</returns>
    public bool IsMoveInput()
    {
        if (Mathf.Abs(moveInput.x) > 0.01f && Mathf.Abs(moveInput.x) >= Mathf.Abs(moveInput.y))
            return true;
        else
            return false;
    }

    /// <summary>
    /// Used locally. Read value of a generic button.
    /// </summary>
    /// <param name="inputName">The name of the button axis.</param>
    /// <returns>True if the button is pressed or held, False otherwise.</returns>
    private bool ReadButton(string inputName)
    {
        return Input.GetAxisRaw(inputName) > 0f;
    }

    /// <summary>
    /// Updates the movement input.
    /// </summary>
    void TickMoveInput()
    {
        var temp = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveInput = Vector2.ClampMagnitude(temp, 1f);
    }

    /// <summary>
    /// Checks what direction the player is holding to climb a ladder
    /// </summary>
    /// <returns>1 for up, -1 for down, 0 for no direction.</returns>
    public int LadderInputDir()
    {
        Vector2 input = Move();

        // if holding diagonal, accept only diagonal close enough to y-axis than x axis.
        // Also with minimum y input
        if (Mathf.Abs(input.y) > Mathf.Abs(input.x) && Mathf.Abs(input.y) >= 0.2f) // TODO: magic numberrrrrrr
        {
            if (input.y > 0.01f)
            {
                return 1;
            }
            else if (input.y < -0.01f)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        return 0;
    }

    public bool IsLadderInput()
    {
        int dir = LadderInputDir();

        if (Mathf.Abs(dir) > 0)
            return true;
        else
            return false;
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
