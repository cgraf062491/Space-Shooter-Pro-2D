using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
	[SerializeField] private float _speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -6)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
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
    			player.Damage();
    		}

    		Destroy(this.gameObject);
    	}
    }
}
