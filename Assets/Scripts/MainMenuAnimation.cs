using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuAnimation : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private RawImage player;
    [SerializeField] private Texture2D defaultPlayer;
    [SerializeField] private Texture2D dash;
    
    
    [SerializeField] private Text title;

    // Update is called once per frame
    void Start()
    {
        title.color = new Color(1, 1, 1, 0);
        player.transform.localPosition = new Vector3(550,-178,0);
        player.GetComponent<RawImage>().texture = dash;
        StartCoroutine(TitleAnimator());
        StartCoroutine(PlayerAnimator());
    }

    IEnumerator TitleAnimator()
    {
        byte t = 0;
        while (t < 255)
        {
            title.color = new Color32(236, 0, 24, t);
            t ++;
            yield return new WaitForSecondsRealtime(0.0025f);
        }

        int i = 5;
        while (true)
        {
            if (i < 10)
            {
                title.transform.localPosition += Vector3.up;
                i++;
            }
            else if (i < 20)
            {
                title.transform.localPosition += Vector3.down;
                i++;
            }
            else
            {
                title.transform.localPosition += Vector3.up;
                i = 1;
            }
            
            yield return new WaitForSecondsRealtime(0.2f);
        }
    }

    IEnumerator PlayerAnimator()
    {
        int t = 0;
        while (t<150)
        {
            player.transform.localPosition -= Vector3.right * 2;
            t++;
            yield return new WaitForSecondsRealtime(0.001f);
        }
        player.transform.localPosition = new Vector3(191,-178,0);
        player.GetComponent<RawImage>().texture = defaultPlayer;
    }
}