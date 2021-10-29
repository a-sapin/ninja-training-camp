using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController myCharController;
    public float playerSpeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        myCharController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        myCharController.Move(move * Time.deltaTime * playerSpeed);
        
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
    }
}
