using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCameraShaker : MonoBehaviour
{
    public CinemachineImpulseSource impulseSource;
    
    public void Shake()
    {
        impulseSource.GenerateImpulse();
    }
}
