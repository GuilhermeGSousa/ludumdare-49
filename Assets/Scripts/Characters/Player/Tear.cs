using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tear : MonoBehaviour
{
    [SerializeField] private float attackDamage = 0f;
    [SerializeField] private float pushImpulse = 1f;
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
        if(enemy) enemy.SetSlow(true);

        int rng =  Random.Range(0, 100);
        
        if(effectOdds < rng) return;
        
        IDamageable damageable = other.GetComponent<IDamageable>();
        IPushable pushable = other.GetComponent<IPushable>();

        if(damageable is IDamageable)
        {
            damageable.OnDamage(attackDamage);
        }

        if(pushable is IPushable)
        {
            Vector2 pushImpulseVector = (other.transform.position - transform.position).normalized * pushImpulse;
            pushable.OnPush(pushImpulseVector);
        }

        if(pushable is IPushable || damageable is IDamageable) Destroy(transform.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player")) return;

        Enemy enemy = other.GetComponent<Enemy>();
        if(enemy) enemy.SetSlow(false);
    }
}
