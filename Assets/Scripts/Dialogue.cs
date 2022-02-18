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
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private Text textZone;
    [SerializeField] private GameObject sensei, player, canvas;
    [SerializeField] private DialoguePart[] dialogue;
    private PlayerManager playerManager;
    private PlayerLocomotion playerLocomotion;
    private Timer timer;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
        playerLocomotion = FindObjectOfType<PlayerLocomotion>();
        StartFirstDialogue();
    }
    
    private void StartFirstDialogue()
    {
        if (PlayerPrefs.GetInt("playDialogue" + currentLevel, 1) != 1)
        {
            canvas.SetActive(false);
            return;
        }
        IEnumerator firstDialogue = PlayDialogue(dialogue);
        StartCoroutine(firstDialogue);
    }
    
    IEnumerator PlayDialogue(DialoguePart[] dialogue)
    {
        yield return new WaitForSecondsRealtime(0.05f);

        playerManager = FindObjectOfType<PlayerManager>();
        playerManager.LockGameplayInput(); // freeze player during dialogue
        timer.PauseTimer();

        int dialogueIndex = 0;
        canvas.SetActive(true);
        if(dialogue.Length > 0)
        {
            textZone.text = dialogue[dialogueIndex].text;
            sensei.SetActive(dialogue[dialogueIndex].isSenseiTalking);
            player.SetActive(!dialogue[dialogueIndex].isSenseiTalking);
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

        timer.RestartTimer();
        canvas.SetActive(false);
        Time.timeScale = 1;
    }
}
