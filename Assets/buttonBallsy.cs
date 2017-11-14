using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonBallsy : MonoBehaviour {

	public GameObject playerMenuPanel;

	public void buttonSpeedySet(){
		PlayerPrefs.SetInt ("player", 1);
		playerMenuPanel.SetActive (false);
	}
}
