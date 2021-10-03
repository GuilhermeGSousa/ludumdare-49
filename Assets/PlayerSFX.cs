using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    [SerializeField] private List<AudioClip> hitAudioClips;
    private AudioSource audioSource;
    private void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
    }
    public void PlayHitSFX()
    {
        AudioClip hitClip = hitAudioClips[Random.Range(0, hitAudioClips.Count)];

        audioSource.PlayOneShot(hitClip);
    }
}
