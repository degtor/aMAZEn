using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		// Rotating the pick-up object
		transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);
	}
}
