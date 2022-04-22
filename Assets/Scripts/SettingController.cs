using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingController : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject[] arrowPos;
    [SerializeField] Slider masterVolumeSlider, musicVolumeSlider, effectVolumeSlider,dialogueSpeedSlider;
    [SerializeField] Toggle fullscreen;
    [SerializeField] Dropdown resolutionDropdown;
    [SerializeField] Button returnButton;
    private IEnumerator move;
    int currentPos = 0;

    public AudioSource buttonArrow;

    private void OnEnable()
    {
        StartMove();
    }
    void Update()
    {
        
    }
    public void StartMove()
    {
        currentPos = 0;
        arrow.transform.position = arrowPos[currentPos].transform.position;
        if (move != null) StopCoroutine(move);
        move = ArrowMove();
        StartCoroutine(move);
    }
    IEnumerator ArrowMove()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        while (true)
        {
            if (Input.GetAxisRaw("Jump") > 0)
            {
                switch (currentPos)
                {
                    case 4:
                        fullscreen.isOn = !fullscreen.isOn;
                        yield return new WaitForSecondsRealtime(0.2f);
                        break;
                    case 6:
                        returnButton.onClick.Invoke();
                        yield return new WaitForSecondsRealtime(0.2f);
                        break;
                    case 5:
                        resolutionDropdown.value = (resolutionDropdown.value+1)%resolutionDropdown.options.Count;
                        Debug.Log(resolutionDropdown.value);
                        yield return new WaitForSecondsRealtime(0.2f);
                        break;
                }               
            }
            float horizontal = Input.GetAxisRaw("Horizontal");
            if (horizontal > 0.3f || horizontal < -0.3f)
            {
                switch (currentPos)
                {
                    case 0:
                        masterVolumeSlider.value = masterVolumeSlider.value + (horizontal > 0 ? 0.05f : -0.05f);
                        masterVolumeSlider.onValueChanged.Invoke(masterVolumeSlider.value + (horizontal > 0 ? 0.05f : -0.05f));
                        yield return new WaitForSecondsRealtime(0.2f);
                        break;
                    case 1:
                        musicVolumeSlider.value = musicVolumeSlider.value + (horizontal > 0 ? 0.05f : -0.05f);
                        musicVolumeSlider.onValueChanged.Invoke(musicVolumeSlider.value + (horizontal > 0 ? 0.05f : -0.05f));
                        yield return new WaitForSecondsRealtime(0.2f);
                        break;
                    case 2:
                        buttonArrow.Play();
                        effectVolumeSlider.value = effectVolumeSlider.value + (horizontal > 0 ? 0.05f : -0.05f);
                        effectVolumeSlider.onValueChanged.Invoke(effectVolumeSlider.value + (horizontal > 0 ? 0.05f : -0.05f));
                        yield return new WaitForSecondsRealtime(0.2f);
                        break;
                    case 3:
                        dialogueSpeedSlider.value = dialogueSpeedSlider.value + (horizontal > 0 ? 0.005f : -0.005f);
                        dialogueSpeedSlider.onValueChanged.Invoke(dialogueSpeedSlider.value + (horizontal > 0 ? 0.005f : -0.005f));
                        yield return new WaitForSecondsRealtime(0.2f);
                        break;
                }
               

                
            }

            if (Input.GetAxisRaw("Vertical") > 0.3)
            {
                buttonArrow.Play();
                currentPos = (currentPos - 1);
                currentPos = (currentPos >= 0 ? currentPos : arrowPos.Length - 1);
                arrow.transform.position = arrowPos[currentPos].transform.position;

                yield return new WaitForSecondsRealtime(0.2f);
            }
            else if (Input.GetAxisRaw("Vertical") < -0.3)
            {
                buttonArrow.Play();
                currentPos = (currentPos + 1) % arrowPos.Length;
                Debug.Log(currentPos);
                arrow.transform.position = arrowPos[currentPos].transform.position;

                yield return new WaitForSecondsRealtime(0.2f);

            }
            yield return null;
        }
    }
}
