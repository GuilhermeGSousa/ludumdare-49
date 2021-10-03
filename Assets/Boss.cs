using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject goonPrefab;
    [SerializeField] private Transform spitPoint;
    [SerializeField] private float spitInitialVelocity = 8f;
    [SerializeField] private AudioSource bossAudioSource;
    [SerializeField] private List<AudioClip> laughAudioClips;
    [SerializeField] private List<AudioClip> damagedAudioClips;
    private int hitCount;

    private string[] actionList = {
        "spitGoons",
        "attack"
    };

    private Animator animator;
    
    private void Start() {
        animator = GetComponent<Animator>();
        bossAudioSource.loop = false;
    }

    public void ChooseNextAction()
    {
        animator.SetTrigger(actionList[Random.Range(0, actionList.Length)]);
    }

    public void SpitGoon()
    {
        GameObject goon = Instantiate(goonPrefab, spitPoint.position, transform.rotation, null);

        float spitAngle = Random.Range(0f, 180f);

        goon.GetComponent<Rigidbody2D>().velocity = Quaternion.Euler(0f, 0f, spitAngle) * Vector2.right * spitInitialVelocity;
    }

    public void LaughSFX()
    {
        bossAudioSource.PlayOneShot(laughAudioClips[Random.Range(0, laughAudioClips.Count)]);
    }

    public void DamagedSFX()
    {
        bossAudioSource.PlayOneShot(damagedAudioClips[Random.Range(0, damagedAudioClips.Count)]);
    }

    public void CountHit()
    {
        hitCount++;
    }

    public void OnDamage(float damage)
    {

    }
}
