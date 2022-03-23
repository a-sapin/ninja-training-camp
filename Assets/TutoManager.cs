using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoManager : MonoBehaviour
{
    private Dialogue dialogue;
    [SerializeField] float maxDist;
    [SerializeField] Transform player;
    [SerializeField] Transform leftRight,jump,grapple,jump2,dash;
    void Start()
    {
        dialogue = FindObjectOfType<Dialogue>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogue.isInDialogue)
        {
            HideAll();
            return;
        }
        TestInputs();
        DisplayInputHelp();
    }
    void TestInputs()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            PlayerPrefs.SetInt("haveMoved", 1);
        }
        if (Input.GetAxis("Jump") > 0)
        {
            PlayerPrefs.SetInt("haveJumped", 1);
        }
        if (Input.GetAxis("Grapple") > 0)
        {
            PlayerPrefs.SetInt("haveGrappled", 1);
        }
        if (Input.GetAxis("Dash") > 0)
        {
            PlayerPrefs.SetInt("haveDashed", 1);
        }
    }
    void HideAll()
    {
        leftRight.GetComponent<SpriteRenderer>().enabled = false;
        jump.GetComponent<SpriteRenderer>().enabled = false;
        jump2.GetComponent<SpriteRenderer>().enabled = false;
        dash.GetComponent<SpriteRenderer>().enabled = false;
        grapple.GetComponent<SpriteRenderer>().enabled = false;
    }
    void DisplayInputHelp()
    {
        if (Vector3.Distance(player.position, leftRight.position) < maxDist && PlayerPrefs.GetInt("haveMoved", 0) ==0)
        {
            leftRight.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            leftRight.GetComponent<SpriteRenderer>().enabled = false;
        }



        if (Vector3.Distance(player.position, jump.position) < maxDist && PlayerPrefs.GetInt("haveJumped", 0) == 0)
        {
            jump.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            jump.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (Vector3.Distance(player.position, jump2.position) < maxDist && PlayerPrefs.GetInt("haveJumped", 0) == 0)
        {
            jump2.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            jump2.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (Vector3.Distance(player.position, grapple.position) < maxDist && PlayerPrefs.GetInt("haveGrappled", 0) == 0)
        {
            grapple.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            grapple.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (Vector3.Distance(player.position, dash.position) < maxDist && PlayerPrefs.GetInt("haveDashed", 0) == 0)
        {
            dash.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            dash.GetComponent<SpriteRenderer>().enabled = false;
        }

    }
}
