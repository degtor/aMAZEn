using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasData : MonoBehaviour {

	private int chosenPlayerInt;
	private int chosenDifficultyInt;
	public Text playerMenuItemText;
	public Text difficultyMenuItemText;
	static string[] playerNames = new string[] {
		"Speedy Ballzales",
		"Ballzy Jumper"
	};
	static string[] gameLevels = new string[] {
		"Easy",
		"Hard"
	};

	public void loadPlayerPrefs(){
		/* 
		 * Loading settings from PlayerPrefs
		 * If player is not set, it gets defined as 0 / Speedy Ballzales
		 * If difficulty is not set, it gets defined as 0 / Easy
		 */
		if (PlayerPrefs.GetInt ("player") != null) {
			chosenPlayerInt = PlayerPrefs.GetInt ("player");
		} else {
			chosenPlayerInt = 0;
		}

		if (PlayerPrefs.GetInt ("difficulty") != null) {
			chosenDifficultyInt = PlayerPrefs.GetInt ("difficulty");
		} else {
			chosenDifficultyInt = 0;
		}

	}

	void Update(){
		loadPlayerPrefs ();
		playerMenuItemText.text = "Player: " + playerNames [chosenPlayerInt];
		difficultyMenuItemText.text = "Difficulty: " + gameLevels [chosenDifficultyInt];
	}

}
