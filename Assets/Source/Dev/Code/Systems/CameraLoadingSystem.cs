using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using NaughtyAttributes;
using UnityEngine;

public class CameraLoadingSystem : GameSystem
{
    [SerializeField] [Tag] private string _raceCameraTag;
    [SerializeField] [Tag] private string _garageCameraTag;

    public override void OnInit()
    {
        cameraController.GarageCamera = GameObject.FindGameObjectWithTag(_garageCameraTag).GetComponent<CinemachineVirtualCamera>();
        cameraController.RaceCamera = GameObject.FindGameObjectWithTag(_raceCameraTag).GetComponent<CinemachineVirtualCamera>();
    }
}
