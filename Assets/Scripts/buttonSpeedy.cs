using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonSpeedy : MonoBehaviour {

	public GameObject playerMenuPanel;

	// Saving player settings and closing the player modal
	public void buttonSpeedySet(){
		PlayerPrefs.SetInt ("player", 0);
		playerMenuPanel.SetActive (false);
	}

}
