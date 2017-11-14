using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonHard : MonoBehaviour {

	public GameObject difficultyMenuPanel;

	public void buttonHardSet(){
		PlayerPrefs.SetInt ("difficulty", 1);
		difficultyMenuPanel.SetActive (false);
	}
}
