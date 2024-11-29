using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public int maxHealth = 3;
    public Animator animator; // Referencia al Animator
    [Tooltip("Nombre de la animación que se reproducirá al destruir el objeto.")]
    public string destroyAnimationName = ""; // Nombre de la animación, configurable en el Inspector

    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            PlayDestroyAnimation(); // Ejecutar animación antes de destruir
        }
    }

    void PlayDestroyAnimation()
    {
        if (animator != null)
        {
            if (!string.IsNullOrEmpty(destroyAnimationName))
            {
                animator.Play(destroyAnimationName); // Reproduce la animación configurada
                Destroy(gameObject, GetAnimationLength(destroyAnimationName)); // Destruye el objeto tras finalizar la animación
            }
            else
            {
                Debug.LogWarning("El nombre de la animación no está configurado en " + gameObject.name);
                Destroy(gameObject); // Si no hay nombre de animación, destruye el objeto directamente
            }
        }
        else
        {
            Debug.LogWarning("Animator no configurado en " + gameObject.name);
            Destroy(gameObject); // Si no hay Animator, destruye el objeto directamente
        }
    }

    float GetAnimationLength(string animationName)
    {
        RuntimeAnimatorController controller = animator.runtimeAnimatorController;

        foreach (AnimationClip clip in controller.animationClips)
        {
            if (clip.name == animationName)
            {
                return clip.length;
            }
        }

        Debug.LogWarning("No se encontró la animación: " + animationName);
        return 0; // Devuelve 0 si no encuentra la animación
    }
}
