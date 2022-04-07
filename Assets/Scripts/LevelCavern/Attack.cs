using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Attack : MonoBehaviour
{

    [SerializeField] private Animator anim;
    [SerializeField] private float attackRange;
    private VFXManager mySoundManager;

    private void Start()
    {
        mySoundManager = FindObjectOfType<VFXManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            PlayerAttack();
            mySoundManager.StopAll();
            mySoundManager.Play("Sword");
        }
    }


    private void PlayerAttack()
    {
        if(anim != null)
            anim.Play("PlayerAttack");
        
        Vector3 rangeVector = new Vector3(attackRange, attackRange, attackRange);

        var transform1 = transform;
        RaycastHit hits;
        
        if(Physics.BoxCast(transform1.position, transform1.localScale, transform1.forward,out hits, transform.rotation, attackRange * 2))
            Debug.Log($"Attack || Hit= {hits.collider.name}");
    }
    
}
