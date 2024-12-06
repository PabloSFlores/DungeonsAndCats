using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // Prefab del enemigo que se generará
    public Transform[] spawnPoints;  // Puntos donde los enemigos pueden aparecer
    public float spawnInterval = 2f;  // Intervalo de tiempo entre apariciones
    public float spawnDuration = 10f;  // Tiempo durante el cual los enemigos aparecerán

    private bool spawning = false;

    void Start()
    {
        // Inicia la generación de enemigos
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        float elapsedTime = 0f;

        while (elapsedTime < spawnDuration)
        {
            // Llama a la función para generar a los enemigos
            SpawnEnemy();
            elapsedTime += spawnInterval;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Función para generar un enemigo en una posición aleatoria
    void SpawnEnemy()
    {
        if (spawnPoints.Length > 0)
        {
            // Elegir un punto de spawn aleatorio
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomIndex];

            // Instanciar el enemigo en el punto de spawn
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
