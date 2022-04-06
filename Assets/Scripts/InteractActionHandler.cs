using UnityEngine;

public class InteractActionHandler : MonoBehaviour
{
    [SerializeField] bool tryingToInteract;
    bool holdingButton;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Interact") >= 0.05)
        {
            if(tryingToInteract == false)
            {
                tryingToInteract = true;
            }
        }
        else
        {
            tryingToInteract = false;
            holdingButton = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Button") && tryingToInteract && !holdingButton)
        {
            holdingButton = true;

            if (collision.gameObject.TryGetComponent(out InteractActionReceiver button))
            {
                button.Activate();
            }
        }
    }

}
