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
    private Timer timer;
    VFXManager mySoundManager;
    private String vfxDialogue;

    void Start()
    {
        writeDelay = PlayerPrefs.GetFloat("writeDelay", 0.03f);
        timer = FindObjectOfType<Timer>();
        mySoundManager = FindObjectOfType<VFXManager>(); //Find sound manager for dialogue "beep"
        playerManager = FindObjectOfType<PlayerManager>();
        StartFirstDialogue();
    }

    private void StartFirstDialogue()
    {
        IEnumerator firstDialogue = PlayDialogue();
        StartCoroutine(firstDialogue);
    }

    IEnumerator PlayDialogue()
    {
        yield return new WaitForSecondsRealtime(0.05f);
        playerManager.LockGameplayInput(); // freeze player during dialogue
        timer.PauseTimer();

        int dialogueIndex = 0;
        bool waiting = false;
        canvas.SetActive(true);
        if (dialogue.Length > 0)
        {
            while (dialogueIndex < dialogue.Length)
            {
                if (!waiting)
                {
                    sensei.SetActive(dialogue[dialogueIndex].isSenseiTalking);
                    player.SetActive(!dialogue[dialogueIndex].isSenseiTalking);

                    if (dialogue[dialogueIndex].isSenseiTalking)
                    {
                        vfxDialogue = "Sensei is talking";
                    }
                    else if(!dialogue[dialogueIndex].isSenseiTalking)
                    {
                        vfxDialogue = "Satoru is talking";
                    }

                    //LOKI (remove when finished)
                    int lengthOfSentence = dialogue[dialogueIndex].text.Length;
                    char[] charsArray = new char[lengthOfSentence];
                    for (int i = 0; i < lengthOfSentence; i++) //Put each char of DIALOGUE TEXT into a slot of the array
                    {
                        charsArray[i] = dialogue[dialogueIndex].text[i];
                    }

                    string currentText = "";

                    for (int o = 0;
                         o < lengthOfSentence;
                         o++) //Append each char of the dialogue to current_text then display it until everything is here
                    {
                        //IF a key is pressed, skip to end of dialogue
                        if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape))
                        {
                            textZone.text = dialogue[dialogueIndex].text;
                            o = lengthOfSentence;
                        }
                        else
                        {
                            currentText += charsArray[o]; //Append char
                            textZone.text = currentText;
                            mySoundManager.Play(vfxDialogue);
                            yield return new WaitForSecondsRealtime(writeDelay);
                        }
                    }

                    waiting = true;
                    //Debug.Log("BULLE COMPLETEE");
                }
                else if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape))
                {
                    dialogueIndex++;
                    textZone.text = "";
                    waiting = false;
                }

                yield return null;
            }

            //LOKI (remove when finished)

            //textZone.text = dialogue[dialogueIndex].text;
        }

        playerManager.UnlockGameplayInput(); // once dialogue is done, let player move

        timer.RestartTimer();
        canvas.SetActive(false);
        Time.timeScale = 1;
    }
}