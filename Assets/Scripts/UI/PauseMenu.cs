using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu; // Asigna el GameObject del menú de pausa
    private bool isPaused = false;

    void Update()
    {
        // Detecta cuando presionas la tecla 'Esc'
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Pausar el juego
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Detiene el tiempo del juego
        pauseMenu.SetActive(true); // Activa el menú de pausa
    }

    // Reanudar el juego
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Restaura el tiempo del juego
        pauseMenu.SetActive(false); // Desactiva el menú de pausa
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f; // Restablece la velocidad del juego
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia la escena actual
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f; // Restablece la velocidad del juego
        SceneManager.LoadScene("MainMenu"); // Carga la escena del menú principal
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit(); // Cierra la aplicación (solo funciona cuando ya compilamos el juego)
    }
}
