using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    [SerializeField]
    private GameObject _enemyShipPrefab;
    [SerializeField]
    private GameObject[] powerups;

    private GameManager _gameManager;
	// Use this for initialization
	void Start ()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
	}
	
    public void StartSpawnRoutines()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    //spawn enemy every 3 sec
    IEnumerator EnemySpawnRoutine()
    {
        while (_gameManager.gameOver == false)
        {
            Instantiate(_enemyShipPrefab, new Vector3(Random.Range(-7f, 7f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(3.0f);
        }
    }

    IEnumerator PowerupSpawnRoutine()
    {
        while (_gameManager.gameOver == false)
        {
            int randomPowerup = Random.Range(0, 3);
            Instantiate(powerups[randomPowerup], new Vector3(Random.Range(-7, 7), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(13.0f);
        }
    }
}
