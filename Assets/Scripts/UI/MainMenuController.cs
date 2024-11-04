using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private TransitionController transitionController;
    public void Play()
    {
        Debug.Log("Se inicio el nivel 1");
        transitionController.StartTransition("Level1");
    }

    public void Quit()
    {
        Debug.Log("Sali� del juego");
        Application.Quit();
    }
}
