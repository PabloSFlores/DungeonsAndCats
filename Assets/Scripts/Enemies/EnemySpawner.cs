using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // Prefab del enemigo que se generará
    public Transform[] spawnPoints;  // Puntos donde los enemigos pueden aparecer
    public float spawnInterval = 2f;  // Intervalo de tiempo entre apariciones
    public float spawnDuration = 10f;  // Tiempo durante el cual los enemigos aparecerán

    private bool spawning = false;

    public void StartSpawning()
    {
        if (!spawning)
        {
            spawning = true;
            StartCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        float elapsedTime = 0f;

        while (elapsedTime < spawnDuration)
        {
            SpawnEnemy();
            elapsedTime += spawnInterval;
            yield return new WaitForSeconds(spawnInterval);
        }

        spawning = false; // Finaliza el proceso de spawn
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomIndex];

            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
