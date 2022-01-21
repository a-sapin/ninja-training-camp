using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerLocomotion myPlayerLocomotion;
    [SerializeField] State currentState;

    public PlayerLocomotion GetLocomotion() { return myPlayerLocomotion; }

    /// <summary>
    /// Properly changes the state of the player by calling Exit() function 
    /// of the current state and Enter() function of the next state.
    /// </summary>
    /// <param name="nextState">The next state to switch to.</param>
    public void ChangeState(State nextState)
    {
        if(currentState.GetType() == nextState.GetType())
        {
            return;
        }
        else
        {
            currentState.Exit();
            currentState = nextState;
            currentState.Enter();
        }
    }

    void Start()
    {
        myPlayerLocomotion = GetComponent<PlayerLocomotion>();
    }

    void Update()
    {
        currentState.HandleSurroundings(this);
        currentState.HandleInputs(this);
        currentState.LogicUpdate(this);
    }

    void FixedUpdate()
    {
        currentState.PhysicsUpdate(this);
    }
}
