using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
	[SerializeField] private float _speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if(transform.position.y > 7 || transform.position.y < -7 || transform.position.x > 10 || transform.position.x < -10)
        {
            if(transform.parent != null && transform.parent.transform.childCount <= 1)
            {
                Destroy(transform.parent.gameObject);
            }
        	Destroy(this.gameObject);
        }
    }
}
