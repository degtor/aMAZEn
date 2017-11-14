using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelect : MonoBehaviour {

	public GameObject difficultyMenuPanel;

	public void openDifficultyMenu(){
		difficultyMenuPanel.SetActive (true);
	}
}
