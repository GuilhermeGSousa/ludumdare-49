using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioClip sadMusic;
    [SerializeField] private AudioClip angryMusic;
    private AudioSource audioSource;
    

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.loop = true;
    }

    // Update is called once per frame
    public void PlaySadSong(bool isSad)
    {
        audioSource.Stop();

        if(isSad)
        {
            audioSource.PlayOneShot(sadMusic);
        }
        else
        {
            audioSource.PlayOneShot(angryMusic);
        }
    }
}
