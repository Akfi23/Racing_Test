using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[Serializable]
public class CameraController
{
    public CinemachineVirtualCamera GarageCamera { get; set; }
    public CinemachineVirtualCamera RaceCamera { get; set; }

    public void SetRaceCameraActive()
    {
        if (GarageCamera.gameObject.activeSelf)
            GarageCamera.gameObject.SetActive(false);
    }

    public void SetGarageCameraActive()
    {
        if (!GarageCamera.gameObject.activeSelf)
            GarageCamera.gameObject.SetActive(true);
    }
}
