using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public int attackDamage = 10; // Daño del ataque
    public Transform attackPoint; // Punto desde donde se detecta el ataque
    public float attackRange = 0.5f; // Rango del ataque
    public LayerMask enemyLayers; // Capas de los enemigos que pueden recibir daño

    private Vector2 direction;
    private Rigidbody2D rigidBody;
    private Animator animator;
    private BasicInteraction basicInteraction;

    private bool isAttacking;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = direction * speed;
    }

    private void Update()
    {
        Movement();
        Animations();
        Inputs();
    }

    private void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (basicInteraction != null)
            {
                Vector2 playerFacing = new Vector2(animator.GetFloat("Horizontal"), animator.GetFloat("Vertical"));
                if (!basicInteraction.Interact(playerFacing, transform.position))
                {
                    Attack();
                }
            }
        }
    }

    private void Movement()
    {
        if (isAttacking || Time.timeScale == 0) return;

        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (attackPoint == null)
        {
            Debug.LogError("AttackPoint no asignado en el jugador.");
            return;
        }

        animator.Play("Attack");
        isAttacking = true;
        direction = Vector3.zero;

        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D obj in hitObjects)
        {
            EnemyHealth enemy = obj.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(attackDamage);
                continue;
            }

            DestructibleObject destructible = obj.GetComponent<DestructibleObject>();
            if (destructible != null)
            {
                destructible.TakeDamage(1); // Aplica 1 punto de daño al objeto destruible
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interaction"))
        {
            basicInteraction = collision.GetComponent<BasicInteraction>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interaction"))
        {
            basicInteraction = null;
        }
    }

    private void Animations()
    {
        if (isAttacking) return;

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
    }

    private void EndAttack()
    {
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
