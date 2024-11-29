using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("EnemyHP parameters")]
    public int maxHP = 50;
    public int hp = 50;

    public float knockbackStrength = 2f;
    protected float knockbackTime = 0.3f;

    protected bool invincible;
    protected float invincibilityTime = 0.8f;
    protected float blinkTime = 0.1f;

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
        Debug.Log("Enemigo recibió daño. Vida restante: " + hp);

        if (hp <= 0)
        {
            StartCoroutine(DieSequence()); // Cambiado para invocar la animación
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
        GetComponentInChildren<EnemyHit>()?.Defeat(); // Invoca la animación de muerte
        yield break; // No destruye directamente al enemigo, la animación lo manejará
    }

    public void TriggerDeathEvent(string eventName)
    {
        if (eventName == "Hide")
        {
            HideEnemy();
        }
        else if (eventName == "Destroy")
        {
            Destroy(gameObject);
        }
    }


    private IEnumerator Invincibility()
    {
        invincible = true;
        float auxTime = invincibilityTime;

        while (auxTime > 0)
        {
            yield return new WaitForSeconds(blinkTime);
            auxTime -= blinkTime;
            spriteRenderer.enabled = !spriteRenderer.enabled; // Parpadeo
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

        Debug.Log("Knockback iniciado hacia: " + knockbackDirection);

        yield return new WaitForSeconds(knockbackTime);
        rigidbody.velocity = Vector2.zero;
        if (hp > 0) ContinueBehaviour();
    }

    public void HideEnemy()
    {
        StopAllCoroutines();
        rigidbody.velocity = Vector3.zero;
        spriteRenderer.enabled = false;
    }

    public virtual void StopBehaviour() { }

    public virtual void ContinueBehaviour() { }
}
