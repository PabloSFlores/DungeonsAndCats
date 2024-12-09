using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ReturnToMenu : MonoBehaviour
{
    public TextMeshProUGUI countdownText; // Referencia al texto del contador
    public float countdownTime = 5f; // Tiempo antes de regresar al menú

    private void Start()
    {
        StartCoroutine(CountdownToMenu());
    }

    private IEnumerator CountdownToMenu()
    {
        float remainingTime = countdownTime;

        while (remainingTime > 0)
        {
            countdownText.text = $"Volviendo al menú en {Mathf.CeilToInt(remainingTime)} segundos...";
            remainingTime -= Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene("MainMenu"); // Cambiar a la escena del menú principal
    }
}
