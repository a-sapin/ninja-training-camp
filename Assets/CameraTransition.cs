using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    public float stayOnToriTime,speed;

    [SerializeField] GameObject tori,player;
    [SerializeField] CinemachineVirtualCamera camera;

     PlayerManager playerManager;
    [SerializeField] Dialogue dialogues;

    void Start()
    {
        Debug.Log("YOOO");
        StartCoroutine(ToriToPlayerTransition());
    }
    IEnumerator ToriToPlayerTransition()
    {
        FindObjectOfType<Timer>().PauseTimer();

        Cinemachine.CinemachineTransposer transposer = camera.GetCinemachineComponent<Cinemachine.CinemachineTransposer>();
        float xDampling = transposer.m_XDamping;
        float yDampling = transposer.m_YDamping;
        camera.Follow = tori.transform;
        transposer.m_XDamping = 0;
        transposer.m_YDamping = 0;
        Debug.Log("YOOO11");
        yield return new WaitForSeconds(stayOnToriTime);

        playerManager = FindObjectOfType<PlayerManager>();
        playerManager.LockGameplayInput();

        camera.Follow = player.transform;
        transposer.m_XDamping = speed;
        transposer.m_YDamping = speed;

        yield return new WaitForSeconds(speed/1.2f);
        transposer.m_XDamping = xDampling;
        transposer.m_YDamping = yDampling;
        FindObjectOfType<Timer>().RestartTimer();
        if (dialogues != null) dialogues.StartFirstDialogue();
        playerManager.UnlockGameplayInput();

        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
