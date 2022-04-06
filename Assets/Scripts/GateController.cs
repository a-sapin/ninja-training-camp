using UnityEngine;

public class GateController : MonoBehaviour
{
    enum MoveDirection
    {
        Up,
        Down,
        Left,
        Right
    };

    [SerializeField] Transform gatePosition;
    [SerializeField] Transform closedPosition;
    [SerializeField] MoveDirection moveDirection;
    [SerializeField] float distanceToMove = 1.0f;
    [SerializeField] float moveSpeed = 1.0f;

    private bool openDoor;
    private Vector2 openPosition;

    // Start is called before the first frame update
    void Start()
    {
        openDoor = false;
        openPosition = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (openDoor)
        {
            MoveGateTo(openPosition);
        }
        else
        {
            MoveGateTo(closedPosition.localPosition);
        }
    }

    private void MoveGateTo(Vector3 targetPosition)
    {
        gatePosition.localPosition = Vector2.Lerp(gatePosition.localPosition, targetPosition, moveSpeed * Time.deltaTime);
    }

    
    public void ToggleDoor()
    {
        if (openDoor)
        {
            openDoor = false;
            openPosition = Vector2.zero;
        }
        else
        {
            openDoor = true;
            switch (moveDirection)
            {
                case MoveDirection.Up:
                    openPosition = Vector2.up * distanceToMove;
                    break;
                case MoveDirection.Down:
                    openPosition = Vector2.down * distanceToMove;
                    break;
                case MoveDirection.Left:
                    openPosition = Vector2.left * distanceToMove;
                    break;
                case MoveDirection.Right:
                    openPosition = Vector2.right * distanceToMove;
                    break;
            }
        }
    }
}
