using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	public AudioClip bombClip;
	private AudioSource source;

	void Awake () {

		source = GetComponent<AudioSource>();

	}

	void OnTriggerExit(Collider other){
		source.PlayOneShot(bombClip, 1F);
	}

}
