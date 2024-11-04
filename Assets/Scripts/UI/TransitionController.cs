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

    // Método público para iniciar la transición y cambiar de escena
    public void StartTransition(string sceneName)
    {
        StartCoroutine(ChangeScene(sceneName));
    }

    private IEnumerator ChangeScene(string sceneName)
    {
        // Activa la animación
        animator.SetTrigger("Start");

        // Espera hasta que la animación termine
        yield return new WaitForSeconds(endAnimation.length);

        // Cambiar a la escena especificada
        SceneManager.LoadScene(sceneName);
    }
}