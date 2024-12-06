using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("EnemyHP parameters")]
    public int maxHP = 50;
    public int hp = 50;
    public GameObject fishPrefab;

    public float knockbackStrength = 2f;
    protected float knockbackTime = 0.3f;

    protected bool invincible;
    protected float invincibilityTime = 0.8f;
    protected float blinkTime = 0.1f;

    private Animator animator;
    protected Rigidbody2D rigidbody;
    protected SpriteRenderer spriteRenderer;

    public virtual void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer no encontrado en " + gameObject.name);
        }
        hp = maxHP;
    }

    public void TakeDamage(int damage)
    {
        if (invincible) return;

        hp -= damage;
        Debug.Log($"Enemigo {gameObject.name} recibe daño: -{damage} HP. Vida restante: {hp}");

        if (hp <= 0)
        {
            StartCoroutine(DieSequence());
        }
        else
        {
            StopBehaviour();
            StartCoroutine(Invincibility());
            StartCoroutine(Knockback(transform.position));
        }
    }


    private IEnumerator DieSequence()
    {
        Debug.Log("Enemigo derrotado");

        // Instanciar el pescado
        if (fishPrefab != null)
        {
            Instantiate(fishPrefab, transform.position, Quaternion.identity); // Crear pescado en la posición del enemigo
        }

        // Ejecutar la animación de muerte
        GetComponentInChildren<EnemyHit>()?.Defeat();

        yield return new WaitForSeconds(0.5f); // Esperar antes de destruir el enemigo
        Destroy(gameObject); // Eliminar el enemigo
    }

    private IEnumerator Invincibility()
    {
        invincible = true;
        float auxTime = invincibilityTime;

        while (auxTime > 0)
        {
            yield return new WaitForSeconds(blinkTime);
            auxTime -= blinkTime;
            spriteRenderer.enabled = !spriteRenderer.enabled;
        }

        spriteRenderer.enabled = true;
        invincible = false;
    }

    private IEnumerator Knockback(Vector3 hitPosition)
    {
        if (knockbackStrength <= 0)
        {
            if (hp > 0) ContinueBehaviour();
            yield break;
        }

        Vector2 knockbackDirection = (transform.position - hitPosition).normalized;
        rigidbody.velocity = knockbackDirection * knockbackStrength;

        yield return new WaitForSeconds(knockbackTime);
        rigidbody.velocity = Vector2.zero;

        if (hp > 0) ContinueBehaviour();
    }

    public virtual void StopBehaviour() { }
    public virtual void ContinueBehaviour() { }
}
