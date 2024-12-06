using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public int maxFish = 3; // Máximo número de pescados
    public int currentFish = 0; // Pescados actuales
    public Image[] fishIcons; // Referencias a los iconos
    public Sprite fishFullSprite; // Sprite del pescado lleno
    public Sprite fishEmptySprite; // Sprite del pescado vacío
    public GameManager gameManager;

    private void Start()
    {
        UpdateFishUI(); // Inicializar los iconos
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && currentFish > 0)
        {
            ConsumeFish();
        }
    }

    public void AddFish()
    {
        if (currentFish < maxFish)
        {
            currentFish++;
            Debug.Log($"¡Pescado recogido! Total: {currentFish}/{maxFish}");
            UpdateFishUI(); // Actualizar los peces o la UI cuando se recoja un pescado
        }
        else
        {
            Debug.Log("Inventario de pescados lleno.");
        }
    }

    private void ConsumeFish()
    {
        if (currentFish > 0)
        {
            currentFish--; // Reducir el contador de pescados
            Debug.Log($"Pescado consumido. Total restante: {currentFish}/{maxFish}");

            // Curar al jugador
            gameManager.UpdateCurrentHP(100);

            // Actualizar peces o UI
            UpdateFishUI();
        }
        else
        {
            Debug.Log("No tienes pescados para consumir.");
        }
    }

    private void UpdateFishUI()
    {
        // Actualizar los íconos de los pescados
        for (int i = 0; i < fishIcons.Length; i++)
        {
            if (i < currentFish)
            {
                fishIcons[i].sprite = fishFullSprite; // Ícono lleno
            }
            else
            {
                fishIcons[i].sprite = fishEmptySprite; // Ícono vacío
            }
        }
    }
}
