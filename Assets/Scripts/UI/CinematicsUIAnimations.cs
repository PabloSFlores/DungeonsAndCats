using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CinematicsUIAnimations : MonoBehaviour
{
    [SerializeField] private CinematicsController cinematicsController;  // Referencia al controlador de la cinemática
    [SerializeField] private List<GameObject> images;  // Lista de imágenes para la cinemática
    [SerializeField] private List<string> cinematicTexts;  // Lista de textos para la cinemática
    [SerializeField] private GameObject textContainer; // Contenedor del texto
    [SerializeField] private TextMeshProUGUI cinematicText;  // Texto que se va a escribir
    [SerializeField] private float displayTime = 3f;  // Tiempo adicional después de la escritura
    [SerializeField] private float fadeTime = 0.5f;    // Tiempo de desvanecimiento
    [SerializeField] private float typingSpeed = 0.1f; // Velocidad de escritura

    void Start()
    {
        StartCoroutine(PlayCinematic());
    }

    private IEnumerator PlayCinematic()
    {
        // yield return new WaitForSeconds(5f);
        for (int i = 0; i < images.Count; i++)
        {
            // Ocultar todas las imágenes y el contenedor al inicio de cada ciclo
            foreach (var img in images)
            {
                SetImageAlpha(img, 0f);
            }
            SetImageAlpha(textContainer, 0f);

            yield return new WaitForSeconds(1f);

            // Desvanecer imagen y contenedor de texto
            LeanTween.alpha(textContainer.GetComponent<RectTransform>(), 1f, fadeTime);
            LeanTween.alpha(images[i].GetComponent<RectTransform>(), 1f, fadeTime);

            yield return new WaitForSeconds(fadeTime);

            // Iniciar efecto de escritura en el texto correspondiente
            cinematicText.text = "";  // Limpiar texto
            yield return StartCoroutine(TypeText(cinematicTexts[i]));

            // Mantener la escena
            yield return new WaitForSeconds(displayTime);

            // Desvanecer imagen y contenedor de texto
            cinematicText.text = "";  // Limpiar texto
            LeanTween.alpha(textContainer.GetComponent<RectTransform>(), 0f, fadeTime);
            LeanTween.alpha(images[i].GetComponent<RectTransform>(), 0f, fadeTime);

            yield return new WaitForSeconds(fadeTime);
        }

        // Notifica al controlador de la cinemática que ha terminado
        cinematicsController.OnCinematicEnd();
    }

    private IEnumerator TypeText(string text)
    {
        foreach (char letter in text)
        {
            cinematicText.text += letter;
            yield return new WaitForSeconds(typingSpeed);  // Controla la velocidad de escritura
        }
    }

    private void SetImageAlpha(GameObject image, float alpha)
    {
        Image img = image.GetComponent<Image>();
        if (img != null)
        {
            Color color = img.color;
            color.a = alpha;
            img.color = color;
        }
    }
}
