using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public int sceneIndex;

    public Vector2 playerDestination;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("¡Continuará! Cambiando a la escena final.");
            DataInstance.Instance.playerPosition = playerDestination;
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
