using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
	[SerializeField] private float _speed = 3.0f;
	[SerializeField] private int _powerupID;
	
	void Update()
	{
		transform.Translate(Vector3.down * _speed * Time.deltaTime);

		if(transform.position.y < -6.0f)
		{
			Destroy(this.gameObject);
		}
	}
    
    void OnTriggerEnter2D(Collider2D other)
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
    			}
    		}
    		Destroy(this.gameObject);
    	}
    }
}
