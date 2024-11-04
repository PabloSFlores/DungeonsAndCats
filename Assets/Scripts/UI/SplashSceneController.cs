using UnityEngine;

public class SplashSceneController : MonoBehaviour
{
    [SerializeField] private TransitionController transitionController;  // Referencia al script de transici�n
    private bool isTransitioning = false;  // Bandera para evitar duplicados

    private void Update()
    {
        // Solo detecta cualquier tecla si la entrada est� habilitada y no est� en transici�n
        if (Input.anyKeyDown && !isTransitioning)
        {
            Debug.Log("Se carg� el men� de inicio con transici�n");
            isTransitioning = true;  // Evita duplicados
            transitionController.StartTransition("MainMenu");
        }
    }
}
