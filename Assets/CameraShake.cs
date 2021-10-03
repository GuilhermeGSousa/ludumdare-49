using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance {get; private set;}
    private CinemachineVirtualCamera cam;
    private float shakeTimer = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin noiseChannel = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        noiseChannel.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

    private void Update() {
        if(shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;

            if(shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin noiseChannel = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                noiseChannel.m_AmplitudeGain = 0f;
            }
        }
    }
}
