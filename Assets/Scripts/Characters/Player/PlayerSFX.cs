using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    [SerializeField] private AudioSource hitAudioSource;
    [SerializeField] private AudioSource voiceAudioSource;
    [SerializeField] private AudioClip armWiggleAudioClip;
    [SerializeField] private List<AudioClip> hitAudioClips;
    [SerializeField] private List<AudioClip> cryAudioClips;
    
    private bool isWaitingForCry = false;
    private void Start() {
        hitAudioSource.loop = false;
    }
    public void PlayHitSFX()
    {
        AudioClip hitClip = hitAudioClips[Random.Range(0, hitAudioClips.Count)];

        hitAudioSource.PlayOneShot(hitClip);
    }

    public void PlayCry()
    {
        if(isWaitingForCry) return;

        isWaitingForCry = true;
        StartCoroutine("CryCoroutine");

    }

    public void StopCry()
    {
        voiceAudioSource.Stop();
    }

    public void PlayArmWiggle()
    {
        //hitAudioSource.PlayOneShot(armWiggleAudioClip);
    }

    IEnumerator CryCoroutine()
    {   
        AudioClip cryClip = cryAudioClips[Random.Range(0, cryAudioClips.Count)];
        voiceAudioSource.PlayOneShot(cryClip);

        yield return new WaitForSeconds(Random.Range(2f, 5f));
        
        isWaitingForCry = false;


    }
}
