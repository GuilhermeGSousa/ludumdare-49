using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovementBehaviour : MonoBehaviour
{
    enum WalkType
    {
        Transform,
        RigidBody
    }

    enum JumpType
    {
        Impulse
    }

    [SerializeField] private Animator animator;

    [Header("Movement Settings")]
    [SerializeField] private WalkType walkType;
    [SerializeField] private JumpType jumpType;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private Transform frontWallCheck;
    [SerializeField] private float frontWallCheckRadius;


    [Header("Walking Movement")]
    [SerializeField] private float walkSpeed = 0.1f;
    [SerializeField] private float maxPlayerXSpeed = 2f;
    [SerializeField] private float maxPlayerYSpeed = 10f;

    [Header("Jumping Movement")]
    [SerializeField] private float jumpHeight = 1f;

    private Rigidbody2D rb;
    private float jumpImpulse;
    private bool isFlipped = false;
    private bool isGrounded;
    private bool isMoving;
    private float inputMovementX;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayerMask);
        jumpImpulse = ComputeJumpImpulse();
    }

    // Update is called once per frame
    private void Update()
    {
        CheckGround();
    }

    private void FixedUpdate() 
    {
        Move();
        LimitSpeed();
    }

    public void SetMovementSpeed(float movementSpeed)
    {
        inputMovementX = movementSpeed;

        if(inputMovementX != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
            
        }

        animator.SetBool("isRunning", isMoving);
    }

    private void Move()
    {
        switch (walkType)
        {
            case WalkType.Transform : 
                transform.position += transform.right * Mathf.Abs(inputMovementX) * walkSpeed * Time.deltaTime;
            break;

            case WalkType.RigidBody :

            break;
            default: break;
        }
        

        if(inputMovementX > 0 && isFlipped || inputMovementX < 0 && !isFlipped)
            Flip();
    }

    public void Jump()
    {   
        if(!isGrounded) return;

        animator.SetTrigger("onJump");

        // Maybe jump on animation?
        
        switch (jumpType)
        {
            case JumpType.Impulse:
                rb.AddForce(transform.up * jumpImpulse, ForceMode2D.Impulse);
            break;
        }
        
    }

    private void Flip()
    {
        isFlipped = !isFlipped;
        transform.rotation = Quaternion.Euler(0f, isFlipped ? 180f : 0f, 0f);
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayerMask);

        //animator.SetBool("isGrounded", isGrounded);
    }
    
    private float ComputeJumpImpulse()
    {
        return Mathf.Sqrt(jumpHeight * 2.0f * rb.gravityScale * -Physics2D.gravity.y) * rb.mass;
    }

    private void LimitSpeed()
    {
        rb.velocity = new Vector2(
            Mathf.Clamp(Mathf.Abs(rb.velocity.x), 0, maxPlayerXSpeed) * Mathf.Sign(rb.velocity.x),
            Mathf.Clamp(Mathf.Abs(rb.velocity.y), 0, maxPlayerYSpeed) * Mathf.Sign(rb.velocity.y)
            );
    }

    private void OnDrawGizmos() 
    {
        if(groundCheck && frontWallCheck)
        {
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
            Gizmos.DrawWireSphere(frontWallCheck.position, frontWallCheckRadius);
        }        
    }
}