using UnityEngine;

public class Fish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerInventory inventory = collision.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.AddFish(); // Añadir pescado al inventario
                Destroy(gameObject); // Eliminar el pescado de la escena
            }
        }
    }
}
