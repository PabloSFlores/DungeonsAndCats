using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 50;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemigo recibió daño. Vida restante: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemigo derrotado");
        Destroy(gameObject); // Destruye al enemigo
    }
}
