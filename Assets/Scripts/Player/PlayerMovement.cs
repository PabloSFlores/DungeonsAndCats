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

    Vector2 direction;
    Rigidbody2D rigidBody;
    Animator animator;

    bool isAttacking;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // Actualiza la velocidad del personaje
        rigidBody.velocity = direction * speed;
    }

    private void Update()
    {
        Movement();
        Animations();
    }

    private void Movement()
    {
        // Detener movimiento mientras ataca
        if (isAttacking) return;

        // Capturar movimiento del jugador
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        // Iniciar ataque con clic izquierdo
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void Attack()
    {
        // Reproducir animación de ataque
        animator.Play("Attack");
        isAttacking = true;
        direction = Vector2.zero;

        // Detectar objetos en el área de ataque
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Aplicar daño a cada objeto detectado
        foreach (Collider2D obj in hitObjects)
        {
            // Aplicar daño a enemigos
            Enemy enemy = obj.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(attackDamage);
                continue; // Continuar con el siguiente objeto
            }

            // Aplicar daño a objetos destruibles
            DestructibleObject destructible = obj.GetComponent<DestructibleObject>();
            if (destructible != null)
            {
                destructible.TakeDamage(1); // Aplica 1 punto de daño al objeto destruible
            }
        }
    }

    private void Animations()
    {
        // Detener animaciones mientras ataca
        if (isAttacking) return;

        // Ajustar animaciones de movimiento
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

    // Llamado desde un evento en la animación para terminar el ataque
    private void EndAttack()
    {
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        // Dibuja el rango de ataque en el editor
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
