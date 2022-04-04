using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    public GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    public GameObject[] powerups;

    private bool _stopSpawning = false;

    private GameManager _gameManager;

    
    public void StartSpawning()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

   
    /// <summary>
    /// spawn game objects every 5 seconds 
    /// Create coroutine of type IEnukmerator -- Yield Events
    /// while loop
    /// </summary>

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        /// <summary>
        /// while loop (infinite loop)
        /// Instantiate enemy prefab
        /// yield wait for 5 seconds 
        /// </summary>
        
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
        
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(powerups[randomPowerUp], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3,9f));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
