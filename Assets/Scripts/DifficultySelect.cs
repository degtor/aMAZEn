using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelect : MonoBehaviour {

	public GameObject difficultyMenuPanel;

	// Opening the modal to select a difficulty level 
	public void openDifficultyMenu(){
		difficultyMenuPanel.SetActive (true);
	}
}
