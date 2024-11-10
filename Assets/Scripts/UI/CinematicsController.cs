using UnityEngine;

public class CinematicsController : MonoBehaviour
{
    [SerializeField] private TransitionController transitionController;
    [SerializeField] private string nextSceneName;

    public void OnCinematicEnd()
    {
        transitionController.StartTransition(nextSceneName);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Debug.Log("Se cargó: " + nextSceneName);
            transitionController.StartTransition(nextSceneName);
        }
    }
}
