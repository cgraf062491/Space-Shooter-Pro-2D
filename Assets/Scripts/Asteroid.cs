using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
	[SerializeField] private float _rotateSpeed;
	[SerializeField] private GameObject _explosion;

    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

        if(_spawnManager == null)
        {
            Debug.LogError("Spawn Manager was NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, _rotateSpeed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
    	if(other.CompareTag("Laser"))
    	{
            if(other is BoxCollider2D)
            {
                Instantiate(_explosion, transform.position, Quaternion.identity);
                Destroy(other.gameObject);
                _spawnManager.StartSpawning();
                Destroy(this.gameObject, 0.2f);
            }
    	}
    }
}
