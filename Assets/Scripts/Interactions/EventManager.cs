using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EventManager : MonoBehaviour
{
    [Header("NPC Indicator")]
    public Transform npcTransform;
    public RectTransform npcArrowIndicator;
    public Canvas canvas;
    public Transform player;

    [Header("Enemy Spawners")]
    public EnemySpawner[] enemySpawners;

    [Header("Countdown Timer")]
    public TextMeshProUGUI countdownText;
    public float countdownTime = 300f;

    [Header("End Screen")]
    public GameObject endScreen;
    public Transform destination;

    private bool isCountdownActive = false;

    void Update()
    {
        if (!isCountdownActive)
        {
            UpdateNpcIndicator();
        }
    }

    public void StartEventSequence()
    {
        if (npcArrowIndicator != null)
        {
            npcArrowIndicator.gameObject.SetActive(false);
        }

        ActivateSpawners();
        StartCoroutine(StartCountdown());
    }

    private void UpdateNpcIndicator()
    {
        if (npcTransform == null || npcArrowIndicator == null || player == null)
            return;

        // Obtener la dirección hacia el NPC desde el jugador
        Vector2 directionToNpc = (npcTransform.position - player.position).normalized;

        // Calcular el ángulo entre el jugador y el NPC
        float angle = Mathf.Atan2(directionToNpc.y, directionToNpc.x) * Mathf.Rad2Deg;

        // Ajustar la rotación de la flecha para que apunte al NPC
        npcArrowIndicator.rotation = Quaternion.Euler(0, 0, angle);

        // Verificar si el NPC está dentro de los límites de la pantalla
        Vector3 npcScreenPosition = Camera.main.WorldToScreenPoint(npcTransform.position);
        bool isNpcVisible = npcScreenPosition.x > 0 && npcScreenPosition.x < Screen.width &&
                            npcScreenPosition.y > 0 && npcScreenPosition.y < Screen.height &&
                            npcScreenPosition.z > 0;

        // Mostrar u ocultar el indicador según la visibilidad del NPC
        if (isNpcVisible)
        {
            npcArrowIndicator.gameObject.SetActive(false); // Ocultar la flecha si el NPC es visible
        }
        else
        {
            npcArrowIndicator.gameObject.SetActive(true); // Mostrar la flecha si el NPC no es visible

            // Calcular la posición del indicador en el borde de la pantalla
            Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
            Vector2 screenDirection = (new Vector2(npcScreenPosition.x, npcScreenPosition.y) - screenCenter).normalized;

            float offsetY = 300f; // Ajustar este valor según lo necesario para mover la flecha hacia arriba
            npcArrowIndicator.anchoredPosition = screenDirection * (canvas.GetComponent<RectTransform>().sizeDelta.x / 2 - 50) + new Vector2(0, offsetY);
        }
    }

    private IEnumerator StartCountdown()
    {
        isCountdownActive = true;

        float remainingTime = countdownTime;
        while (remainingTime > 0)
        {
            UpdateCountdownUI(remainingTime);
            remainingTime -= Time.deltaTime;
            yield return null;
        }

        UpdateCountdownUI(0);

        // Cambiar el objetivo al destino final
        npcTransform = destination;
        npcArrowIndicator.gameObject.SetActive(true);
        isCountdownActive = false;

        Debug.Log("El temporizador terminó. Dirígete al destino para continuar.");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Colisión detectada con: {other.name}");

        if (other.CompareTag("Destination"))
        {
            Debug.Log("Trigger alcanzado. Mostrando pantalla de fin.");
            ShowEndScreen(); // Mostrar pantalla final
        }
    }

    private void ShowEndScreen()
    {
        Debug.Log("¡Continuará! Cambiando a la escena final.");
        SceneManager.LoadScene("Continuara");
    }

    private void ActivateSpawners()
    {
        foreach (var spawner in enemySpawners)
        {
            spawner.StartSpawning();
        }
    }

    private void UpdateCountdownUI(float timeRemaining)
    {
        if (countdownText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            countdownText.text = $"{minutes:D2}:{seconds:D2}";
        }
    }
}
