using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	//public variables can be seen by the world
	//private variables can only be seen by this class
	//data type (int, float, bool, string)
	//optional value for each declared variable
	[SerializeField] private int _lives = 3;
	[SerializeField] private int _score = 0;

	[SerializeField] private float _speed = 5.0f;
	[SerializeField] private float _fireRate = 0.15f;
	[SerializeField] private GameObject _laser;
	[SerializeField] private GameObject _tripleLaser;
	[SerializeField] private GameObject _playerShield;
	[SerializeField] private GameObject _playerThrusters;
	[SerializeField] private GameObject[] _playerEngines;
	[SerializeField] private AudioClip _laser_clip;
	[SerializeField] private AudioClip _powerup_clip;

	private bool _tripleShot = false;
	private bool _shieldActive = false;
	private bool _speedActive = false;

	private int _shieldLevel = 3;
	private int _ammoCount = 15;
	private float _canFire = -1f;
	private SpawnManager _spawnManager;
	private UIManager _uiManager;
	private GameManager _gameManager;
	private AudioSource _audio;


	void Start()
	{
		//Take current position = new position (0, 0, 0)
		transform.position = new Vector3(0, 0, 0);

		_spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
		_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
		_gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

		_audio = GetComponent<AudioSource>();

		if(_spawnManager == null)
		{
			Debug.LogError("Spawn Manager is NULL");
		}

		if(_uiManager == null)
		{
			Debug.LogError("UI Manager is NULL");
		}

		if(_gameManager == null)
		{
			Debug.LogError("Game Manager is NULL");
		}

		if(_audio == null)
		{
			Debug.LogError("AudioSource is NULL");
		}
	}

	void Update()
	{
		// Run all movement code
		Movement();

		if(Input.GetKey(KeyCode.LeftShift) && _speedActive == false)
		{
			_speed = 7.0f;
			_playerThrusters.SetActive(true);
		}
		else if(Input.GetKeyUp(KeyCode.LeftShift) && _speedActive == false)
		{
			_speed = 5.0f;
			_playerThrusters.SetActive(false);
		}

		if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
		{
			Laser();
		}
	}

	void Movement()
	{
		// Gets the player input on Arrow keys and WASD
		float x_input = Input.GetAxisRaw("Horizontal");
		float y_input = Input.GetAxisRaw("Vertical");

		//Move the player based on the horizontal and vertical input as well as speed
		//transform.Translate(Vector3.right * x_input * _speed * Time.deltaTime);
		//transform.Translate(Vector3.up * y_input * _speed * Time.deltaTime);

		//More optimized version of movement
		Vector3 direction = new Vector3(x_input, y_input, 0);
		transform.Translate(direction * _speed * Time.deltaTime);

		//Keep the player bounded on the y axis
		transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.0f, 0.0f), 0);

		//Wrap the player on the x axis if out of bounds
		if(transform.position.x > 11.25f)
		{
			transform.position = new Vector3(-11.25f, transform.position.y, 0);
		}
		else if(transform.position.x < -11.25f)
		{
			transform.position = new Vector3(11.25f, transform.position.y, 0);
		}
	}

	void Laser()
	{
		if(_ammoCount > 0)
		{
			_ammoCount -= 1;
			_uiManager.UpdateAmmo(_ammoCount);
			_canFire = Time.time + _fireRate;
			
			if(_tripleShot == true)
			{
				Instantiate(_tripleLaser, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
			}
			else
			{
				Instantiate(_laser, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
			}

			//player audio clip	
			_audio.PlayOneShot(_laser_clip);
		}	
	}

	public void Damage()
	{
		if(_shieldActive == true)
		{
			_shieldLevel -= 1;
			switch(_shieldLevel)
			{
				case 2:
					_playerShield.GetComponent<Renderer>().material.color = Color.yellow;
					break;
				case 1:
					_playerShield.GetComponent<Renderer>().material.color = Color.red;
					break;
				case 0:
					_shieldActive = false;
					_playerShield.SetActive(false);
					break;
			}
			return;
		}

		_lives -= 1;
		_uiManager.UpdateLives(_lives);

		if(_lives == 2)
		{
			_playerEngines[0].SetActive(true);
		}
		else if(_lives == 1)
		{
			_playerEngines[1].SetActive(true);
		}

		if(_lives <= 0)
		{
			_spawnManager.OnPlayerDeath();
			_gameManager.GameOver();
			Destroy(this.gameObject);
		}
	}

	public void TripleShotActivate()
	{
		_tripleShot = true;
		_audio.PlayOneShot(_powerup_clip);
		StartCoroutine(TripleShotDeactivate());
	}

	IEnumerator TripleShotDeactivate()
	{
		yield return new WaitForSeconds(5.0f);
		_tripleShot = false;
	}

	public void SpeedActivate()
	{
		_speed = 8.5f;
		_speedActive = true;
		_audio.PlayOneShot(_powerup_clip);
		StartCoroutine(SpeedDeactivate());
	}

	IEnumerator SpeedDeactivate()
	{
		yield return new WaitForSeconds(5.0f);
		_speedActive = false;
		_speed = 5.0f;
	}

	public void ShieldActivate()
	{
		_shieldActive = true;
		_shieldLevel = 3;
		_playerShield.GetComponent<Renderer>().material.color = Color.white;
		_audio.PlayOneShot(_powerup_clip);
		_playerShield.SetActive(true);
	}

	public void AmmoRefill()
	{
		_ammoCount = 15;
		_uiManager.UpdateAmmo(_ammoCount);
	}

	public void HealthRefill()
	{
		_lives += 1;

		if(_lives == 2)
		{
			_playerEngines[1].SetActive(false);
		}
		else if(_lives == 3)
		{
			_playerEngines[0].SetActive(false);
		}
		_uiManager.UpdateLives(_lives);
	}

	public void EnemyDestroyed()
	{
		_score += 10;
		_uiManager.UpdateScore(_score);
	}
}
