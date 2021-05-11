using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitToSayHello());
    }

    IEnumerator WaitToSayHello()
    {
    	//yield return new WaitForSeconds(2.0f);
    	yield return null;
    	Debug.Log("Hello World");
    }
}
