using UnityEngine;

public class SplashSceneController : MonoBehaviour
{
    [SerializeField] private TransitionController transitionController;  // Referencia al script de transición
    private bool isTransitioning = false;  // Bandera para evitar duplicados

    private void Update()
    {
        // Solo detecta cualquier tecla si la entrada está habilitada y no está en transición
        if (Input.anyKeyDown && !isTransitioning)
        {
            Debug.Log("Se cargó el menú de inicio con transición");
            isTransitioning = true;  // Evita duplicados
            transitionController.StartTransition("MainMenu");
        }
    }
}
