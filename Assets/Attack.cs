using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    [SerializeField] private Animator anim;
    private VFXManager mySoundManager;

    private void Start()
    {
        mySoundManager = FindObjectOfType<VFXManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log("Attack || Right click pressed.");
            PlayerAttack();
            mySoundManager.StopAll();
            mySoundManager.Play("Sword");
        }
    }


    private void PlayerAttack()
    {
        if(anim != null)
            anim.Play("PlayerAttack");
    }
}
