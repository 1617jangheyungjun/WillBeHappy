using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraShake : MonoBehaviour
{
    CinemachineVirtualCamera cinemachine;    
    CinemachineBasicMultiChannelPerlin cine;
    PlayerMovement movement;
    public NoiseSettings noiseSettings;

    void Start()
    {
        cine = cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        movement = GetComponent<PlayerMovement>();
    }

    void update()
    {
        
    }


}
