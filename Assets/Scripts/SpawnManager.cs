using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	[SerializeField] private GameObject[] _enemies;
    [SerializeField] private GameObject[] powerups;
	[SerializeField] private GameObject _enemyContainer;

	private bool _stopSpawning;
    private int _waveNum = 1;
    

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
            _waveNum += 1;
            int enemyType;
            for(int i = 0; i < _waveNum; i++)
            {
                float x_pos = Random.Range(-9.5f, 9.5f);
                float randomType = Random.Range(0.0f, 1.0f);
                if(randomType >= 0.0f && randomType <= 0.6f)
                {
                    enemyType = 0;
                }
                else if(randomType > 0.6f && randomType <= 0.9f)
                {
                    enemyType = 2;
                }
                else
                {
                    enemyType = 1;
                }
                GameObject newEnemy = Instantiate(_enemies[enemyType], new Vector3(x_pos, 7.0f, 0), Quaternion.identity);
                newEnemy.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(2.0f);
            }
            yield return new WaitForSeconds(10.0f);
        }
    		
    }

    IEnumerator SpawnPowerup()
    {
    	yield return new WaitForSeconds(3.0f);
        while(_stopSpawning == false)
        {
            float x_pos = Random.Range(-9.5f, 9.5f);
            float spawnTime = Random.Range(3.0f, 7.0f);
            int spawnType = 3;
            float randomType = Random.Range(0.0f, 1.0f);
            if(randomType >= 0.0f && randomType <= 0.25f)
            {
                spawnType = 3;
            }
            else if(randomType > 0.25f && randomType <= 0.45f)
            {
                spawnType = 0;
            }
            else if(randomType > 0.45f && randomType <= 0.65f)
            {
                spawnType = 1;
            }
            else if(randomType > 0.65f && randomType <= 0.85f)
            {
                spawnType = 2;
            }
            else if(randomType > 0.85f && randomType <= 0.9f)
            {
                spawnType = 4;
            }
            else if(randomType > 0.9f && randomType <= 0.95f)
            {
                spawnType = 5;
            }
            else if(randomType > 0.95f && randomType <= 1.0f)
            {
                spawnType = 6;
            }
            Instantiate(powerups[spawnType], new Vector3(x_pos, 7.0f, 0), Quaternion.identity);
            yield return new WaitForSeconds(spawnTime);
        }
    }

    public void OnPlayerDeath()
    {
    	_stopSpawning = true;
    }
}
