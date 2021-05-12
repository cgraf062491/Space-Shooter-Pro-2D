using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] private float _speed = 4.0f;
    [SerializeField] private AudioClip _explosion_clip;
    [SerializeField] private AudioClip _enemyLaser_clip;
    [SerializeField] private GameObject _enemyLaser;

    private Player player;
    private Animator anim;
    private AudioSource audio;

    private bool _canShoot = true;
    private float _origX;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        _origX = transform.position.x;

        if(player == null)
        {
        	Debug.LogError("Player was NULL");
        }

        if(anim == null)
        {
        	Debug.LogError("Animator on Enemy was NULL");
        }

        if(audio == null)
        {
            Debug.LogError("AudioSource on Enemy was NULL");
        }

        StartCoroutine(ShootLaser());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.x <= _origX - 5.0f)
        {
        	transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }
        else if(transform.position.x >= _origX + 5.0f)
        {
        	transform.Translate(Vector3.left * _speed * Time.deltaTime);
        }

        if(transform.position.y < -5.5f)
        {
        	float x_pos = Random.Range(-9.5f, 9.5f);
        	transform.position = new Vector3(x_pos, 7.0f, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
    	if(other.CompareTag("Player"))
    	{
    		//Damage player
    		//Player player = other.transform.GetComponent<Player>();

    		if(player != null)
    		{
    			player.Damage();
    		}

    		_speed = 0f;
            _canShoot = false;
    		anim.SetTrigger("OnEnemyDeath");
            audio.PlayOneShot(_explosion_clip);
            Destroy(GetComponent<Collider2D>());
    		//Destroy(this.gameObject);
    	}
    	else if(other.CompareTag("Laser"))
    	{
    		Destroy(other.gameObject);
            if(player != null)
            {
                player.EnemyDestroyed();
            }
            
            _speed = 0f;
            _canShoot = false;
            anim.SetTrigger("OnEnemyDeath");
            audio.PlayOneShot(_explosion_clip);
            Destroy(GetComponent<Collider2D>());
    		//Destroy(this.gameObject);
    	}
    }

    IEnumerator ShootLaser()
    {
    	while(_canShoot == true)
    	{
    		float laser_delay = Random.Range(3.0f, 5.0f);
    		yield return new WaitForSeconds(laser_delay);
    		Instantiate(_enemyLaser, transform.position + new Vector3(0, -1.0f, 0), Quaternion.identity);
    		audio.PlayOneShot(_enemyLaser_clip);
    	}
    }

    public void DestroySelf()
    {
    	Destroy(this.gameObject);
    }
}
