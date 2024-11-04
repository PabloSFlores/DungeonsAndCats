using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private AnimationClip endAnimation;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // M�todo p�blico para iniciar la transici�n y cambiar de escena
    public void StartTransition(string sceneName)
    {
        StartCoroutine(ChangeScene(sceneName));
    }

    private IEnumerator ChangeScene(string sceneName)
    {
        // Activa la animaci�n
        animator.SetTrigger("Start");

        // Espera hasta que la animaci�n termine
        yield return new WaitForSeconds(endAnimation.length);

        // Cambiar a la escena especificada
        SceneManager.LoadScene(sceneName);
    }
}