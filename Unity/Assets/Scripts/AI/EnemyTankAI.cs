using System;
using UnityEngine;

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

    [Header("Health")]
    public float maxHealth = 100f;

    private float currentHealth;
    private Transform playerTarget;
    private Rigidbody2D rb;
    private Animator animator;
    private float angularVelocity = 0f;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTarget = player != null ? player.transform : null;
    }

    void Update() {
        if (playerTarget == null || rb == null || currentState == TankState.Dead) return;

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
                break;
        }

        UpdateMovement();
        UpdateAnimator();
    }

    void UpdateMovement() {
        if (rb == null || playerTarget == null) return;

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
        if (rb == null) return;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = Mathf.SmoothDampAngle(rb.rotation, angle, ref angularVelocity, rotationSpeed * Time.deltaTime);
    }

    Vector2 GetRandomPatrolPoint() {
        Vector2 randomPoint = (Vector2)transform.position + UnityEngine.Random.insideUnitCircle * patrolRadius;
        return (randomPoint - (Vector2)transform.position).normalized;
    }

    void Attack() {
        // Fire projectile or trigger attack animation
        if (animator != null) {
            animator.SetTrigger("Attack");
        }
    }

    void UpdateAnimator() {
        if (animator == null || rb == null) return;

        bool isMoving = rb.velocity.magnitude > 0.1f;
        animator.SetBool("IsMoving", isMoving);
        animator.SetFloat("Speed", rb.velocity.magnitude);
    }

    public void TakeDamage(float damage) {
        if (currentState == TankState.Dead) return;

        currentHealth -= damage;
        if (currentHealth <= 0f) {
            currentHealth = 0f;
            currentState = TankState.Dead;

            if (animator != null) {
                animator.SetTrigger("Die");
            }

            if (rb != null) {
                rb.velocity = Vector2.zero;
            }
        }
    }
}
