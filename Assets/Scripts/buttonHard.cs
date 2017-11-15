using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonHard : MonoBehaviour {

	public GameObject difficultyMenuPanel;

	// Saving game difficulty player settings and closing its modal
	public void buttonHardSet(){
		PlayerPrefs.SetInt ("difficulty", 1);
		difficultyMenuPanel.SetActive (false);
	}
}
