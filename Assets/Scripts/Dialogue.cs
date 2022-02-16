using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
class DialoguePart
{
    public bool isSenseiTalking;
    public string text;
}
public class Dialogue : MonoBehaviour
{
    [SerializeField] private Text textZone;
    [SerializeField] private GameObject sensei, player, canvas;
    [SerializeField] private DialoguePart[] dialogue;
    [SerializeField] private float writeDelay; //LOKI
    private PlayerManager playerManager;
    VFXManager mySoundManager;

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
        yield return new WaitForSecondsRealtime(0.05f);
        mySoundManager = FindObjectOfType<VFXManager>(); //Find sound manager for dialogue "beep"
        playerManager = FindObjectOfType<PlayerManager>();
        playerManager.LockGameplayInput(); // freeze player during dialogue
        //lock
        int dialogueIndex = 0;
        canvas.SetActive(true);
        if (dialogue.Length > 0)
        {

            sensei.SetActive(dialogue[dialogueIndex].isSenseiTalking);
            player.SetActive(!dialogue[dialogueIndex].isSenseiTalking);

            //LOKI (remove when finished)
            int lengthOfSentence = dialogue[dialogueIndex].text.Length;
            char[] charsArray = new char[lengthOfSentence];
            for (int i = 0; i < lengthOfSentence; i++) //Put each char of DIALOGUE TEXT into a slot of the array
            {
                charsArray[i] = dialogue[dialogueIndex].text[i];
            }

            string current_Text = "";

            for (int o = 0; o < lengthOfSentence; o++) //Append each char of the dialogue to current_text then display it until everything is here
            {
                current_Text = current_Text + charsArray[o]; //Append char
                textZone.text = current_Text;
                mySoundManager.Play("Talking");
                yield return new WaitForSecondsRealtime(writeDelay);
            }

            //LOKI (remove when finished)

            //textZone.text = dialogue[dialogueIndex].text;
        }
        yield return new WaitForSecondsRealtime(0.5f);
        while (true)
        {
            if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape))
            {

                dialogueIndex++;
                if (dialogueIndex >= dialogue.Length) break;

                textZone.text = dialogue[dialogueIndex].text;
                sensei.SetActive(dialogue[dialogueIndex].isSenseiTalking);
                player.SetActive(!dialogue[dialogueIndex].isSenseiTalking);

                yield return new WaitForSecondsRealtime(0.5f);
            }
            yield return null;
        }
        playerManager.UnlockGameplayInput(); // once dialogue is done, let player move
        canvas.SetActive(false);
        Time.timeScale = 1;
    }
}
