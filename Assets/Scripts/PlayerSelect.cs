using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour {

	public GameObject playerMenuPanel;

	// Opening the modal window for player selection
	public void openPlayerMenu(){
		playerMenuPanel.SetActive (true);
	}
}
