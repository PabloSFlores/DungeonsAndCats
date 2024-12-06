using System.Collections;
using UnityEngine;

public class RandomPatrol : EnemyHealth
{
    [Header("RandomPatrol parameters")]
    public float speed;
    public float minPatrolTime;
    public float maxPatrolTime;
    public float minWaitTime;
    public float maxWaitTime;
    public float detectionRange = 5f;
    public float attackRange = 1.5f;
    public float attackCooldown = 1f;

    private Animator animator;
    private Vector2 direction;
    private Transform player;

    private bool isAttacking = false;
    private Coroutine patrolCoroutine;

    public override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        PatrolBehavior();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            if (distanceToPlayer <= attackRange && !isAttacking)
            {
                StartCoroutine(Attack());
            }
            else if (!isAttacking)
            {
                MoveTowardsPlayer();
            }
        }
        else if (!isAttacking)
        {
            if (patrolCoroutine == null)
            {
                PatrolBehavior();
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        direction = (player.position - transform.position).normalized;
        rigidbody.velocity = direction * speed;
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.Play("Run");
    }

    IEnumerator Attack()
    {
        if (isAttacking) yield break;

        isAttacking = true;
        rigidbody.velocity = Vector2.zero; // Detener movimiento
        animator.Play("Attack");

        Debug.Log("Intentando atacar al jugador");

        PlayerMovement playerHealth = player?.GetComponent<PlayerMovement>();
        if (playerHealth != null)
        {
            Debug.Log("Jugador detectado. Aplicando daño.");
            playerHealth.TakeDamage(10);
            Debug.Log("Jugador recibe daño: -10 HP");
        }
        else
        {
            Debug.LogWarning("No se encontró el componente PlayerMovement");
        }

        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        // Visualización del rango de detección y ataque
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }


    private void PatrolBehavior()
    {
        if (patrolCoroutine != null) StopCoroutine(patrolCoroutine);
        patrolCoroutine = StartCoroutine(Patrol());
    }

    IEnumerator Patrol()
    {
        direction = RandomDirection();
        Animations();
        yield return new WaitForSeconds(Random.Range(minPatrolTime, maxPatrolTime));

        direction = Vector2.zero;
        Animations();
        yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));

        PatrolBehavior();
    }

    private Vector2 RandomDirection()
    {
        int x = Random.Range(0, 8);

        return x switch
        {
            0 => Vector2.up,
            1 => Vector2.down,
            2 => Vector2.left,
            3 => Vector2.right,
            4 => new Vector2(1, 1),
            5 => new Vector2(1, -1),
            6 => new Vector2(-1, 1),
            _ => new Vector2(-1, -1),
        };
    }

    private void Animations()
    {
        if (direction.magnitude != 0)
        {
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
            animator.Play("Run");
        }
        else
        {
            animator.Play("Idle");
        }

        rigidbody.velocity = direction.normalized * speed;
    }

    public override void StopBehaviour()
    {
        if (patrolCoroutine != null) StopCoroutine(patrolCoroutine);
        rigidbody.velocity = Vector2.zero;
        animator.Play("Idle");
    }

    public override void ContinueBehaviour()
    {
        if (patrolCoroutine == null)
        {
            PatrolBehavior();
        }
    }
}
