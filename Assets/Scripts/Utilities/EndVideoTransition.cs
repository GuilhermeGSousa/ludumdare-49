using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Events;
public class EndVideoTransition : MonoBehaviour
{
    [SerializeField] private UnityEvent onVideoEnd;
    private VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        videoPlayer.loopPointReached += OnVideoEnd;
    }

    private void OnVideoEnd(UnityEngine.Video.VideoPlayer vp)
    {
        onVideoEnd.Invoke();
    }
}
