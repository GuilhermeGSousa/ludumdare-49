using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IDamageable, IPushable
{
    [SerializeField] private float health = 5f;
    [SerializeField] private float speed  = 3f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;

    private float currentSpeed;
    [SerializeField] private float minDistanceToPlayer = 0.1f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float groundCheckRadius = 0.08f;
    [SerializeField] private float attackDamage = 2f;
    [SerializeField] private float pushImpulse = 2f;
    [SerializeField] private float pushAngle = 20f;
    [SerializeField] private UnityEvent onDeath;
    [SerializeField] private GameObject bloodPrefab;
    private Rigidbody2D rb;
    private Animator animator;
    private GameObject player;
    private bool isFlipped = false;
    private bool isGrounded = false;
    private bool isSlowed = false;
    
    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = speed;
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        PursueTarget();
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayerMask);

        animator.SetBool("isGrounded", isGrounded);
    }

    private void PursueTarget()
    {
        Vector3 playerDirection = player.transform.position - transform.position;
        bool isCloseToPlayer = Mathf.Abs(playerDirection.x) < minDistanceToPlayer;
        if(Mathf.Sign(playerDirection.x) > 0 && isFlipped || Mathf.Sign(playerDirection.x) < 0 && !isFlipped) Flip();

        if(!isCloseToPlayer && health > 0f)
        {
            transform.position += transform.right * (isSlowed ? speed * 0.5f : speed) * Time.deltaTime;
            animator.SetBool("isRunning", true); 
        }
        else
        {
            animator.SetBool("isRunning", false);

            animator.SetTrigger("onAttack");
        }
    }

    public void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius);

        foreach (var collider in colliders)
        {
            if(collider.CompareTag("Enemy")) continue;

            IDamageable damageable = collider.GetComponent<IDamageable>();
            IPushable pushable = collider.GetComponent<IPushable>();

            if(damageable is IDamageable) 
            {
                damageable.OnDamage(attackDamage);
            }
            if(pushable is IPushable)
            {
                pushable.OnPush(new Vector2(Mathf.Sign(transform.right.x) * Mathf.Sin(pushAngle), Mathf.Cos(pushAngle)) * pushImpulse);
            } 
        }
    }

    public void Jump()
    {   
        if(!isGrounded) return;

        // Maybe do jumps for enemies
        
    }

    private void Flip()
    {
        isFlipped = !isFlipped;
        transform.rotation = Quaternion.Euler(0f, isFlipped ? 180f : 0f, 0f);
    }

    public void OnDamage(float damage)
    {
        health -= damage;
        animator.SetTrigger("onDamaged");
        
        StartCoroutine("OnDamageCoroutine");

        if(health <= 0) 
        {
            animator.SetTrigger("onDeath");
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void SetSlow(bool isSlow)
    {
        isSlowed = isSlow;
    }

    public void EmitBlood()
    {
        Instantiate(bloodPrefab, transform.position, transform.rotation);
    }
    

    public void OnPush(Vector2 pushImpulse)
    {
        rb.AddForce(pushImpulse, ForceMode2D.Impulse);
    }

    private void OnDrawGizmos() 
    {
        if(groundCheck)
        {
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

        if(attackPoint)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }
    }

    IEnumerator OnDamageCoroutine()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        float ElapsedTime = 0.0f;
        float TotalTime = 0.5f;
        while (ElapsedTime < TotalTime) 
        {
            ElapsedTime += Time.deltaTime;
            //spriteRenderer.color = Color.Lerp(Color.red, Color.white, (ElapsedTime / TotalTime));
            yield return null;
        }
    }


}
