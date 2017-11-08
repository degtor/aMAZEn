using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		float translation = Time.deltaTime;
		transform.Rotate (new Vector3 (90, 180, 30)* translation);
	}
}
