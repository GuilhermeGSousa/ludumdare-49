using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IDamageable, IPushable
{
    [SerializeField] private float health = 5f;
    [SerializeField] private float speed  = 3f;
    private float currentSpeed;
    [SerializeField] private float minDistanceToPlayer = 0.1f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float groundCheckRadius = 0.08f;
    [SerializeField] private UnityEvent onDeath;
    private Rigidbody2D rb;
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
    }

    private void PursueTarget()
    {
        Vector3 playerDirection = player.transform.position - transform.position;
        bool isCloseToPlayer = Mathf.Abs(playerDirection.x) < minDistanceToPlayer;
        if(Mathf.Sign(playerDirection.x) > 0 && isFlipped || Mathf.Sign(playerDirection.x) < 0 && !isFlipped) Flip();

        if(!isCloseToPlayer)
        {
            transform.position += transform.right * (isSlowed ? speed * 0.5f : speed) * Time.deltaTime;   
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

        if(health <= 0) 
        {
            onDeath.Invoke();
            Destroy(gameObject);
        }
    }

    public void SetSlow(bool isSlow)
    {
        isSlowed = isSlow;
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
    }


}
