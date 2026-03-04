using System;
using UnityEngine;
using System.Collections.Generic;

public enum TankState {
    Idle,
    Patrolling,
    SeekingPlayer,
    Attacking,
    Fleeing,
    Dead
}

[System.Serializable]
public class EnemyTankAI : MonoBehaviour {
    public TankState currentState = TankState.Idle;
    public float patrolRadius = 10f;
    public float chaseRadius = 20f;
    public float attackRadius = 5f;
    public float moveSpeed = 3f;
    public float rotationSpeed = 5f;
    public float attackCooldown = 2f;
    public float lastAttackTime = 0f;
    
    private Transform playerTarget;
    private Rigidbody2D rb;
    private Animator animator;
    
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void Update() {
        if (playerTarget == null) return;
        
        float distanceToPlayer = Vector2.Distance(transform.position, playerTarget.position);
        
        switch (currentState) {
            case TankState.Idle:
                currentState = TankState.Patrolling;
                break;
            
            case TankState.Patrolling:
                if (distanceToPlayer <= chaseRadius) {
                    currentState = TankState.SeekingPlayer;
                }
                break;
            
            case TankState.SeekingPlayer:
                if (distanceToPlayer <= attackRadius) {
                    currentState = TankState.Attacking;
                } else if (distanceToPlayer > chaseRadius * 1.5f) {
                    currentState = TankState.Patrolling;
                }
                break;
            
            case TankState.Attacking:
                if (Time.time - lastAttackTime >= attackCooldown) {
                    Attack();
                    lastAttackTime = Time.time;
                }
                if (distanceToPlayer > attackRadius * 1.5f) {
                    currentState = TankState.SeekingPlayer;
                }
                break;
            
            case TankState.Fleeing:
                if (distanceToPlayer > chaseRadius * 2f) {
                    currentState = TankState.Patrolling;
                }
                break;
            
            case TankState.Dead:
                // Handle death state
                break;
        }
        
        UpdateMovement();
        UpdateAnimator();
    }
    
    void UpdateMovement() {
        Vector2 direction = Vector2.zero;
        
        switch (currentState) {
            case TankState.Patrolling:
                direction = GetRandomPatrolPoint();
                break;
            
            case TankState.SeekingPlayer:
                direction = (playerTarget.position - transform.position).normalized;
                break;
            
            case TankState.Attacking:
                direction = (playerTarget.position - transform.position).normalized;
                break;
            
            case TankState.Fleeing:
                direction = (transform.position - playerTarget.position).normalized;
                break;
        }
        
        if (direction != Vector2.zero) {
            rb.velocity = direction * moveSpeed;
            RotateTowards(direction);
        } else {
            rb.velocity = Vector2.zero;
        }
    }
    
    void RotateTowards(Vector2 direction) {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = Mathf.SmoothDampAngle(rb.rotation, angle, ref angularVelocity, rotationSpeed * Time.deltaTime);
    }
    
    Vector2 GetRandomPatrolPoint() {
        Vector2 randomPoint = transform.position + Random.insideUnitCircle * patrolRadius;
        return (randomPoint - transform.position).normalized;
    }
    
    void Attack() {
        // Fire projectile or trigger attack animation
        animator.SetTrigger("Attack");
    }
    
    void UpdateAnimator() {
        bool isMoving = rb.velocity.magnitude > 0.1f;
        animator.SetBool("IsMoving", isMoving);
        animator.SetFloat("Speed", rb.velocity.magnitude);
    }
    
    public void TakeDamage(float damage) {
        // Handle damage logic
        if (currentState != TankState.Dead) {
            // Optional: trigger flee if health is low
            if (health <= 0) {
                currentState = TankState.Dead;
                animator.SetTrigger("Die");
                rb.velocity = Vector2.zero;
            }
        }
    }
    
    private float health = 100f;
    private float angularVelocity = 0f;
}