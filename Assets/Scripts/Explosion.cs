using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

	private AudioSource _audio;


    // Start is called before the first frame update
    void Start()
    {
    	_audio = GetComponent<AudioSource>();

    	if(_audio == null)
    	{
    		Debug.LogError("AudioSource was NULL");
    	}

    	_audio.Play();
        Destroy(this.gameObject, 3.0f);
    }
}
