using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    private EnemyHealth enemyHealth;
    private Animator animator;

    private void Start()
    {
        enemyHealth = GetComponentInParent<EnemyHealth>();
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator no encontrado en " + gameObject.name);
        }
    }

    public void Defeat()
    {
        Debug.Log("Ejecutando animación Death");

        if (animator != null)
        {
            animator.Play("Death");
        }
        else
        {
            Debug.LogWarning("No se puede ejecutar la animación de muerte. Animator no asignado.");
        }
    }
}
