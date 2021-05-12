using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

	//private Transform _camTransform;
	private Vector3 _origCamPos;

	[SerializeField] private float _duration;
	[SerializeField] private float _power = 0.7f;

    // Start is called before the first frame update
    void Start()
    {
    	_origCamPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(_duration > 0)
        {
        	transform.localPosition = _origCamPos + Random.insideUnitSphere * _power;
        	_duration -= Time.deltaTime;
        }
        else
        {
        	_duration = 0f;
        	transform.localPosition = _origCamPos;
        }
    }

    public void Shake()
    {
    	_duration = 0.6f;
    }
}
