using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraShaker : MonoBehaviour
{
    public float shakeTime = 0.5f;
    public float shakeIntensity = 5f;
    public void Shake()
    {
        CameraShake.Instance.ShakeCamera(shakeIntensity, shakeTime);
    }
}
