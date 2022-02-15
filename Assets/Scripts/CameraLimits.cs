using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using Cinemachine;

/// <summary>
/// An add-on module for Cinemachine Virtual Camera that locks the camera's Y co-ordinate
/// </summary>
[SaveDuringPlay]
[AddComponentMenu("")] // Hide in menu
public class CameraLimits : CinemachineExtension
{
    [SerializeField] private GameObject player;
    public float limYPosition;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (player.transform.position.y<limYPosition)
        {
            Vector3 pos = state.RawPosition;
            pos.y = limYPosition;
            state.RawPosition = pos;
        }
    }
}
