using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	[SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject[] powerups;
	[SerializeField] private GameObject _enemyContainer;

	private bool _stopSpawning;
    

    public void StartSpawning()
    {
    	StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerup());
    }

    IEnumerator SpawnEnemy()
    {
    	yield return new WaitForSeconds(3.0f);
    	while(_stopSpawning == false)
    	{
    		float x_pos = Random.Range(-9.5f, 9.5f);
    		GameObject newEnemy = Instantiate(_enemy, new Vector3(x_pos, 7.0f, 0), Quaternion.identity);
    		newEnemy.transform.parent = _enemyContainer.transform;
    		yield return new WaitForSeconds(2.0f);
    	}
    }

    IEnumerator SpawnPowerup()
    {
    	yield return new WaitForSeconds(3.0f);
        while(_stopSpawning == false)
        {
            float x_pos = Random.Range(-9.5f, 9.5f);
            float spawnTime = Random.Range(3.0f, 7.0f);
            int spawnType = Random.Range(0, 3);

            Instantiate(powerups[spawnType], new Vector3(x_pos, 7.0f, 0), Quaternion.identity);
            yield return new WaitForSeconds(spawnTime);
        }
    }

    public void OnPlayerDeath()
    {
    	_stopSpawning = true;
    }
}
