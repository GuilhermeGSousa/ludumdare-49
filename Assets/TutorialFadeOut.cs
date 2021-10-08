using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Events;

public class TutorialFadeOut : MonoBehaviour
{
    [SerializeField] private Volume volume;
    [SerializeField] private float fadeOutTime = 2f;
    [SerializeField] private UnityEvent onFadeStart;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeOut()
    {
        StartCoroutine("Fade");
    }

    public IEnumerator Fade()
    {
        volume.profile.TryGet<DepthOfField>(out DepthOfField depthOfField);

        float currentTime = 0f;
        float startFocalLength = depthOfField.focalLength.value;
        onFadeStart.Invoke();
        while (currentTime < fadeOutTime)
        {
            currentTime += Time.unscaledDeltaTime;
            depthOfField.focalLength.value = Mathf.Lerp(startFocalLength, 1f, currentTime / fadeOutTime);
            yield return null;
        }

        Time.timeScale = 1f;
    }
}
