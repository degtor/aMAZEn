using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasData : MonoBehaviour {

	private int chosenPlayerInt;
	public Text playerMenuItemText;
	static string[] playerNames = new string[] {
		"Speedy Ballzales",
		"Ballzy Jumper"
	};

	public void loadPlayerPrefs(){
		/* 
		 * Loading settings from PlayerPrefs
		 * If player is not set, it gets defined as 1 / Speedy Ballzales
		 */
		if (PlayerPrefs.GetInt ("player") != null) {
			chosenPlayerInt = PlayerPrefs.GetInt ("player");
		} else {
			chosenPlayerInt = 0;
		}
	}

	void Update(){
		loadPlayerPrefs ();
		playerMenuItemText.text = "Player: " + playerNames [chosenPlayerInt];
	}

}
