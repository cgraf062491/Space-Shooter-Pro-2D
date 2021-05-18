using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
	[SerializeField] private float _speed = 2.5f;
	[SerializeField] private int _health = 50;
	[SerializeField] private GameObject _tripleShot;
	[SerializeField] private GameObject _homingShot;
	[SerializeField] private GameObject _fastShot;
	[SerializeField] private AudioClip _laserSound;
	[SerializeField] private AudioClip _tripleLaserSound;
	[SerializeField] private AudioClip _homingSound;
	[SerializeField] private AudioClip _explosionSound;


	private GameObject _player;
	private AudioSource _audio;
	private Renderer _sprite;
	private Animator _anim;
	private UIManager _uiManager;
	private GameManager _gameManager;

	private bool _inPosition = false;
	private bool _playerDefeated = false;
	private bool _kindaHurt = false;
	private bool _veryHurt = false;
	private bool _isDefeated = false;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        _audio = GetComponent<AudioSource>();
        _sprite = GetComponent<Renderer>();
        _anim = GetComponent<Animator>();

        if(_player	== null)
        {
        	Debug.LogError("Player is NULL");
        }

        if(_audio	== null)
        {
        	Debug.LogError("AudioSource is NULL");
        }

        if(_sprite	== null)
        {
        	Debug.LogError("Renderer is NULL");
        }

        if(_anim	== null)
        {
        	Debug.LogError("Animator is NULL");
        }

    }

    // Update is called once per frame
    void Update()
    {
    	if(_inPosition == false)
        {
        	if(transform.position.y <= 2)
        	{
        		_inPosition = true;
        		StartCoroutine(Attack());
        	}
        }

        if(_inPosition == false)
        {
        	//Debug.Log("Moving down");
        	transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }

        if(_kindaHurt == false && (_health <= 35 && _health > 15))
        {
        	_kindaHurt = true;
        	_sprite.material.color = Color.yellow;
        }
        else if(_veryHurt == false && _health <= 15)
        {
        	_veryHurt = true;
        	_sprite.material.color = Color.red;
        }
    }

    IEnumerator Attack()
    {
    	float waitTime = Random.Range(2.0f, 4.0f);
    	yield return new WaitForSeconds(waitTime);
    	while(_health > 0 && _playerDefeated == false)
    	{
    		int randomAttack = Random.Range(0, 3);
    		switch (randomAttack)
    		{
    			case 0:
    				Instantiate(_tripleShot, transform.position + new Vector3(-1f, 0, 0), Quaternion.identity);
    				_audio.PlayOneShot(_tripleLaserSound);
    				yield return new WaitForSeconds(1.5f);
    				Instantiate(_tripleShot, transform.position + new Vector3(1f, 0, 0), Quaternion.identity);
    				_audio.PlayOneShot(_tripleLaserSound);
    				break;
    			case 1:
    				for(int i = 0; i < 3; i ++)
    				{
    					Instantiate(_homingShot, transform.position, Quaternion.identity);
    					_audio.PlayOneShot(_homingSound);
    					yield return new WaitForSeconds(1f);
    				}
    				break;
    			case 2:
    				//Shoot in a circle covering the screen. Make the player circle.
    				for(int i = -65; i < 65; i++)
    				{
    					Instantiate(_fastShot, transform.position, Quaternion.Euler(0, 0, i));
    					_audio.PlayOneShot(_laserSound);
    					yield return new WaitForSeconds(0.03f);
    				}
    				break;
    		}

    		waitTime = Random.Range(3.0f, 5.0f);
    		yield return new WaitForSeconds(waitTime);
    	}
    }

    void OnTriggerEnter2D(Collider2D other)
    {
    	if(other.CompareTag("Laser") && other is BoxCollider2D)
    	{
    		_health -= 1;
    		Destroy(other.gameObject);

    		if(_health <= 0 && _isDefeated == false)
    		{
    			_isDefeated = true;
    			EnemyBossDeath();
    		}
    	}

    	if(other.CompareTag("Player") && other is BoxCollider2D)
    	{
    		_health -= 1;

    		if(_player != null)
            {
                _player.GetComponent<Player>().Damage();
            }

            if(_health <= 0 && _isDefeated == false)
            {
            	_isDefeated = true;
            	EnemyBossDeath();
            }
    	}
    }

    void EnemyBossDeath()
    {
    	_anim.SetTrigger("EnemyBossDefeated");
    	_audio.PlayOneShot(_explosionSound);
    	Destroy(this.gameObject, 5.0f);
    	_uiManager.GameWin();
    	_gameManager.GameOver();

    }
}
