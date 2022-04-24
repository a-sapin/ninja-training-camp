using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraTransition : MonoBehaviour
{
    public float stayOnToriTime,damping,transitionTime;

    [SerializeField] GameObject tori,player;
    [FormerlySerializedAs("camera")] [SerializeField] CinemachineVirtualCamera cinemachineCamera;

     PlayerManager playerManager;
    [SerializeField] Dialogue dialogues;

    void Start()
    {
        //Debug.Log("YOOO");
        StartCoroutine(ToriToPlayerTransition());
    }
    IEnumerator ToriToPlayerTransition()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        
        Cinemachine.CinemachineTransposer transposer = cinemachineCamera.GetCinemachineComponent<Cinemachine.CinemachineTransposer>();
        float xDampling = transposer.m_XDamping;
        float yDampling = transposer.m_YDamping;
        cinemachineCamera.Follow = tori.transform;
        transposer.m_XDamping = 0;
        transposer.m_YDamping = 0;
        yield return null;
        playerManager.LockGameplayInput();
        yield return new WaitForSeconds(stayOnToriTime);

        cinemachineCamera.Follow = player.transform;
        transposer.m_XDamping = damping;
        transposer.m_YDamping = damping;

        yield return new WaitForSeconds(transitionTime);
        transposer.m_XDamping = xDampling;
        transposer.m_YDamping = yDampling;
        FindObjectOfType<Timer>().RestartTimer();
        playerManager.UnlockGameplayInput();
        if (dialogues != null) dialogues.StartFirstDialogue();
       

        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
