using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossSeeker : MonoBehaviour
{
	private GameObject target;
	private bool _foundPlayer = false;

	[SerializeField] private float _speed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {	
    	target = GameObject.Find("Player");
    	if(target == null)
    	{
    		Destroy(this.gameObject);
    	}
    	else
    	{
    		_foundPlayer = true;
    	}

    	Destroy(this.gameObject, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(this.gameObject);
            return;
        }
        if(_foundPlayer == true)
        {
        	transform.position = Vector3.MoveTowards(transform.position, target.transform.position, _speed * Time.deltaTime);
        }
    }
}
