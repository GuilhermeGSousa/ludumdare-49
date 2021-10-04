using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IDamageable
{
    [SerializeField] private GameEvent<float> onDamagedEvent;
    [SerializeField] private GameEvent onDeath;
    [SerializeField] private float maxHealth = 20f;
    private float health;
    [SerializeField] private GameObject goonPrefab;
    [SerializeField] private Transform spitPoint;
    [SerializeField] private float spitInitialVelocity = 8f;
    [SerializeField] private AudioSource bossAudioSource;
    [SerializeField] private List<AudioClip> laughAudioClips;
    [SerializeField] private List<AudioClip> damagedAudioClips;
    [SerializeField] private List<AudioClip> screamAudioClips;
    [SerializeField] private AudioClip deathAudioClip;

    [SerializeField] private Color damagedColor;

    private string[] actionList = {
        "spitGoons",
        "attack"
    };

    private Animator animator;
    
    private void Start() {
        animator = GetComponent<Animator>();
        bossAudioSource.loop = false;

        health = maxHealth;
        onDamagedEvent.Raise(1f);
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
    public void ScreamSFX()
    {
        bossAudioSource.PlayOneShot(screamAudioClips[Random.Range(0, screamAudioClips.Count)]);
    }

    public void DeathSFX()
    {
        bossAudioSource.PlayOneShot(deathAudioClip);
    }

    public void OnDamage(float damage)
    {
        health -= damage;
        onDamagedEvent.Raise(health / maxHealth);
        animator.SetTrigger("damaged");
        StartCoroutine("OnDamageCoroutine");

        if(health <= 0)
        {
            animator.SetTrigger("dead");
        }
    }

    public void OnBossDeath()
    {
        onDeath.Raise();
    }

    IEnumerator OnDamageCoroutine()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        float ElapsedTime = 0.0f;
        float TotalTime = 0.2f;
        while (ElapsedTime < TotalTime) 
        {
            ElapsedTime += Time.deltaTime;
            spriteRenderer.color = Color.Lerp(damagedColor, Color.white, (ElapsedTime / TotalTime));
            yield return null;
        }
    }
}
