using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acid : MonoBehaviour
{
    AudioSource aSource;
    void Start()
    {
        aSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	private void OnTriggerEnter(Collider other) 
	{
		other.BroadcastMessage("Kill", null, SendMessageOptions.DontRequireReceiver);
		aSource.Play();
	}
}
