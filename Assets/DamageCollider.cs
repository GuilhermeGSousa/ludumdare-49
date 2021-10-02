using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    [SerializeField] private float damageAmount = 3.1f;

    [SerializeField] private bool onCollision = true;
    [SerializeField] private bool onTrigger = true;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(!onCollision) return;

        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();

        if(damageable is IDamageable)
        {
            damageable.OnDamage(damageAmount);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(!onTrigger) return;
        
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();

        if(damageable is IDamageable)
        {
            damageable.OnDamage(damageAmount);
        }
    }
}
