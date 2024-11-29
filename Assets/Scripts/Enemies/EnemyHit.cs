using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    private EnemyHealth enemyHealth;
    private Animator animator;

    private void Start()
    {
        enemyHealth = GetComponentInParent<EnemyHealth>();
        animator = GetComponent<Animator>();
    }

    public void Defeat()
    {
        Debug.Log("Ejecutando animación Death");
        animator.Play("Death");
    }

    // Métodos para invocar eventos desde la animación
    private void Hide()
    {
        Debug.Log("Evento Hide ejecutado");
        enemyHealth.TriggerDeathEvent("Hide");
    }

    private void Destroy()
    {
        Debug.Log("Evento Destroy ejecutado");
        enemyHealth.TriggerDeathEvent("Destroy");
    }
}

