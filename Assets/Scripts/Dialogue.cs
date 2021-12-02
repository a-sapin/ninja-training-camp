using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[Serializable] class DialoguePart
{
    public bool isSenseiTalking;
    public string text;
}
public class Dialogue : MonoBehaviour
{
    [SerializeField] private Text textZone;
    [SerializeField] private GameObject sensei, player, canvas;
    [SerializeField] private DialoguePart[] dialogue;
    private PlayerLocomotion playerLocomotion;

    void Start()
    {
        StartFirstDialogue();
    }

    private void StartFirstDialogue()
    {
        IEnumerator firstDialogue = PlayDialogue(dialogue);
        StartCoroutine(firstDialogue);
    }

    IEnumerator PlayDialogue(DialoguePart[] dialogue)
    {
        playerLocomotion = FindObjectOfType<PlayerLocomotion>();
        bool normalPlayerCanMove = playerLocomotion.canMove;
        playerLocomotion.canMove = false;
        int dialogueIndex = 0;
        canvas.SetActive(true);
        if(dialogue.Length > 0)
        {
            textZone.text = dialogue[dialogueIndex].text;
            sensei.SetActive(dialogue[dialogueIndex].isSenseiTalking);
            player.SetActive(!dialogue[dialogueIndex].isSenseiTalking);
        }
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape))
            {
               
                dialogueIndex++;
                if (dialogueIndex >= dialogue.Length) break;

                textZone.text = dialogue[dialogueIndex].text;
                sensei.SetActive(dialogue[dialogueIndex].isSenseiTalking);
                player.SetActive(!dialogue[dialogueIndex].isSenseiTalking);
                
                yield return new WaitForSeconds(0.5f);
            }
            yield return null;
        }
        playerLocomotion.canMove = normalPlayerCanMove;
        canvas.SetActive(false);
    }
}
