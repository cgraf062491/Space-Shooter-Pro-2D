using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingBlast : MonoBehaviour
{
	private GameObject[] enemies;
	private GameObject target_enemy;
	private float distance = Mathf.Infinity;
	private Vector3 pos;

	private bool _foundEnemy = false;
	private float _speed = 6.0f;

    // Start is called before the first frame update
    void Start()
    {	
    	pos = transform.position;
    	enemies = GameObject.FindGameObjectsWithTag("Enemy");
        FindClosestEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if(target_enemy == null && _foundEnemy == true)
        {
            Destroy(this.gameObject);
            return;
        }
        if(_foundEnemy == true)
        {
        	transform.position = Vector3.MoveTowards(transform.position, target_enemy.transform.position, _speed * Time.deltaTime);
        }
    }

    void FindClosestEnemy()
    {
        if(enemies == null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            foreach(GameObject go in enemies)
            {
                Vector3 diffrence = go.transform.position - pos;
                float new_distance = diffrence.sqrMagnitude;
                if(new_distance < distance)
                {
                    target_enemy = go;
                    distance = new_distance;
                }
            }
            _foundEnemy = true;
        }
    }
}
