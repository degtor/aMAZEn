using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonEasy : MonoBehaviour {

	public GameObject difficultyMenuPanel;

	public void buttonEasySet(){
		PlayerPrefs.SetInt ("difficulty", 0);
		difficultyMenuPanel.SetActive (false);
	}
}
