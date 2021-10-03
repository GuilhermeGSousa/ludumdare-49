using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class Player : MonoBehaviour, IDamageable, IEventListener<bool>
{
    [SerializeField] private PlayerMovementBehaviour playerMovement;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerCry playerCry;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 2f;
    [SerializeField] private float attackDamage = 2f;
    [SerializeField] private float pushImpulse = 2f;
    [SerializeField] private float pushAngle = 20f;
    [SerializeField] private Animator animator;
    [SerializeField] private GameEvent<bool> gamePauseEvent;
    private bool isSad = true;
    
    private void Start() 
    {
        animator.GetComponent<Animator>();
    }
    public void OnMovement(InputAction.CallbackContext value)
    {
        float inputMovement = value.ReadValue<float>();

        playerMovement.SetMovementSpeed(inputMovement);
    }

    public void OnAttack(InputAction.CallbackContext value)
    {
        if(isSad)
        {
            if(value.performed) 
            {
                animator.SetBool("isCrying", true);
                playerCry.SetCrying(true);
            }

            if(value.canceled)
            {
                animator.SetBool("isCrying", false);
                playerCry.SetCrying(false);
            }
        }
        else
        {
            if(value.started) 
            {
                animator.SetTrigger("onAttack");
            }
        }
        
    }

    public void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius);

        foreach (var collider in colliders)
        {
            if(collider.CompareTag("Player")) continue;

            IDamageable damageable = collider.GetComponent<IDamageable>();
            IPushable pushable = collider.GetComponent<IPushable>();

            if(damageable is IDamageable) damageable.OnDamage(attackDamage);
            if(pushable is IPushable)
            {
                pushable.OnPush(new Vector2(Mathf.Sign(transform.right.x) * Mathf.Sin(pushAngle), Mathf.Cos(pushAngle)) * pushImpulse);
            } 
        }
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if(value.performed) 
        {
            playerMovement.Jump();
        }
    }

    private void ProcessAttack()
    {
        
    }

    public void OnDamage(float damage)
    {
        playerHealth.ApplyDamage(damage);
    }

    public void onCryStateChanged(bool cryState)
    {
        
        if(cryState)
        {
            if(!isSad) animator.SetTrigger("onSad");
        }
        else
        {
            animator.SetBool("isCrying", false);
            animator.SetTrigger("onAngry");
        }

        isSad = cryState;
    }

    private void OnEnable() {
        gamePauseEvent.RegisterListener(this);
    }

    private void OnDisable() {
        gamePauseEvent.UnregisterListener(this);
    }

    public void OnEventRaised(bool parameter)
    {
        if(parameter)
        {            
            GetComponent<PlayerInput>().actions.FindActionMap("Movement").Disable();
        }
        else
        {
            GetComponent<PlayerInput>().actions.FindActionMap("Movement").Enable();
        }
    }
    private void OnDrawGizmos() 
    {
        if(attackPoint)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }        
    }
}
