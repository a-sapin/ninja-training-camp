using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControllerInLevelSelection : MonoBehaviour
{
    [SerializeField] private bool simulateCompletionLVL1, simulateCompletionLVL2, simulateCompletionLVL3;
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject[] arrowPos;
    [SerializeField] private Button[] linkedButton;
    private LevelInformations info;
    private int currentPos;

    public AudioSource buttonArrow;
    private bool isPressed = false;

    private void Start()
    {
        if (simulateCompletionLVL1) PlayerPrefs.SetInt("Level1Finished", 1);
        if (simulateCompletionLVL2) PlayerPrefs.SetInt("Level2Finished", 1);
        if (simulateCompletionLVL3) PlayerPrefs.SetInt("Level3Finished", 1);

        if (PlayerPrefs.GetInt("Level1Finished", 0) == 1) linkedButton[2].gameObject.SetActive(true);
        if (PlayerPrefs.GetInt("Level2Finished", 0) == 1) linkedButton[3].gameObject.SetActive(true);
        if (PlayerPrefs.GetInt("Level3Finished", 0) == 1) linkedButton[4].gameObject.SetActive(true);
        StartCoroutine(nameof(ArrowMove));
        info = FindObjectOfType<LevelInformations>();
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Jump") > 0 && !isPressed)
        {
            isPressed = true;
            linkedButton[currentPos].onClick.Invoke();
        }
    }

    private IEnumerator ArrowMove()
    {
        while (true)
        {
            if (Input.GetAxisRaw("Vertical") > 0.3 || Input.GetAxisRaw("Horizontal") > 0.3)
            {
                buttonArrow.Play();
                if (currentPos == 0 ||
                    (currentPos == 1 && PlayerPrefs.GetInt("Level1Finished", 0) > 0 )||
                    (currentPos == 2 && PlayerPrefs.GetInt("Level2Finished", 0) > 0) ||
                    (currentPos == 3 && PlayerPrefs.GetInt("Level3Finished", 0) > 0)) 

                {
                    currentPos++;
                    info.OpenLevelInfo("Level" + currentPos);
                    arrow.transform.position = arrowPos[currentPos].transform.position;
                }

                yield return new WaitForSecondsRealtime(0.2f);
            }
            else if (Input.GetAxisRaw("Vertical") < -0.3 || Input.GetAxisRaw("Horizontal") < -0.3)
            {
                buttonArrow.Play();
                if (currentPos > 0)
                {
                    if (currentPos == 1) info.CloseLevelInfo();
                        currentPos--;
                    if (currentPos > 0) info.OpenLevelInfo("Level" + currentPos);
                    arrow.transform.position = arrowPos[currentPos].transform.position;
                }

                yield return new WaitForSecondsRealtime(0.2f);
            }

            yield return null;
        }
    }
}