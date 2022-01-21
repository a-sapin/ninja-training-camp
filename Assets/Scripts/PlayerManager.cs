using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerLocomotion myPlayerLocomotion;


    public PlayerLocomotion GetLocomotion() { return myPlayerLocomotion; }

    // Start is called before the first frame update
    void Start()
    {
        myPlayerLocomotion = GetComponent<PlayerLocomotion>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
