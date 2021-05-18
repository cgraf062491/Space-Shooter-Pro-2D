using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	[SerializeField] private GameObject[] _enemies;
    [SerializeField] private GameObject[] powerups;
	[SerializeField] private GameObject _enemyContainer;

	private bool _stopSpawningEnemies;
    private bool _stopSpawningPowerups;
    private bool _bossSpawned;
    private int _waveNum = 2;
    private float _waveWait = 5.0f;
    

    public void StartSpawning()
    {
    	StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerup());
    }

    IEnumerator SpawnEnemy()
    {
    	yield return new WaitForSeconds(3.0f);
    	while(_stopSpawningEnemies == false)
    	{
            _waveNum += 1;
            int enemyType;
            if(_waveNum <= 8)
            {
                for(int i = 0; i < _waveNum; i++)
                {
                    float x_pos = Random.Range(-9.5f, 9.5f);
                    float randomType = Random.Range(0.0f, 1.0f);
                    if(randomType >= 0.0f && randomType <= 0.25f)
                    {
                        enemyType = 0;
                    }
                    else if(randomType > 0.25 && randomType <= 0.45)
                    {
                        enemyType = 4;
                    }
                    else if(randomType > 0.45 && randomType <= 0.65)
                    {
                        enemyType = 3;
                    }
                    else if(randomType > 0.65f && randomType <= 0.9f)
                    {
                        enemyType = 2;
                    }
                    else
                    {
                        enemyType = 1;
                    }
                    GameObject newEnemy = Instantiate(_enemies[enemyType], new Vector3(x_pos, 7.0f, 0), Quaternion.identity);
                    newEnemy.transform.parent = _enemyContainer.transform;
                    yield return new WaitForSeconds(1.0f);
                }
                yield return new WaitForSeconds(_waveWait);
                _waveWait += 2.0f;
            }
            else
            {
                //yield return new WaitForSeconds(20.0f);
                _bossSpawned = true;
                _stopSpawningEnemies = true;
                GameObject bossEnemy = Instantiate(_enemies[5], new Vector3(0, 12.0f, 0), Quaternion.identity);
                bossEnemy.transform.parent = _enemyContainer.transform;
            }   
        }	
    }

    IEnumerator SpawnPowerup()
    {
    	yield return new WaitForSeconds(3.0f);
        while(_stopSpawningPowerups == false)
        {
            float x_pos = Random.Range(-9.5f, 9.5f);
            float spawnTime = Random.Range(3.0f, 7.0f);
            int spawnType = 3;
            float randomType = Random.Range(0.0f, 1.0f);
            if(randomType >= 0.0f && randomType <= 0.25f)
            {
                spawnType = 3;
            }
            else if(randomType > 0.25f && randomType <= 0.44f)
            {
                spawnType = 0;
            }
            else if(randomType > 0.44f && randomType <= 0.62f)
            {
                spawnType = 1;
            }
            else if(randomType > 0.62f && randomType <= 0.8f)
            {
                spawnType = 2;
            }
            else if(randomType > 0.80f && randomType <= 0.85f)
            {
                spawnType = 4;
            }
            else if(randomType > 0.85f && randomType <= 0.9f)
            {
                spawnType = 5;
            }
            else if(randomType > 0.9f && randomType <= 0.95f)
            {
                spawnType = 6;
            }
            else
            {
                spawnType = 7;
            }
            Instantiate(powerups[spawnType], new Vector3(x_pos, 7.0f, 0), Quaternion.identity);
            yield return new WaitForSeconds(spawnTime);
        }
    }

    public void OnPlayerDeath()
    {
    	_stopSpawningEnemies = true;
        _stopSpawningPowerups = true;
    }
}
