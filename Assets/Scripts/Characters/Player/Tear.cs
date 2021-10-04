using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tear : MonoBehaviour
{
    [SerializeField] private float pushImpulse = 1f;
    [SerializeField] private float damageAmount = 1f;
    [Range(0, 100)]
    [SerializeField] private int effectOdds = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player")) return;

        Enemy enemy = other.GetComponent<Enemy>();
        Boss boss = other.GetComponent<Boss>();
        if(enemy)
        {
            enemy.SetSlow(true);

            int rng =  Random.Range(0, 100);
            
            if(effectOdds < rng) return;

            enemy.OnDamage(damageAmount);
            Vector2 pushImpulseVector = (other.transform.position - transform.position).normalized * pushImpulse;
            enemy.OnPush(pushImpulseVector);
            Destroy(gameObject);
        }

        if(boss)
        {
            int rng =  Random.Range(0, 100);
            
            if(effectOdds < rng) return;

            boss.OnDamage(damageAmount);
            Destroy(gameObject);
        }

    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player")) return;

        Enemy enemy = other.GetComponent<Enemy>();
        if(enemy) enemy.SetSlow(false);
    }
}
