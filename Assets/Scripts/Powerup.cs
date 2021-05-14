using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
	[SerializeField] private float _speed = 3.0f;
	[SerializeField] private int _powerupID;

    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }
	
	void Update()
	{
        if(Input.GetKey(KeyCode.C))
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, _speed * Time.deltaTime);
        }
        
		transform.Translate(Vector3.down * _speed * Time.deltaTime);

		if(transform.position.y < -6.0f)
		{
			Destroy(this.gameObject);
		}
	}
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other is BoxCollider2D)
        {
            if(other.CompareTag("Player"))
            {
                Player player = other.transform.GetComponent<Player>();
                if(player != null)
                {
                    switch(_powerupID)
                    {
                        case 0:
                            player.TripleShotActivate();
                            break;

                        case 1:
                            player.SpeedActivate();
                            break;

                        case 2:
                            player.ShieldActivate();
                            break;
                        case 3:
                            player.AmmoRefill();
                            break;
                        case 4:
                            player.HealthRefill();
                            break;
                        case 5:
                            player.AmmoDown();
                            break;
                        case 6:
                            player.CrossShotActivate();
                            break;
                        case 7:
                            player.SeekingShotActivate();
                            break;
                    }
                }
                Destroy(this.gameObject);
            }
            else if(other.CompareTag("EnemyLaser"))
            {
            	Destroy(this.gameObject);
            	Destroy(other.gameObject);
            }
        }
    }
}
