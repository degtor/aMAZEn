using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	//Setting up audio track that can be set through the UI
	public AudioClip bombClip;
	private AudioSource source;

	// Runs once
	void Awake () {

		source = GetComponent<AudioSource>();

	}

	// Detecting collision to play a sound
	void OnTriggerExit(Collider other){
		source.PlayOneShot(bombClip, 1F);
	}

}
