using System.Collections;
using UnityEngine;

public class ScrollableCredits : MonoBehaviour
{
    [SerializeField] private RectTransform contentRect; // El RectTransform del Content dentro del ScrollView
    [SerializeField] private float scrollDuration = 10f; // Duración del scroll
    [SerializeField] private float startPositionOffset = -500f; // Offset inicial desde donde empieza el contenido
    [SerializeField] private float endPositionOffset = 1000f; // Offset final hasta donde se moverá el contenido
    [SerializeField] private float delayBeforeStart = 1f; // Delay antes de empezar el desplazamiento

    private void Start()
    {
        // Llama a la función que iniciará el desplazamiento
        StartCreditsScroll();
    }

    private void StartCreditsScroll()
    {
        // Coloca el contenido en la posición inicial (fuera de la pantalla hacia abajo)
        contentRect.anchoredPosition = new Vector2(0, startPositionOffset);

        // Inicia el scroll de manera infinita
        StartCoroutine(InfiniteScroll());
    }

    private IEnumerator InfiniteScroll()
    {
        // Añadir un pequeño delay antes de que inicie el desplazamiento
        yield return new WaitForSeconds(delayBeforeStart);

        while (true)  // Esto hará que el scroll sea infinito
        {
            // Mueve el contenido hacia arriba en el tiempo especificado
            LeanTween.moveY(contentRect, endPositionOffset, scrollDuration)
                     .setEase(LeanTweenType.linear); // Movimiento constante sin aceleración o desaceleración

            // Esperar a que el contenido termine de desplazarse hacia arriba
            yield return new WaitForSeconds(scrollDuration);

            // Después de mover el contenido hacia arriba, reiniciamos su posición
            contentRect.anchoredPosition = new Vector2(0, startPositionOffset);
        }
    }
}
