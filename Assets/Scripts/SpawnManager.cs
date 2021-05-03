using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemyContainer;
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private GameObject asteroidContainer;
    [SerializeField] private GameObject[] powerups; // 0 = Triple SHot, 1 = Speed Boost, 2 = Shield Boost

    private bool _stopSpawning;

    private void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
        StartCoroutine(SpawnAsteroidRoutine());
    }

  
    private IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            var randomY = Random.Range(-4.53f, 6.0f);
            var positionToSpawn = new Vector3(9.4f, randomY, 0);

            var newEnemy = Instantiate(enemyPrefab, positionToSpawn, Quaternion.identity);
            newEnemy.transform.parent = enemyContainer.transform;
            yield return new WaitForSeconds(4.0f);
        }
    }

    private IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            var randomY = Random.Range(-4.53f, 6.0f);
            var positionToSpawn = new Vector3(9.4f, randomY, 0);
            var randomPowerup = Random.Range(0, 3);
            Instantiate(powerups[randomPowerup], positionToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }
    
    private IEnumerator SpawnAsteroidRoutine()
    {
        yield return new WaitForSeconds(4.0f);
        while (_stopSpawning == false)
        {
            var randomY = Random.Range(-4.53f, 6.0f);
           // var randomX = Random.Range(-2f, 9.3f);
            var positionToSpawn = new Vector3(9.4f, randomY, 0);

            var newAsteroid = Instantiate(asteroidPrefab, positionToSpawn, Quaternion.identity);
            newAsteroid.transform.parent = asteroidContainer.transform;
            yield return new WaitForSeconds(4.0f);
        }
    }


    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}